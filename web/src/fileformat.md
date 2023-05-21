# File Format

File format is UTF-8 encoded text consisting of multiple sections, each with
field definitions inside.

Suggested extension is `.aaseq`.


# Comments

Each line can be commented out using hash character (#). If character appears
unquoted in the middle of line, all characters after it will be ignored (i.e.
mid-line comments are also supported).

All empty lines are ignored if they are not part of field value.

It is suggested that each file starts with shebang interpreter directive, e.g.:

    #!/usr/bin/env aaseq


# Sections

Each section is enclosed in square brackets ([]) and it can be one of the
following kinds: Endpoint, Message, or Command. Any section content is
case-insensitive.


## Endpoints

Endpoint section controls the definition for each endpoint. All endpoints are
processed in file order and should include Name and Plugin fields.

For example:

    [Name: Plugin]

Name is the identifier used further in file for given endpoint. It must start
with a letter and it can contain letters, numbers, and underscore character.

Plugin is an optional field specifying which protocol plugin is to be used. If
not specified, it will be assumed to be the same as Name. Plugin must start
with a letter and it can contain letters, numbers, and underscore character.

Special endpoint "Me" signifies application itself. If it's not defined, it
will be automatically added.


## Messages

Message section has definition of each outgoing or incoming message. It contains
Source, Direction, Destination, and Message.

All the following entries are valid message definitions:

    [> Destination Message]
    [< Destination Message]
    [Source > Destination Message]
    [Source < Destination Message]

Source identifies the first endpoint for a message. It must match the Name of
the previously defined endpoint. If source is omitted, endpoint `Me` is assumed
to be a source. If neither Source nor Destination are `Me` endpoint, there will
be no action for the message.

Direction can be either outgoing (`>`) or incoming (`<`).

Destination identifies the second endpoint for a message. It must match the
Name of previously defined endpoint and cannot be omitted.

Message is the name of message and it's dependant on protocol definition. If
destination is "Me" or protocol supports only one message type, message can be
omitted. It must start with a letter and it can contain letters, numbers, dash,
and underscore character.


## Commands

Command section executes builtin commands without any external communication.

The following entry is example of valid definition.

    [!Command]

Command is prefixed with exclamation point (`!`). Command must start with a
letter and it can contain letters, numbers, and underscore character.


# Fields

Each section can contain fields if additional information is necessary. Basic
field is a key/value pair, using colon as a separator, e.g.:

    Key: Value

Key must start with a letter and it can contain letters, numbers, dash, and
underscore character. Anything after colon (`:`) is considered to be a value.

If field contains sub-fields, they are to be listed under key with a higher
indentation level. E.g.:

    Key:
        Subfields1: SubValue1
        Subfields2: SubValue2

Multiline strings can be added in manner similar to Yaml. When using literal
`|` as a value, lines below will be taken as they are with their indentation
removed. A single new line will be part of resulting string, e.g.

    Key: |
        Line 1
        Line 2

If new line at the end of the string is not needed, one can use `|-`. If one
wants to keep all line endings, one can use `|+`.

If there's need for additional information, each key can include tag entries.
Tags must start with a letter and they can contain letters, numbers, and
underscore character. Multiple tags are separated by space.

System tags will additionally have at sign (`@`) as a prefix, e.g.:

    Key [@tag]: Value

There cannot be multiple tags with the same name (including the system tags).


## Data Types

Each fields will auto-detect its data type. However, if data type has to be
forced, one can use system tags. The following types are supported:

| Data type    | .NET data type | Tag             | Example value                      |
|--------------|----------------|-----------------|------------------------------------|
| Boolean      | Boolean        | @bool           | `true`                             |
| Int8         | SByte          | @int8           | `42`                               |
| Int16        | Int16          | @int16          | `42`                               |
| Int32        | Int32          | @int32          | `42`                               |
| Int64        | Int64          | @int64          | `42`                               |
| Int64        | Int64          | @int            | `42`                               |
| UInt8        | Byte           | @uint8          | `42`                               |
| UInt16       | UInt16         | @uint16         | `42`                               |
| UInt32       | UInt32         | @uint32         | `42`                               |
| UInt64       | UInt64         | @uint64         | `42`                               |
| UInt64       | UInt64         | @uint           | `42`                               |
| Float16      | Half           | @float16        | `42.0`                             |
| Float32      | Single         | @float32        | `42.0`                             |
| Float64      | Double         | @float64        | `42.0`                             |
| Float64      | Double         | @float          | `42.0`                             |
| DateTime     | DateTimeOffset | @datetime       | `2021-01-02T03:04:05` `1621824203` |
| Date         | DateOnly       | @date           | `2021-01-02`                       |
| Time         | TimeOnly       | @time           | `03:04:05`                         |
| Duration     | TimeSpan       | @duration       | `1d`                               |
| String       | String         | @string         | `Text`                             |
| IPAddress    | IPAddress      | @ip             | `1.2.3.4` `2001:db8::/32`          |
| IPAddressv4  | IPAddress      | @ipv4           | `1.2.3.4`                          |
| IPAddressv6  | IPAddress      | @ipv6           | `2001:db8::/32`                    |
| IPEndpoint   | IPEndPoint     | @ep             | `1.2.3.4:443` `[2001:db8::]:443`   |
| IPEndpointv4 | IPEndPoint     | @epv4           | `1.2.3.4:443`                      |
| IPEndpointv6 | IPEndPoint     | @epv6           | `[2001:db8::]:443`                 |
| Binary       | Byte[]         | @binary         | `4C6F7265 6D206970 73756D`         |
| Binary       | Byte[]         | @binary @hex    | `4C6F7265 6D206970 73756D`         |
| Binary       | Byte[]         | @binary @base64 | `TG9yZW0gaXBzdW0K`                 |

## Quoting

Unless otherwise noted, strings variables can be enclosed in either single or
double quotes.

### Single Quotes

If single quotes are used, all characters inside are taken as is until the
next single quote occurs. If two single quotes appear next to each other, this
will result in a single quote character being present. Special characters are
not expanded.

Here are a few examples:

| Quoted text               | Results                 |
|---------------------------|-------------------------|
| `'Single "double" quote'` | `Single "double" quote` |
| `'I''m escaped'`          | `I'm escaped`           |

### Double Quotes

Double quotes allow for a more rich escaping. Each escape sequence starts with
backslash (\\) and can be seen as 

| Description                | Character | Escape       |
|----------------------------|-----------|--------------|
| BEL (alert)                | 0x07      | `\a`         |
| BS (backspace)             | 0x08      | `\b`         |
| HT (tab)                   | 0x09      | `\t`         |
| LF (line feed)             | 0x0A      | `\n`         |
| VT (vertical tab)          | 0x0B      | `\v`         |
| FF (form feed)             | 0x0C      | `\f`         |
| CR (carriage return)       | 0x0C      | `\r`         |
| ESC (escape)               | 0x1B      | `\e`         |
| Dollar sign                | `$`       | `\$`         |
| Quotation mark             | `"`       | `\"`         |
| Backslash                  | `\`       | `\\`         |
| ASCII character            |           | `\x##`       |
| Unicode character (16-bit) |           | `\u####`     |
| Unicode character (32-bit) |           | `\U########` |

In addition to these, it's not necessary to escape the following characters
but their escape sequences are supported.

| Description                | Character | Escape       |
|----------------------------|-----------|--------------|
| Apostrophe                 | `'`       | `\'`         |
| Question mark              | `?`       | `\?`         |

Within double quotes, one can also include a reference to other variables
using `$` character followed by variable name, e.g.:

    $VARIABLE

AASeq Document Language
=======================

NOTE: This specification is work in progress.

Document is a simplified node-based case-insensitive language that aims to
provide Attribute-Value pair semantics with strictly defined data types and
conversions.


## Overview

### Node

A node consists of a node name string, followed by zero or more properties,
optional value, and optional children:
~~~
title "Hello, World"
~~~

Node name can be any non-white space unicode character except for braces (`{`
and `}`), brackets (`(` and `)`), slashes (`\` and `/`), equals character (`=`),
quote (`"`), semicolon (`;`), and hash character (`#`). Full list of forbidden
node characters is thus: `{}()\/=";#`. Not all forbidden characters are
utilized at this time.

If properties are to be used, they are listed canonically listed immediately
after node name, separated by one or more whitespace characters. Property name
follows the same naming rules as node name does while value can be quoted as
necessary.
~~~
author lastName=John firstName=Green isAlive=true
~~~


### Property

Any number of properties can follow node name. Property name follows the same
limitations as the node name.

Property is defined by its name, followed immediately with equals character
(`=`), and finally value. No space is to be used before or after equals.
~~~
author firstName=John lastName=Green
~~~

Property value is always a string.

If there are duplicates, the later defined property will overwrite previously
defined one. Please note that names are case-insensitive by design and thus `P1`
and `p1` are considered to be the same property name.

~~~
author firstName=John lastName=Green
~~~

Property value can also use simple quoting.
~~~
author fullName="John Green"
~~~

Multiline quoting for properties is not supported.


### Value

Each node can also contain a single value.
~~~
author "John Green"
~~~

Value can come either before or after properties but canonical output will
always place it after. In case multiple values exist, the last one will be used.

Each value can also have a type prefix in round brackets that will denote its
type:
~~~
author (u64)42
~~~

While values are strongly typed, it is not necessary to prefix each value with
a type annotation. If there is no such annotation, the following conversions
will apply:

| Text                   | Annotation | C# equivalent             |
|------------------------|------------|---------------------------|
| `null`                 | N/A        | N/A                       |
| `false`                | `bool`     | `(bool)false`             |
| `true`                 | `bool`     | `(bool)true`              |
| `NaN`                  | `f64`      | `double.NaN`              |
| `+Inf`                 | `f64`      | `double.Infinity`         |
| `-Inf`                 | `f64`      | `double.NegativeInfinity` |
| Integer numbers        | `i32`      | `int`                     |
| Floating-point numbers | `f64`      | `double`                  |
| Strings                | `string`   | `string`                  |

The following data types are supported for the value:

| Annotation | C# equivalent             | Description                         |
|------------|---------------------------|-------------------------------------|
| `bool`     | `Boolean` (`bool`)        | Boolean data type                   |
| `i8`       | `SByte` (`sbyte`)         | Signed 8-bit integer                |
| `u8`       | `Byte` (`byte`)           | Unsigned 8-bit integer              |
| `i16`      | `Int16` (`short`)         | Signed 16-bit integer               |
| `u16`      | `UInt16` (`ushort`)       | Unsigned 16-bit integer             |
| `i32`      | `Int32` (`int`)           | Signed 32-bit integer               |
| `u32`      | `UInt32` (`uint`)         | Unsigned 32-bit integer             |
| `i64`      | `Int64` (`long`)          | Signed 64-bit integer               |
| `u64`      | `UInt64` (`ulong`)        | Unsigned 64-bit integer             |
| `i128`     | `Int128`                  | Signed 128-bit integer              |
| `u128`     | `UInt128`                 | Unsigned 128-bit integer            |
| `f16`      | `Half`                    | 16-bit floating point number        |
| `f32`      | `Single` (`float`)        | 32-bit floating point number        |
| `f64`      | `Double` (`double`)       | 64-bit floating point number        |
| `d128`     | `Decimal` (`decimal`)     | 128-bit decimal number              |
| `datetime` | `DateTimeOffset`          | Date, time, and timezone            |
| `dateonly` | `DateOnly`                | Only date component                 |
| `timeonly` | `TimeOnly`                | Only time component                 |
| `duration` | `TimeSpan`                | Time duration                       |
| `ip`       | `IPAddress`               | IP address                          |
| `regex`    | `Regex`                   | Regular expression                  |
| `uri`      | `Uri`                     | URI                                 |
| `uuid`     | `Guid`                    | UUID                                |
| `base64`   | `Byte[]` (`byte[]`)       | Raw bytes, `hex` is preferred       |
| `hex`      | `Byte[]` (`byte[]`)       | Raw bytes                           |
| `string`   | `String` (`string`)       | String                              |


#### Null

Null values are always unquoted.
~~~
node null
~~~

Any node without children is implied to have value `null`.
~~~
node
~~~


#### Booleans

Boolean values are always unquoted.
~~~
node1 true
node2 false
~~~


#### Numbers

Value is considered a number, if value is unquoted and matches the following
rules:
* value starts with a digit;
* value starts with `+`, `-`, `.`, and a digit
* value starts with `-.`, `+.`, and a digit

Values are parsed in the following order:
* float constants (`NaN`, `+Inf`, `-Inf`)
* hexadecimal `0x` prefix values
* binary `0b` prefix values
* integer values
* float values

Numeric values always use comma (`,`) as thousand separator and a period (`.`)
as a decimal point. For hexadecimal and binary output, use of underscore (`_`)
separator is also allowed.


#### Strings

If value doesn't match any of the above types, it is considered to be an
unquoted string. No escape sequences are allowed in unquoted strings. Unquoted
string also cannot contain any of the following:
* braces (`{` and `}`): used for children nodes
* brackets (`(` and `)`): used for type annotation
* backslash (`\`): reserved
* forward slash (`/`): complex comments
* equals character (`=`): used for properties
* quote (`"`): used to start quoted string
* semicolon (`;`): used to end children node
* hash (`#`). Full list of forbidden node characters is thus:

Values will use unquoted strings if they don't match any of the null, boolean,
or number characteristics.


### Child Node

Alteratively, node can define its children.
~~~
author "John Green" {
    book "Looking for Alaska"
    book "The Fault in Our Stars"
    book "Everything Is Tuberculosis"
}
~~~

Nodes without children can be terminated using semicolon `;`.
~~~
node1; node2; node3;
~~~


### Quoting

If value is quoted, all characters are allowed, with following escape values:
* `\"`: double quote (`\u0022`)
* `\\`: backslash (`\u005C`)
* `\0`: null (`\u0000`)
* `\a`: alert (`\u0007`)
* `\b`: backspace (`\u0008`)
* `\e`: escape (`\u001B`)
* `\f`: Form feed (`\u000C`)
* `\n`: New line (`\u000A`)
* `\r`: Carriage return (`\u000D`)
* `\t`: Horizontal tab (`\u0009`)
* `\v`: Vertical tab (`\u000B`)
* `\x`: Unicode escape sequence (ASCII, `\uHH`)
* `\u`: Unicode escape sequence (UTF-16, `\uHHHH`)
* `\U`: Unicode escape sequence (UTF-32, `\UHHHHHHHH`)

~~~
node "Hello\nWorld!"
~~~


### Block Quoting

For easier writing of multiline strings, one can use block quotes that allow for
multiline strings. To start one, use at least 3 quote characters (`"`). and
close it with the same number of quotes.

~~~aaseq
node """Hello World,
        in "multiple" lines"""
nodeSingle "Hello World,\nin \"multiple\" lines"
~~~

Prefix whitespace present in second and later lines will be reduced by common
whitespace count.

Nothing is expected after node ends (i.e. node ends).

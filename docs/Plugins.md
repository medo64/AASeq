## Plugins

Plugins are identified by method signature only.
While there are `ICommandPlugin` and `IEndpointPlugin` definitions, their usage is not mandatory.

All plugin operations are also cooperative in nature.
For example, once engine sends control to any of the plugin methods (e.g., `TryReceive`), it will not interrupt the same, even if cancellation token expires.

There are two kinds of plugins: Command and Endpoint.
Command plugin executes an action that is not dependant on the external systems.
Endpoint plugin allows for handling of messages to/from external system.


### Command Methods

The following methods must be defined for all command plugins:
* `static Object CreateInstance()`
* `static AASeqNodes ValidateData(AASeqNodes data)`
* `bool TryExecute(AASeqNodes data, CancellationToken cancellationToken)`


#### CreateInstance Method

This static method will return the instance of the command.
No configuration data is expected as each command execution has a separate instance.


#### ValidateData Method

This method will return validated data for the instance.


#### TryExecute Method

This method will attempt to execute an command.
Data (`data`) containts any configuration needed.
Cancellation token (`cancellationToken`) is recommended for handling timeouts but its usage is not mandatory if operation has short duration.


### Endpoint Methods

The following methods must be defined for all endpoint plugins:
* `static Object CreateInstance(AASeqNodes configuration)`
* `static AASeqNodes ValidateConfiguration(AASeqNodes configuration)`
* `static AASeqNodes ValidateData(string messageName, AASeqNodes data)`
* `bool TrySend(Guid id, string messageName, AASeqNodes data, CancellationToken cancellationToken)`
* `bool TryReceive(Guid id, [MaybeNullWhen(false)] out string messageName, [MaybeNullWhen(false)] out AASeqNodes data, CancellationToken cancellationToken)`


#### CreateInstance Method

This static method will return the instance of the endpoint.
Configuration data (`configuration`) is provided as an argument.


#### ValidateConfiguration Method

This method will return validated configuration data for the instance.
Note that this data is used for all messages since it belongs in the common instance.


#### ValidateData Method

This method will return validated data for the instance.


#### TrySend Method

Attempts to send a message.
Id (`id`) provided is used to match with message received in `TryReceive` (if needed).
Message name (`messageName`) contains name of the message to send.
Provided data (`data`) contains any message-specific data needed.
Cancellation token (`cancellationToken`) is recommended for handling timeouts but its usage is not mandatory if operation has short duration.


#### TryReceive Method

Attempts to send a message.
Id (`id`) provided is used to match with message sent in `TrySend` (if needed).
Message name (`messageName`) contains name of the message received.
Data (`data`) contains any message-specific data received.
Cancellation token (`cancellationToken`) is recommended for handling timeouts but its usage is not mandatory if operation has short duration.

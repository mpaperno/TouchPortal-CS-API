# Touch Portal .NET API - Change Log

## v1.47.1 (26-Nov-24)
* Fixed `TriggerEventCommand` not being serialized properly for `TriggerEvent()` method (since v1.47.0). ([@2675613b](https://github.com/mpaperno/TouchPortal-CS-API/commit/2675613b))
* Changed `TriggerEventStates` dictionary type to `<string, string>` because `triggerEvent.states` values must be of string type. ([@dab4d6e0](https://github.com/mpaperno/TouchPortal-CS-API/commit/dab4d6e0))
* All `ITouchPortalEventHandler` method implementations are now optional in plugin code. ([@e6f2385d](https://github.com/mpaperno/TouchPortal-CS-API/commit/e6f2385d))
* Exposed `ICommandHandler.SendCommand()` method as public in `TouchPortalClient`. ([@134268b5](https://github.com/mpaperno/TouchPortal-CS-API/commit/134268b5))
* Actual JSON message being sent is now logged (at Debug level) instead of just success status. ([@0e954f08](https://github.com/mpaperno/TouchPortal-CS-API/commit/0e954f08))

[Full change log...](https://github.com/mpaperno/TouchPortal-CS-API/compare/v1.47.0.0...v1.47.1.0)

---
## v1.47.0 (25-Nov-24)
Updates for Touch Portal API versions 7-10.  ([@69beeb19](https://github.com/mpaperno/TouchPortal-CS-API/commit/69beeb19))
* Added `TriggerEvent(string eventId, Dictionary<string, object> states)` method & `TriggerEventCommand` type.
* Added `StateListUpdate(string stateId, string[] values)` method & `StateListUpdateCommand` type.
* Added `forceUpdate` option to `CreateState()` method and `CreateStateCommand.ForceUpdate` member.
* Added `ListChangeEvent.Values` property for `listChangeEvent` incoming message. This is a `Dictionary<string, string>` type.
* Added `InfoEvent.CurrentPagePathMainDevice` and `InfoIevent.CurrentPagePathSecondaryDevices` properties for `info` incoming message type.
* Added `PreviousPageName`, `DeviceIP`, `DeviceName`, and `DeviceID` to `BroadcastEvent` incoming message type.
* Added & improved some documentation comments.

---
## v1.46.1 (16-Nov-24)
* Updated for .NET8 and dropped .NET5 support.
* Updated dependencies to 8.x versions (also fixes a security issue in the `System.Text.Json` library).

---
## v1.46.0 (4-Aug-23)
* Optimized TP message output by skipping a byte array copy step for appending the terminating newline after each JSON struct.
* Published .NET6 and .NET7 builds.

---
## v1.45.0 (18-Jul-22)
Both changes affect the feature which parses the "Long" connector ID from the short ID notification events into individual key/value fields (see notes for v0.43.0-mp below).
* Connector data key names are now truncated if `TouchPortalOptions.ActionDataIdSeparator` is set, just like action/connector data keys.
* Fix that the "pc_plugin-name_" part wasn't properly stripped from the "actual" connector ID.

---
## v1.44.0 (6-Jul-22)
* Add TP API v6 `parentGroup` parameter for dynamic state creation.
* Add static `TouchPortalOptions.ValidateCommandParameters` setting to bypass all parameter validation when creating new Command types.

---
## v0.43.0-mp (17-Apr-22)
* BREAKING: Convert the action/connector `Data` type into a dictionary of id=value pairs. See the [SamplePllugin.cs changes on this commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/8a918b5a)
* `TouchPortalClient` now sends the `OnCloseEvent()` to the plugin _before_ disconnecting, so the plugin can send any final data updates, etc (unless TP just crashed or quit, of course).
*  Add static `TouchPortalOptions.ActionDataIdSeparator` option to split action data IDs on a character and only store the last part in the dictionary key
  (eg. for IDs like `<plugin>.<category>.<action>.<data1>` one could split on the period and have much shorter/simpler key lookups).
  [@89a81c42](https://github.com/mpaperno/TouchPortal-CS-API/commit/89a81c42)
* `ShortConnectorIdNotificationEvent.Data` property will parse all the `|setting1=testvalue|setting2=anothervalue` action data pairs from the long `connectorId` string into a `Dictionary<string, string>`.
  Also the `ActualConnectorId` property is available to get the _actual_ connector ID part (before any data=value pairs).
  [@2238db9d](https://github.com/mpaperno/TouchPortal-CS-API/commit/2238db9d)

---
## v0.40.0-mp (3-Mar-22)
Changes since v0.30.0-beta (original @oddbear version [@1f431d05](https://github.com/oddbear/TouchPortalSDK/commit/1f431d05))

### Performance and efficiency improvements:  ([@63e886c0](https://github.com/mpaperno/TouchPortal-CS-API/commit/63e886c0))
* Does not crash on shutdown. Your plugin code can clean up and shut down properly.
* Benchmarked at around 30-50% better performance/throughput in several areas like JSON de-serialization and actual socket efficiency (removes 2 layers of buffers and reads/writes UTF8 JSON bytes directly).
* Log verbosity at the Info level greatly reduced. Logging in general improved and made more consistent, especially at Debug level.

### New Features
* Added `ConnectorChangeEvent.Data` property to get Connector data members (structure is identical to Actions).
  `ConnectorChangeEvent` and `ActionEvent` now have a common base class `DataContainerEventBase`.
  [commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/ca778ccfdde2ee624198b70abffa356839ead350)
* Added `OnShortConnectorIdNotification` event.  [commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/0fbc8e66)
* Added `ConnectorUpdateShort()` command to send connector updates based on their `shortId` (from the above notification event).
  [commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/0610921d5bc1539638e02a004243bb54a21ee23e)
  [sample](https://github.com/mpaperno/TouchPortal-CS-API/blob/mp/main/TouchPortalSDK.Sample/SamplePlugin.cs#L228)
* Added `TouchPortalClient.IsConnected` property.

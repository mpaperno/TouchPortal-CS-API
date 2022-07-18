# Touch Portal C# API
[![Nuget](https://img.shields.io/nuget/v/TouchPortal-CS-API)](https://www.nuget.org/packages/TouchPortal-CS-API)
[![.NET](https://github.com/mpaperno/TouchPortal-CS-API/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mpaperno/TouchPortal-CS-API/actions/workflows/dotnet.yml) 
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Originally a fork of [https://github.com/oddbear/TouchPortalSDK](https://github.com/oddbear/TouchPortalSDK), optimized for performance, usability, and good behavior (eg. not crashing on exit).
See further details below.

----------------------

## Touch Portal API for making plugins with C#

Built based on documentation at [Touch Portal Plugin API](https://www.touch-portal.com/api/).

### Getting started:

The simplest way of getting started, is to implement the `ITouchPortalEventHandler` and use `TouchPortalFactory` to create a client.
And then Connect to Touch Portal before sending or receiving events.

```csharp
public class SamplePlugin : ITouchPortalEventHandler
{
    public string PluginId => "Plugin.Id"; //Replace "Plugin.Id" with your unique id.

    private readonly ITouchPortalClient _client;

    public SamplePlugin()
    {
        _client = TouchPortalFactory.CreateClient(this);
    }

    public void Run()
    {
        _client.Connect();
    }
    ...
```

More information on the original [Wiki](https://github.com/oddbear/TouchPortalSDK/wiki), 
or see the [Sample](https://github.com/mpaperno/TouchPortal-CS-API/blob/mp/main/TouchPortalSDK.Sample/SamplePlugin.cs) project in this repository.

### Compatibility With Original

Drop-in replacement for `oddbear's` TouchPortalSDK as of his v0.30.0-beta2, **except**:
* The `ActionEvent.Data` property, which was an array or key-value pairs, is now a `Dictionary<string, string>`
with each data ID mapped to its corresponding value. 
See the [SamplePllugin.cs changes on this commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/8a918b5ad1e82f01b459c233447465a9c6157de0#diff-cb35f57a6de34300ca9fce15af2bada215b8c92a45456f671b02b78923a5b083)
for how to update (but now you can also `message.Data.TryGetValue("myDataId", out string value)`, for example).

Since `oddbear's` TouchPortalSDK v 0.30.0 release version, the paths have diverged further, most notably in the handling of TP Connectors.

### New Features & Change Log

#### v 1.45.0
Both changes affect the feature which parses the "Long" connector ID from the short ID notification events into individual key/value fields (see notes for v0.43.0-mp below).
* Connector data key names are now truncated if `TouchPortalOptions.ActionDataIdSeparator` is set, just like action/connector data keys.
* Fix that the "pc_plugin-name_" part wasn't properly stripped from the "actual" connector ID.

#### v 1.44.0
* Add TP API v6 `parentGroup` parameter for dynamic state creation.
* Add static `TouchPortalOptions.ValidateCommandParameters` setting to bypass all parameter validation when creating new Command types.

#### v0.43.0-mp
* Add `ConnectorChangeEvent.Data` property to get Connector data members (structure is identical to Actions). 
  `ConnectorChangeEvent` and `ActionEvent` now have a common base class `DataContainerEventBase`.
  [commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/ca778ccfdde2ee624198b70abffa356839ead350)
* Add `ShortConnectorIdNotification` event.  [commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/0fbc8e6650b06d0fc2889f13955fe7012bbde34d)
* Add `ConnectorUpdateShort()` command to send connector updates based on their `shortId` (from the above notification event).  
  [commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/0610921d5bc1539638e02a004243bb54a21ee23e) 
  [sample](https://github.com/mpaperno/TouchPortal-CS-API/blob/mp/main/TouchPortalSDK.Sample/SamplePlugin.cs#L228)
* Add `TouchPortalClient.IsConnected` property.
* `TouchPortalClient` now sends the `OnCloseEvent()` to the plugin _before_ disconnecting, so the plugin can send any final data updates, etc (unless TP just crashed or quit, of course).
* `ShortConnectorIdNotificationEvent.Data` property will parse all the `|setting1=testvalue|setting2=anothervalue` action data pairs from the long `connectorId` string into a `Dictionary<string, string>`.
  Also the `ActualConnectorId` property is available to get the _actual_ connector ID part (before any data=value pairs).  
  [commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/2238db9db4c0d4463be5822966c9e8382aa4102c)
*  Add static `TouchPortalOptions.ActionDataIdSeparator` option to split action data IDs on a character and only store the last part in the dictionary key
  (eg. for IDs like `<plugin>.<category>.<action>.<data1>` one could split on the period and have much shorter/simpler key lookups).
  [commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/89a81c42a68f75d6d4ea35b2d31ae83aaa8568f1)

#### Some other changes:
* Does not crash on shutdown. Your plugin code can clean up and shut down properly.
* Benchmarked at around 30-50% better performance/throughput in several areas like JSON de-serialization and actual socket efficiency (removes 2 layers of buffers and reads/writes UTF8 JSON bytes directly).
* Log verbosity at the Info level greatly reduced. Logging in general improved and made more consistent, especially at Debug level.

### Plugins Using This API

Working examples at:

* https://github.com/mpaperno/MSFSTouchPortalPlugin
* https://github.com/mpaperno/TJoy

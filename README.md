# Touch Portal C# and .NET API
[![Made for Touch Portal](https://img.shields.io/static/v1?style=flat&labelColor=5884b3&color=black&label=made%20for&message=Touch%20Portal%20API%20v10&logo=data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAetJREFUeNp0UruqWlEQXUePb1HERi18gShYWVqJYGeXgF+Qzh9IGh8QiOmECIYkpRY21pZWFnZaqWBhUG4KjWih4msys8FLbrhZMOfsx6w1e9beWjAYBOMtx0eOGBEZzuczrtcreAyTyQSz2QxN04j3f3J84vim8+cNR4s3rKfTSUQQi8UQjUYlGYvFAtPpVIQ0u90eZrGvnHLXuOKcB1GpkkqlUCqVEA6HsVqt4HA4EAgEMJvNUC6XMRwOwWTRfhIi3e93WK1W1Go1dbTBYIDj8YhOp4NIJIJGo4FEIoF8Po/JZAKLxQIIUSIUChGrEy9Sr9cjQTKZJJvNRtlsVs3r9Tq53W6Vb+Cy0rQyQtd1OJ1O9b/dbpCTyHoul1O9z+dzGI1Gla7jFUiyGBWPx9FsNpHJZNBqtdDtdlXfAv3vZLmCB6SiJIlJhUIB/X7/cS0viXI8n8+nrBcRIblcLlSrVez3e4jrD6LsK3O8Xi8Vi0ViJ4nVid2kB3a7HY3HY2q325ROp8nv94s5d0XkSsR90OFwoOVySaPRiF6DiHs8nmdXn+QInIxKpaJclWe4Xq9fxGazAQvDYBAKfssDeMeD7zITc1gR/4M8isvlIn2+F3N+cIjMB76j4Ha7fb7bf8H7v5j0hYef/wgwAKl+FUPYXaLjAAAAAElFTkSuQmCC)](https://www.touch-portal.com/)
[![Nuget](https://img.shields.io/nuget/v/TouchPortal-CS-API)](https://www.nuget.org/packages/TouchPortal-CS-API)
[![.NET](https://github.com/mpaperno/TouchPortal-CS-API/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mpaperno/TouchPortal-CS-API/actions/workflows/dotnet.yml) 
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Discord](https://img.shields.io/static/v1?style=flat&color=7289DA&&labelColor=7289DA&message=Discord%20Chat&label=&logo=discord&logoColor=white)](https://discord.gg/hMU9tjCG2s)

----------------------

## Touch Portal API for making plugins with .NET

Built based on documentation at [Touch Portal Plugin API](https://www.touch-portal.com/api/).

Current Touch Portal API support level: **v10.0** (Touch Portal **v4.3**)

Originally a fork of [https://github.com/oddbear/TouchPortalSDK](https://github.com/oddbear/TouchPortalSDK), optimized for performance, usability, and good behavior.

### Download / Install

- The API is available via a [nuget package](https://www.nuget.org/packages/TouchPortal-CS-API) along with a separate debug symbols package.
- An equivalent .zip archive is also available for download in [Releases](https://github.com/mpaperno/TouchPortal-CS-API/releases). This includes debug symbols in external .pdb files.
- Pre-built libraries are available for .NET versions 6, 7, and 8 via both delivery methods and include generated documentation XML.

### Getting started:

The simplest way of getting started, is to implement 
[`ITouchPortalEventHandler`](https://github.com/mpaperno/TouchPortal-CS-API/blob/main/TouchPortalSDK/ITouchPortalEventHandler.cs) 
and use [`TouchPortalFactory`](https://github.com/mpaperno/TouchPortal-CS-API/blob/main/TouchPortalSDK/TouchPortalFactory.cs) to create an instance of 
[`TouchPortalClient`](https://github.com/mpaperno/TouchPortal-CS-API/blob/main/TouchPortalSDK/Clients/TouchPortalClient.cs).
Then `TouchPortalClient.Connect()` to Touch Portal before sending or receiving events.

```csharp
public class SamplePlugin : ITouchPortalEventHandler
{
    // Replace "Plugin.Id" with your unique id.
    public string PluginId => "Plugin.Id";  

    private readonly ITouchPortalClient _client;
    private readonly ILogger<SamplePlugin> _logger;

    public SamplePlugin()
    {
        _client = TouchPortalFactory.CreateClient(this);
        _logger = new Logger<SamplePlugin>(default);
    }

    public void Run()
    {
        // Connect to Touch Portal on startup.
        _client.Connect();
    }
    
    // Event received when plugin connects to Touch Portal.
    public void OnInfoEvent(InfoEvent message)
    {
        _logger.LogInformation(
          "[InfoEvent] VersionCode: '{TpVersionCode}', VersionString: '{TpVersionString}', SDK: '{SdkVersion}', PluginVersion: '{PluginVersion}', Status: '{Status}'",
          message.TpVersionCode, message.TpVersionString, message.SdkVersion, message.PluginVersion, message.Status
        );
        
        // Update a static state defined in entry.tp
        _client.StateUpdate($"{PluginId}.staticState1", "Connected!");

        // Add a dynamic state
        _client.CreateState($"{PluginId}.dynamicState1", "Test dynamic state 1", "Test 123");
    }

    // Event triggered when one of this plugin's actions, defined in entry.tp, is triggered.
    public void OnActionEvent(ActionEvent message) {
        _logger.LogInformation("{@message}", message);_
        // Handle the action....
    }
    // ...
}
```

More complete example in the [Sample project](https://github.com/mpaperno/TouchPortal-CS-API/blob/main/TouchPortalSDK.Sample/SamplePlugin.cs) of this repository.
For more documentation see the original [Wiki](https://github.com/oddbear/TouchPortalSDK/wiki).

### Compatibility With Original

Drop-in replacement for `oddbear`'s TouchPortalSDK as of his v0.30.0-beta2, **except**:
* The `ActionEvent.Data` property, which was an array or key-value pairs, is now a `Dictionary<string, string>`
with each data ID mapped to its corresponding value. 
See the [SamplePllugin.cs changes on this commit](https://github.com/mpaperno/TouchPortal-CS-API/commit/8a918b5ad1e82f01b459c233447465a9c6157de0#diff-cb35f57a6de34300ca9fce15af2bada215b8c92a45456f671b02b78923a5b083)
for how to update (but now you can also `message.Data.TryGetValue("myDataId", out string value)`, for example).

Since `oddbear`'s TouchPortalSDK v 0.30.0 release version, the paths have diverged further, most notably in the handling of TP Connectors.

### Change Log

See [CHANGELOG.md](https://github.com/mpaperno/TouchPortal-CS-API/blob/main/CHANGELOG.md).

### Plugins Using This API

Working examples at:

* https://github.com/mpaperno/MSFSTouchPortalPlugin
* https://github.com/mpaperno/TJoy

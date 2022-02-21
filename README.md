# Touch Portal .Net SDK
[![.NET](https://github.com/mpaperno/TouchPortalSDK/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mpaperno/TouchPortalSDK/actions/workflows/dotnet.yml) 
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Please note you're on the main branch for this fork (version). This README is specific to this version and repository. The `master` branch will stay synced with the original.

----------------------

## Touch Portal API for making plugins in .Net

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

More information on the [Wiki](https://github.com/oddbear/TouchPortalSDK/wiki), or see the Sample project in this repository.

### [Work in progress] Extensions

Repository: [TouchPortalSDK.Extensions](https://github.com/oddbear/TouchPortalSDK.Extensions)

The TouchPortalSDK is ment to be the basic simple SDK, but there is an extended version aim to be like the [Java SDK](https://github.com/ChristopheCVB/TouchPortalPluginSDK).

* Automatic generation of `entry.tp`
* Automatic generation of `.tpp` package file.

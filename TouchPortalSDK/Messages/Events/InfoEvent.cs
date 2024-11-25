using System.Collections.Generic;
using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Models;

namespace TouchPortalSDK.Messages.Events
{
    public class InfoEvent : ITouchPortalMessage
    {
        /// <summary>
        /// Event from Touch Portal when a connection is established.
        /// This event includes information about the Touch Portal service.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Status ex. "paired"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Version of the SDK this version of Touch Portal knows about.
        /// Ex. 2
        /// </summary>
        public int SdkVersion { get; set; }

        /// <summary>
        /// Touch Portal version as string.
        /// Major, Minor, Patch: M.m.ppp
        /// </summary>
        public string TpVersionString { get; set; }

        /// <summary>
        /// Touch Portal version as int.
        /// Format: Major * 10000 + Minor * 1000 + patch.
        /// </summary>
        public int TpVersionCode { get; set; }

        /// <summary>
        /// Plugin version as code.
        /// </summary>
        public int PluginVersion { get; set; }

        /// <summary>
        /// Values in settings.
        /// </summary>
        public IReadOnlyCollection<Setting> Settings { get; set; }

        /// <summary>
        /// relative path of the page including extension
        /// Since TP API v9.
        /// </summary>
        public string CurrentPagePathMainDevice { get; set; } = null;

        /// <summary>
        /// Since TP API v9.
        /// </summary>
        public IEnumerable<SecondaryDeviceInformation> CurrentPagePathSecondaryDevices { get; set; } = null;

        /// <inheritdoc cref="ITouchPortalMessage" />
        public Identifier GetIdentifier()
            => new Identifier(Type, default, default);
    }

    /// <summary>
    /// `InfoEvent.currentPagePathSecondaryDevices` array member object.
    /// </summary>
    public sealed class SecondaryDeviceInformation
    {
        /// <summary>
        /// eg. "Sim 1.0"
        /// </summary>
        public string TpDeviceId { get; set; }

        /// <summary>
        /// eg. "\\main.tml"
        /// </summary>
        public string CurrentPagePath { get; set; }

        /// <summary>
        /// eg. "iPhone 15"
        /// </summary>
        public string DeviceName { get; set; }
    }
}

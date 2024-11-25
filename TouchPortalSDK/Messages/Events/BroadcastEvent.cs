using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Models;

namespace TouchPortalSDK.Messages.Events
{
    /// <summary>
    /// Touch Portal will send messages to the plug-in at certain events. Currently the only message that is broadcast is the page change event. You can use this broadcast for example to resend states whenever a page is loaded.
    /// </summary>
    public class BroadcastEvent : ITouchPortalMessage
    {

        /// <inheritdoc cref="ITouchPortalMessage.Type" />
        public string Type { get; set; }

        /// <summary>
        /// The type of broadcast event that is triggered. Currently only the "pageChange" is supported.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Name of the page the device is currently on. Ex. "(main)"
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// The name of the page navigated from.
        /// Since TP API v8.
        /// </summary>
        public string PreviousPageName { get; set; }

        /// <summary>
        /// The IP address of the device navigating pages.
        /// Since TP API v8.
        /// </summary>
        public string DeviceIP { get; set; }

        /// <summary>
        /// The Device Name of the device navigating pages.
        /// Since TP API v8.
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// The device id (set for multiple devices upgrade) of the device navigating pages.
        /// Since TP API v9.
        /// </summary>
        public string DeviceId { get; set; }

        /// <inheritdoc cref="ITouchPortalMessage" />
        public Identifier GetIdentifier()
            => new Identifier(Type, PageName, default);
        }
}

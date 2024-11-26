using TouchPortalSDK.Messages.Events;

namespace TouchPortalSDK
{
    /// <summary>
    /// Interface used to register a plugin that can handle events from Touch Portal.
    /// Implementation of all methods is optional.
    /// </summary>
    public interface ITouchPortalEventHandler
    {
        /// <summary>
        /// EventHandler must define a pluginId to receive plugin events.
        /// </summary>
        public string PluginId { get; }

        /// <summary>
        /// Method to call when Touch Portal is connected.
        /// </summary>
        void OnInfoEvent(InfoEvent message) { /* empty default implementation */ }

        /// <summary>
        /// Method to call when an item is selected from dropdown in Action Creation of a button.
        /// </summary>
        void OnListChangedEvent(ListChangeEvent message) { /* empty default implementation */ }

        /// <summary>
        /// Method is called when an broadcast message is sent.
        /// </summary>
        void OnBroadcastEvent(BroadcastEvent message) { /* empty default implementation */ }

        /// <summary>
        /// Settings are first received as a part of the OnInfoEvent.
        /// Then updated through this event if either user changes a setting in Touch Portal, or the SettingUpdate is successfully triggered.
        /// </summary>
        void OnSettingsEvent(SettingsEvent message) { /* empty default implementation */ }

        /// <summary>
        /// Method to call when a user presses a button on their device.
        /// </summary>
        void OnActionEvent(ActionEvent message) { /* empty default implementation */ }

        /// <summary>
        /// Called when a user clicks on a notification option.
        /// </summary>
        void OnNotificationOptionClickedEvent(NotificationOptionClickedEvent message) { /* empty default implementation */ }

        /// <summary>
        /// Method to call when a user moves a slider on their device.
        /// </summary>
        void OnConnecterChangeEvent(ConnectorChangeEvent message) { /* empty default implementation */ }

        /// <summary>
        /// Called when TP reports a new instance of your connector, either at startup or if user adds/modifies one.
        /// </summary>
        void OnShortConnectorIdNotificationEvent(ShortConnectorIdNotificationEvent message) { /* empty default implementation */ }

        /// <summary>
        /// Method to call when we loose connection to Touch Portal.
        /// </summary>
        /// <param name="message">A reason for Touch Portal sending the close event.</param>
        void OnClosedEvent(string message) { /* empty default implementation */ }

        /// <summary>
        /// Messages that are unknown, and therefor we cannot deserialize to a known type.
        /// </summary>
        /// <param name="jsonMessage">The unknown message in JSON format.</param>
        void OnUnhandledEvent(string jsonMessage) { /* empty default implementation */ }
    }
}

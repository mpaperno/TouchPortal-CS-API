using TouchPortalSDK.Messages.Models;
using TouchPortalSDK.Messages.Models.Enums;

namespace TouchPortalSDK.Interfaces
{
    public interface ICommandHandler
    {
        /// <summary>
        /// Send a custom command to Touch Portal. The message must be properly encoded JSON format.
        /// </summary>
        /// <param name="message">A JSON message to send. A terminating newline will be added automatically when the message is sent.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool SendMessage(string message);

        /// <summary>
        /// Send a Command directly to Touch Portal. All the other command sending methods are conveniences for this one.
        /// </summary>
        /// <param name="command">One of `TouchPortalSDK.Messages.Commands` types.</param>
        /// <param name="callerMemberName">Name of the calling method. By default this is determined automatically at compile time.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool SendCommand<TCommand>(TCommand  command, [System.Runtime.CompilerServices.CallerMemberName]string callerMemberName = "")
            where TCommand : ITouchPortalMessage;

        /// <summary>
        /// Creates a dynamic state in Touch Portal Memory.
        /// This state will disappear when restarting Touch Portal.
        /// You will need to persist them yourself and reload them on plugin load.
        /// </summary>
        /// <param name="stateId">A unique ID for the new state.</param>
        /// <param name="desc">Description of the created state (name in menus).</param>
        /// <param name="defaultValue">Default value of this state, default is empty string.</param>
        /// <param name="parentGroup">Parent group of this state (TP API v6). Default is an empty string.</param>
        /// <param name="forceUpate">
        ///   This will force the update of the state if it is already created or existing and will trigger the state
        ///   changed event even if the value is the same as the already existing one.
        ///   Since TP API v7.
        /// </param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool CreateState(string stateId, string desc, string defaultValue = "", string parentGroup = "", bool forceUpate = false);

        /// <summary>
        /// Updates a setting in Touch Portal.
        /// </summary>
        /// <param name="name">Setting name</param>
        /// <param name="value">New value for the setting</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool SettingUpdate(string name, string value = "");

        /// <summary>
        /// Removes the dynamic state from Touch Portal.
        /// </summary>
        /// <param name="stateId">ID of the state to remove.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool RemoveState(string stateId);

        /// <summary>
        /// Value that can be displayed, or an event can trigger on.
        /// Values are not persisted, and will fall back to default value on restart.
        /// - Plugin: Defined in the Entry.tp
        /// - Dynamic: Created or removed at runtime. (in memory only)
        /// - Global: Defined in the Touch Portal UI. (state definition persisted in %AppData%\TouchPortal\states.tp)
        /// </summary>
        /// <param name="stateId">ID of the state to update.</param>
        /// <param name="value">The new value to set for the state. Must be a string.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool StateUpdate(string stateId, string value = "");

        /// <summary>
        /// Updates the drop down choices in the Touch Portal UI.
        /// InstanceId can be used to dynamically update a dropdown based on the value chosen from another dropdown.
        /// </summary>
        /// <param name="choiceId">Id of UI dropdown.</param>
        /// <param name="values">Values as string array that you can choose from.</param>
        /// <param name="instanceId">if set (fetched from listChange event), this will only update this particular list.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool ChoiceUpdate(string choiceId, string[] values, string instanceId = default);

        /// <summary>
        /// Updates the constraints of a data value.
        /// </summary>
        /// <param name="dataId">Id of action the number box.</param>
        /// <param name="minValue">Min value the field can be.</param>
        /// <param name="maxValue">Max value the field can be.</param>
        /// <param name="dataType">Type of the data field.</param>
        /// <param name="instanceId">if set (fetched from listChange event), this will only update this particular list.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool UpdateActionData(string dataId, double minValue, double maxValue, ActionDataType dataType, string instanceId = default);

        /// <summary>
        /// Adds a notification to the Touch Portal UI. Ex. if the plugin has a updated version.
        /// </summary>
        /// <param name="notificationId">If of the notification.</param>
        /// <param name="title">Title on the notification shown to the user.</param>
        /// <param name="message">Text / description of the notification shown to the user.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool ShowNotification(string notificationId, string title, string message, NotificationOptions[] notificationOptions);

        /// <summary>
        /// Sends a connector value update to Touch Portal using the long form of the connector ID.
        /// </summary>
        /// <param name="connectorId">The long ID of the connector to update. The string "pc_{pluginId}_" is automatically prepended
        /// before sending to TP. The total length must not exceed 200 chars.</param>
        /// <param name="value">The value to send, must be between 0 and 100, inclusive.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool ConnectorUpdate(string connectorId, int value);

        /// <summary>
        /// Sends a connector value update to Touch Portal using the short form of the connector ID.
        /// </summary>
        /// <param name="shortId">The short ID of the connector to update. This is obtained from a <see cref="ShortConnectorIdNotification"/> event.</param>
        /// <param name="value">The value to send, must be between 0 and 100, inclusive.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool ConnectorUpdateShort(string shortId, int value);

        /// <summary>
        /// Trigger predefined Events by sending a message to Touch Portal with the given eventId and additional data.
        /// Since TP API v7.
        /// </summary>
        /// <param name="eventId">The event id to trigger. This event must be defined in the plugin's entry.tp declaration.</param>
        /// <param name="states">This gets serialized to a JSON Object that holds key value pairs of data that are used within Touch Portal as Local States.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool TriggerEvent(string eventId, System.Collections.Generic.Dictionary<string,string> states = null);
        /// <summary>
        /// Trigger predefined Events by sending a message to Touch Portal with the given eventId and additional data. This is an overloaded function.
        /// Since TP API v7.
        /// </summary>
        /// <param name="eventId">The event id to trigger. This event must be defined in the plugin's entry.tp declaration.</param>
        /// <param name="states">This gets serialized to a JSON Object that holds key value pairs of data that are used within Touch Portal as Local States.</param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool TriggerEvent(string eventId, System.Collections.Generic.IReadOnlyDictionary<string,string> states);

        /// <summary>
        /// You can also update state lists in Touch Portal. These state lists needs to be defined in the entry file.
        /// Since TP API v7.
        /// </summary>
        /// <param name="stateId">The state id to set/update.</param>
        /// <param name="values">The collection of texts that should be the new list to display for this given choice list id. </param>
        /// <returns>true on successful sending of the message, false otherwise.</returns>
        bool StateListUpdate(string stateId, string[] values);

    }
}

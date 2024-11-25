using System;
using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Models;

namespace TouchPortalSDK.Messages.Commands
{
    /// <summary>
    /// States can be created on runtime using by sending a "createState" message to Touch Portal with the given information.
    /// </summary>
    public class CreateStateCommand : ITouchPortalMessage
    {
        public string Type => "createState";

        /// <summary>
        /// The id of the newly created plug-in state. Please ensure unique names, otherwise you may corrupt other plug-ins.
        /// </summary>
        public string Id { get; set; }

        /// <summary> The displayed name within Touch Portal which represents the state. </summary>
        public string Desc { get; set; }

        /// <summary> The default value the state will have on creation. </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// The name of the parent group of this state. The parent group of this state will be used to group the state in
        /// the menus used throughout Touch Portal. Every state belonging to the same parent group name will be in the same selection menu.
        /// Since TP API v6.
        /// </summary>
        public string ParentGroup { get; set; }

        /// <summary>
        /// This will force the update of the state if it is already created or existing and will trigger the state
        /// changed event even if the value is the same as the already existing one.
        /// Since TP API v7.
        /// </summary>
        public bool ForceUpdate { get; set; } = false;

        public CreateStateCommand(
          string stateId,
          string desc,
          string defaultValue = "",
          string parentGroup = "",
          bool forceUpdate = false
        ) {
            if (TouchPortalOptions.ValidateCommandParameters) {
                if (string.IsNullOrWhiteSpace(stateId))
                    throw new ArgumentNullException(nameof(stateId));

                if (string.IsNullOrWhiteSpace(desc))
                    throw new ArgumentNullException(nameof(desc));
            }
            Id = stateId;
            Desc = desc;
            DefaultValue = defaultValue ?? string.Empty;
            ParentGroup = parentGroup ?? string.Empty;
            ForceUpdate = forceUpdate;
        }

        /// <inheritdoc cref="ITouchPortalMessage" />
        public Identifier GetIdentifier()
            => new Identifier(Type, Id, default);
    }
}

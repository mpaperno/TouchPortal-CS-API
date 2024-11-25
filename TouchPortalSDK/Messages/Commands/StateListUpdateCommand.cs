using System;
using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Models;

namespace TouchPortalSDK.Messages.Commands
{
    /// <summary>
    /// You can also update state lists in Touch Portal. These state lists needs to be defined in the entry file.
    /// Since TP API v7.
    /// </summary>
    public class StateListUpdateCommand : ITouchPortalMessage
    {

        /// <inheritdoc cref="ITouchPortalMessage" />
        public string Type => "stateListUpdate";

        /// <summary> The state id to set/update. </summary>
        public string Id { get; set; }

        /// <summary>The collection of texts that should be the new list to display for this given choice list id. </summary>
        public string[] Value { get; set; }

        public StateListUpdateCommand(string stateId, string[] value)
        {
            if (TouchPortalOptions.ValidateCommandParameters && string.IsNullOrWhiteSpace(stateId))
                throw new ArgumentNullException(nameof(stateId));

            Id = stateId;
            Value = value ?? Array.Empty<string>();
        }

        /// <inheritdoc cref="ITouchPortalMessage" />
        public Identifier GetIdentifier()
            => new Identifier(Type, Id, default);
    }
}

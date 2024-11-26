using System;
using System.Collections.Generic;
using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Models;
using TouchPortalSDK.Messages.Models.Enums;

namespace TouchPortalSDK.Messages.Commands
{
  /// <summary>
  /// You can trigger predefined Events by sending a message to Touch Portal with the given eventId and additional data.
  /// Since: TP API v7
  /// </summary>
  public class TriggerEventCommand : ITouchPortalMessage
  {
    /// <inheritdoc cref="ITouchPortalMessage.Type" />
    public string Type => "triggerEvent";

    /// <summary> The event id to trigger. This event must be defined in the plugin's entry.tp declaration.</summary>
    public string EventId { get; set; }
    /// <summary> This gets serialized to a JSON Object that holds key value pairs of data that are used within Touch Portal as Local States. Optional.</summary>
    public TriggerEventStates States { get; set; } = null;

    /// <summary> Construct a new TriggerEventCommand. </summary>
    /// <param name="eventId">The event id to trigger.</param>
    /// <param name="states">This optional argument gets serialized to a JSON Object that holds key value pairs of data that are used within Touch Portal as Local States.</param>
    /// <exception cref="ArgumentNullException">If eventId is null or empty and `TouchPortalOptions.ValidateCommandParameters` is enabled.</exception>
    public TriggerEventCommand(string eventId, TriggerEventStates states = null)
    {
      if (TouchPortalOptions.ValidateCommandParameters && string.IsNullOrWhiteSpace(eventId))
        throw new ArgumentNullException(nameof(eventId));

      EventId = eventId;
      States = states;
    }

    /// <inheritdoc cref="ITouchPortalMessage.GetIdentifier" />
    public Identifier GetIdentifier()
        => new Identifier(Type, EventId, default);
  }


  /// <summary>
  /// This is a `Dictionary&lt;string, string&gt;` typedef for usage in TriggerEventCommand.States message value.
  /// </summary>
  public sealed class TriggerEventStates : System.Collections.Generic.Dictionary<string, string>
  { }

}

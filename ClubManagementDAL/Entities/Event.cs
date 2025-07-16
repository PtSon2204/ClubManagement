using System;
using System.Collections.Generic;

namespace ClubManagement.DAL.Entities;

public partial class Event
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime EventDate { get; set; }

    public string Location { get; set; } = null!;

    public int? ClubId { get; set; }

    public virtual Club? Club { get; set; }

    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
}

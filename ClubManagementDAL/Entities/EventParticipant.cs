using System;
using System.Collections.Generic;

namespace ClubManagement.DAL.Entities;

public partial class EventParticipant
{
    public int EventParticipantId { get; set; }

    public int? EventId { get; set; }

    public int? UserId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Event? Event { get; set; }

    public virtual User? User { get; set; }
}

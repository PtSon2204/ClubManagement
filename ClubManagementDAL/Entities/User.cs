using System;
using System.Collections.Generic;

namespace ClubManagement.DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int? ClubId { get; set; }

    public virtual Club? Club { get; set; }

    public virtual ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
}

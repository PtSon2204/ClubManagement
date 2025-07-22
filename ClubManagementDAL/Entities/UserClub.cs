using System;
using System.Collections.Generic;

namespace ClubManagement.DAL.Entities;

public partial class UserClub
{
    public int UserClubId { get; set; }

    public int? UserId { get; set; }

    public int? ClubId { get; set; }

    public string Role { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Club? Club { get; set; }

    public virtual User? User { get; set; }
}

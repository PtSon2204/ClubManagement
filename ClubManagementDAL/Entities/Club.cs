using System;
using System.Collections.Generic;

namespace ClubManagement.DAL.Entities;

public partial class Club
{
    public int ClubId { get; set; }

    public string ClubName { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? EstablishedDate { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<UserClub> UserClubs { get; set; } = new List<UserClub>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

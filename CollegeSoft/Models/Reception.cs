using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class Reception
{
    public int Rid { get; set; }

    public string PersonName { get; set; } = null!;

    public DateTime EntryDate { get; set; }

    public string EntryTime { get; set; } = null!;

    public string Purpose { get; set; } = null!;

    public string PersonAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime CancelledDate { get; set; }

    public int CancelledById { get; set; }

    public string FiscalYear { get; set; } = null!;

    public string? ReceptionStatus { get; set; }

    public virtual User CancelledBy { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

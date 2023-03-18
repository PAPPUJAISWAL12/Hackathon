using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class Reception:ReceptionEdit
{
 

    public virtual User CancelledBy { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
public partial class ReceptionEdit
{
	public int Rid { get; set; }

	public string? PersonName { get; set; }

	public DateTime EntryDate { get; set; }

	public string? EntryTime { get; set; }

	public string? Purpose { get; set; }

	public string? PersonAddress { get; set; }

	public string? Phone { get; set; }

	public int UserId { get; set; }

	public DateTime CancelledDate { get; set; }

	public int CancelledById { get; set; }

	public string? FiscalYear { get; set; }

	public string? ReceptionStatus { get; set; }
}
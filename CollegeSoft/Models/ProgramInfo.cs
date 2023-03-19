using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class ProgramInfo:ProgramInfoEdit
{
    public virtual User? Cancelled { get; set; }

    public virtual User? User { get; set; } 
}
public partial class ProgramInfoEdit
{

	public int Pid { get; set; }

	public string? Pname { get; set; }

	public string? Pdescription { get; set; }

	public string? Venue { get; set; }

	public DateTime StartDate { get; set; }

	public string? StartTime { get; set; }

	public DateTime EndDate { get; set; }

	public string? EndTime { get; set; }

	public int UserId { get; set; }

	public DateTime EntryDate { get; set; }

	public int? CancelledId { get; set; }

	public DateTime? CancelledDate { get; set; }

	public string? ReasonForCancell { get; set; }
}

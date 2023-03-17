using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class FeeDetailsView
{
    public int DetailId { get; set; }

    public int FeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string Cname { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public decimal MonthlyFeeAmt { get; set; }

    public decimal YearlyAmt { get; set; }

    public decimal YearlyDiscount { get; set; }

    public decimal Examfee { get; set; }

    public string FiscalYear { get; set; } = null!;

    public string? EntryUser { get; set; }

    public int? EntryBy { get; set; }

    public decimal TotalAmt { get; set; }

    public decimal PaidAmt { get; set; }

    public decimal RemainingAmt { get; set; }

    public string FeeStatus { get; set; } = null!;

    public int? PrintCount { get; set; }

    public DateTime CancelledDate { get; set; }

    public string? CancelledUser { get; set; }

    public int? CancelledBy { get; set; }

    public string ResonForCancelled { get; set; } = null!;
}

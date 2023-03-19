namespace CollegeSoft.Models
{
	public partial class FeeEdit
	{
		public int FeeId { get; set; }

		public int StdId { get; set; }

		public decimal MonthlyFeeAmt { get; set; }

		public decimal YearlyAmt { get; set; }

		public decimal YearlyDiscount { get; set; }

		public decimal Examfee { get; set; }

		public string FiscalYear { get; set; }

		public int EntryBy { get; set; }

		public DateTime EntryDate { get; set; }

		public string? EntryTime { get; set; } 

		

		


		public int DetailId { get; set; }		

		public decimal TotalAmt { get; set; }

		public decimal PaidAmt { get; set; }

		public decimal RemainingAmt { get; set; }

		public string FeeStatus { get; set; }


		public int PrintId { get; set; }

		public DateTime PrintDate { get; set; }

		public string? PrintTime { get; set; } 

		public int PrintUserId { get; set; }

		


	}
}

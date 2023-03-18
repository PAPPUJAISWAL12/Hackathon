namespace CollegeSoft.Models
{
	public partial class StudentEdit
	{
		public int UserId { get; set; }

		public string? UserEmail { get; set; }

		public string? Upassword { get; set; }

		public string? FullName { get; set; }

		public string? Phone { get; set; }

		public string? UserAddress { get; set; } 

		public bool? LoginStatus { get; set; }


		public int StdId { get; set; }

		public int Cid { get; set; }


		public int UserRoleId { get; set; }		

		public int RoleId { get; set; }

	}
}

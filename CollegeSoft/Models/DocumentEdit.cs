namespace CollegeSoft.Models
{
	public partial class DocumentEdit
	{
		public int DocId { get; set; }

		public int UserId { get; set; }

		public virtual List<UploadFile> UploadFiles { get; set; } = new List<UploadFile>();

	}
}

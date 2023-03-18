using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class Document
{
    public int DocId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<UploadFile> UploadFiles { get; set; } = new List<UploadFile>();

    public virtual User? User { get; set; }
}

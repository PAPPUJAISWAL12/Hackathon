using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class Upload
{
    public int UploadId { get; set; }

    public string DocumentType { get; set; } = null!;

    public string? Docs { get; set; }

    public virtual ICollection<Document> Documents { get; } = new List<Document>();
}

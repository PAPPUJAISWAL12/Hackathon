using System;
using System.Collections.Generic;

namespace CollegeSoft.Models;

public partial class UploadFileView
{
    public int UploadId { get; set; }

    public int DocId { get; set; }

    public int TypeId { get; set; }

    public string? DocFile { get; set; }

    public string DocumetCat { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string UserEmail { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace back_end.Model;

public partial class TblCetificate
{
    public int CetificateId { get; set; }

    public string? Cerificate { get; set; }

    public int? ArtistId { get; set; }

    public virtual TblArtist? Artist { get; set; }
}

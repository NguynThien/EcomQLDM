using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class ChiTietPx
{
    public int MaCtpx { get; set; }

    public int MaPx { get; set; }

    public int MaHh { get; set; }

    public int SoLuong { get; set; }

    public virtual HangHoa MaHhNavigation { get; set; } = null!;

    public virtual PhieuXuat MaPxNavigation { get; set; } = null!;
}

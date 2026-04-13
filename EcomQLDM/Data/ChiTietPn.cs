using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class ChiTietPn
{
    public int MaCtpn { get; set; }

    public int MaPn { get; set; }

    public int MaHh { get; set; }

    public int SoLuong { get; set; }

    public virtual HangHoa MaHhNavigation { get; set; } = null!;

    public virtual PhieuNhap MaPnNavigation { get; set; } = null!;
}

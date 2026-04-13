using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class VoHang
{
    public int MaVh { get; set; }

    public string MaKh { get; set; } = null!;

    public int MaHh { get; set; }

    public int SoLuong { get; set; }

    public int HieuLuc { get; set; }

    public virtual HangHoa MaHhNavigation { get; set; } = null!;

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}

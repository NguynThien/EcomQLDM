using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public int MaQh { get; set; }

    public int? HieuLuc { get; set; }

    public virtual QuyenHang MaQhNavigation { get; set; } = null!;
}

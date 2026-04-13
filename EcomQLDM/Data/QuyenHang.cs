using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class QuyenHang
{
    public int MaQh { get; set; }

    public string TenQh { get; set; } = null!;

    public string? ThongTin { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}

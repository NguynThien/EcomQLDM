using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class PhieuNhap
{
    public int MaPn { get; set; }

    public string MaKho { get; set; } = null!;

    public DateOnly NgayLapPhieu { get; set; }

    public DateOnly NgayNhapHang { get; set; }

    public virtual ICollection<ChiTietPn> ChiTietPns { get; set; } = new List<ChiTietPn>();

    public virtual Kho MaKhoNavigation { get; set; } = null!;
}

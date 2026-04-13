using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class PhieuXuat
{
    public int MaPx { get; set; }

    public string MaKho { get; set; } = null!;

    public DateOnly NgayLapPhieu { get; set; }

    public DateOnly NgayXuatHang { get; set; }

    public virtual ICollection<ChiTietPx> ChiTietPxes { get; set; } = new List<ChiTietPx>();

    public virtual Kho MaKhoNavigation { get; set; } = null!;
}

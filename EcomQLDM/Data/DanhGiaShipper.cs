using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class DanhGiaShipper
{
    public int MaDgs { get; set; }

    public string? TenNdg { get; set; }

    public string? Email { get; set; }

    public string? DienThoai { get; set; }

    public string MaShipper { get; set; } = null!;

    public string? NoiDung { get; set; }

    public double? DiemDanhGia { get; set; }

    public int BaoViPham { get; set; }

    public virtual Shipper MaShipperNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class Shipper
{
    public string MaShipper { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string TenShipper { get; set; } = null!;

    public string GioiTinh { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string DienThoai { get; set; } = null!;

    public int HieuLuc { get; set; }

    public int? SoDonGiao { get; set; }

    public double? DiemDanhGia { get; set; }

    public virtual ICollection<ChiTietDh> ChiTietDhs { get; set; } = new List<ChiTietDh>();

    public virtual ICollection<DanhGiaShipper> DanhGiaShippers { get; set; } = new List<DanhGiaShipper>();
}

using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class Kho
{
    public string MaKho { get; set; } = null!;

    public string TenKho { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string HoTenQuanDoc { get; set; } = null!;

    public string DienThoai { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();
}

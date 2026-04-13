using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class TrangThaiDh
{
    public int MaTrangThai { get; set; }

    public string TenTrangThai { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<ChiTietDh> ChiTietDhs { get; set; } = new List<ChiTietDh>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}

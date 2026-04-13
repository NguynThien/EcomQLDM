using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class ChiTietDh
{
    public int MaCtdh { get; set; }

    public int MaDh { get; set; }

    public int MaTrangThai { get; set; }

    public int MaHh { get; set; }

    public string MaShipper { get; set; } = null!;

    public DateTime NgayCapNhat { get; set; }

    public DateTime NgayNhanDh { get; set; }

    public string DiaChi { get; set; } = null!;

    public string? GhiChu { get; set; }

    public virtual DonHang MaDhNavigation { get; set; } = null!;

    public virtual HangHoa MaHhNavigation { get; set; } = null!;

    public virtual Shipper MaShipperNavigation { get; set; } = null!;

    public virtual TrangThaiDh MaTrangThaiNavigation { get; set; } = null!;
}

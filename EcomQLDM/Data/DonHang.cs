using System;
using System.Collections.Generic;

namespace EcomQLDM.Data;

public partial class DonHang
{
    public int MaDh { get; set; }

    public int HangHoa { get; set; }

    public string MaKh { get; set; } = null!;

    public int MaTrangThai { get; set; }

    public string DiaChiLayHang { get; set; } = null!;

    public string DiaChiGiaoHang { get; set; } = null!;

    public DateTime NgayLapDh { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietDh> ChiTietDhs { get; set; } = new List<ChiTietDh>();

    public virtual TrangThaiDh MaTrangThaiNavigation { get; set; } = null!;
}

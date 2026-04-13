using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EcomQLDM.Data;

public partial class TmdtdatabaseContext : DbContext
{
    public TmdtdatabaseContext()
    {
    }

    public TmdtdatabaseContext(DbContextOptions<TmdtdatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDh> ChiTietDhs { get; set; }

    public virtual DbSet<ChiTietHd> ChiTietHds { get; set; }

    public virtual DbSet<ChiTietPn> ChiTietPns { get; set; }

    public virtual DbSet<ChiTietPx> ChiTietPxes { get; set; }

    public virtual DbSet<DanhGiaShipper> DanhGiaShippers { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<HangHoa> HangHoas { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<Kho> Khos { get; set; }

    public virtual DbSet<Loai> Loais { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhanCong> PhanCongs { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }

    public virtual DbSet<PhongBan> PhongBans { get; set; }

    public virtual DbSet<QuyenHang> QuyenHangs { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

    public virtual DbSet<TrangThaiDh> TrangThaiDhs { get; set; }

    public virtual DbSet<VChiTietHoaDon> VChiTietHoaDons { get; set; }

    public virtual DbSet<VoHang> VoHangs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-OCN62QK;Initial Catalog=TMDTDATABASE;Persist Security Info=True;User ID=sa;Password=0973093110;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDh>(entity =>
        {
            entity.HasKey(e => e.MaCtdh).HasName("PK__ChiTietD__1E4E40F0A2A4313C");

            entity.ToTable("ChiTietDH");

            entity.Property(e => e.MaCtdh).HasColumnName("MaCTDH");
            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.MaShipper).HasMaxLength(50);
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");
            entity.Property(e => e.NgayNhanDh)
                .HasColumnType("datetime")
                .HasColumnName("NgayNhanDH");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDhs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDH__MaDH__45BE5BA9");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.ChiTietDhs)
                .HasForeignKey(d => d.MaHh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDH__MaHH__47A6A41B");

            entity.HasOne(d => d.MaShipperNavigation).WithMany(p => p.ChiTietDhs)
                .HasForeignKey(d => d.MaShipper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDH__MaShi__489AC854");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.ChiTietDhs)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDH__MaTra__46B27FE2");
        });

        modelBuilder.Entity<ChiTietHd>(entity =>
        {
            entity.HasKey(e => e.MaCt).HasName("PK_OrderDetails");

            entity.ToTable("ChiTietHD");

            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHd)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Products");
        });

        modelBuilder.Entity<ChiTietPn>(entity =>
        {
            entity.HasKey(e => e.MaCtpn).HasName("PK__ChiTietP__1E4E60758D300E77");

            entity.ToTable("ChiTietPN");

            entity.Property(e => e.MaCtpn).HasColumnName("MaCTPN");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.MaPn).HasColumnName("MaPN");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.ChiTietPns)
                .HasForeignKey(d => d.MaHh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPN__MaHH__31B762FC");

            entity.HasOne(d => d.MaPnNavigation).WithMany(p => p.ChiTietPns)
                .HasForeignKey(d => d.MaPn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPN__MaPN__30C33EC3");
        });

        modelBuilder.Entity<ChiTietPx>(entity =>
        {
            entity.HasKey(e => e.MaCtpx).HasName("PK__ChiTietP__1E4E606F6768DF94");

            entity.ToTable("ChiTietPX");

            entity.Property(e => e.MaCtpx).HasColumnName("MaCTPX");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.MaPx).HasColumnName("MaPX");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.ChiTietPxes)
                .HasForeignKey(d => d.MaHh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPX__MaHH__3587F3E0");

            entity.HasOne(d => d.MaPxNavigation).WithMany(p => p.ChiTietPxes)
                .HasForeignKey(d => d.MaPx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPX__MaPX__3493CFA7");
        });

        modelBuilder.Entity<DanhGiaShipper>(entity =>
        {
            entity.HasKey(e => e.MaDgs).HasName("PK__DanhGiaS__3D88B06B4E769276");

            entity.ToTable("DanhGiaShipper");

            entity.Property(e => e.MaDgs).HasColumnName("MaDGS");
            entity.Property(e => e.DienThoai).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.MaShipper).HasMaxLength(50);
            entity.Property(e => e.TenNdg)
                .HasMaxLength(50)
                .HasColumnName("TenNDG");

            entity.HasOne(d => d.MaShipperNavigation).WithMany(p => p.DanhGiaShippers)
                .HasForeignKey(d => d.MaShipper)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGiaSh__MaShi__40058253");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__272586619720FE63");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");
            entity.Property(e => e.NgayLapDh)
                .HasColumnType("datetime")
                .HasColumnName("NgayLapDH");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonHang__MaTrang__42E1EEFE");
        });

        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.MaHh).HasName("PK_Products");

            entity.ToTable("HangHoa");

            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.DacDiem).HasMaxLength(200);
            entity.Property(e => e.DonGia).HasDefaultValue(0.0);
            entity.Property(e => e.Hinh).HasMaxLength(200);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MoTaDonVi).HasMaxLength(500);
            entity.Property(e => e.NgaySx)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NgaySX");
            entity.Property(e => e.TenAlias).HasMaxLength(200);
            entity.Property(e => e.TenHh)
                .HasMaxLength(200)
                .HasColumnName("TenHH");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK_Orders");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.CachThanhToan)
                .HasMaxLength(50)
                .HasDefaultValue("Cash");
            entity.Property(e => e.CachVanChuyen)
                .HasMaxLength(50)
                .HasDefaultValue("Airline");
            entity.Property(e => e.DiaChi).HasMaxLength(60);
            entity.Property(e => e.GhiChu).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayCan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NgayGiao)
                .HasDefaultValueSql("(((1)/(1))/(1900))")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDon_TrangThai");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK_Customers");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(60);
            entity.Property(e => e.DienThoai).HasMaxLength(24);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Hinh)
                .HasMaxLength(50)
                .HasDefaultValue("Photo.gif");
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.NgaySinh)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RandomKey)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Kho>(entity =>
        {
            entity.HasKey(e => e.MaKho).HasName("PK__Kho__3BDA9350B7B0F9A7");

            entity.ToTable("Kho");

            entity.Property(e => e.MaKho).HasMaxLength(10);
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.DienThoai).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.HoTenQuanDoc).HasMaxLength(30);
            entity.Property(e => e.TenKho).HasMaxLength(30);
        });

        modelBuilder.Entity<Loai>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK_Categories");

            entity.ToTable("Loai");

            entity.Property(e => e.Hinh).HasMaxLength(50);
            entity.Property(e => e.TenLoai).HasMaxLength(50);
            entity.Property(e => e.TenLoaiAlias).HasMaxLength(50);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK_Suppliers");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .HasColumnName("MaNCC");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.DienThoai).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Logo).HasMaxLength(50);
            entity.Property(e => e.NguoiLienLac).HasMaxLength(50);
            entity.Property(e => e.TenCongTy).HasMaxLength(50);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70ABA82150D");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaQh).HasColumnName("MaQH");
            entity.Property(e => e.MatKhau).HasMaxLength(50);

            entity.HasOne(d => d.MaQhNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaQh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__MaQH__236943A5");
        });

        modelBuilder.Entity<PhanCong>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PhanCong");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.MaPb)
                .HasMaxLength(50)
                .HasColumnName("MaPB");
            entity.Property(e => e.MaPc)
                .ValueGeneratedOnAdd()
                .HasColumnName("MaPC");
            entity.Property(e => e.NgayPc)
                .HasColumnType("datetime")
                .HasColumnName("NgayPC");

            entity.HasOne(d => d.MaNvNavigation).WithMany()
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhanCong__MaNV__25518C17");

            entity.HasOne(d => d.MaPbNavigation).WithMany()
                .HasForeignKey(d => d.MaPb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhanCong__MaPB__2645B050");
        });

        modelBuilder.Entity<PhieuNhap>(entity =>
        {
            entity.HasKey(e => e.MaPn).HasName("PK__PhieuNha__2725E7F00D2AB64F");

            entity.ToTable("PhieuNhap");

            entity.Property(e => e.MaPn).HasColumnName("MaPN");
            entity.Property(e => e.MaKho).HasMaxLength(10);

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.MaKho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuNhap__MaKho__2B0A656D");
        });

        modelBuilder.Entity<PhieuXuat>(entity =>
        {
            entity.HasKey(e => e.MaPx).HasName("PK__PhieuXua__2725E7CAFE41E3C3");

            entity.ToTable("PhieuXuat");

            entity.Property(e => e.MaPx).HasColumnName("MaPX");
            entity.Property(e => e.MaKho).HasMaxLength(10);

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.PhieuXuats)
                .HasForeignKey(d => d.MaKho)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuXuat__MaKho__2DE6D218");
        });

        modelBuilder.Entity<PhongBan>(entity =>
        {
            entity.HasKey(e => e.MaPb).HasName("PK__PhongBan__2725E7E44CD3062F");

            entity.ToTable("PhongBan");

            entity.Property(e => e.MaPb)
                .HasMaxLength(50)
                .HasColumnName("MaPB");
            entity.Property(e => e.TenPb)
                .HasMaxLength(50)
                .HasColumnName("TenPB");
        });

        modelBuilder.Entity<QuyenHang>(entity =>
        {
            entity.HasKey(e => e.MaQh).HasName("PK__QuyenHan__2725F8564101682F");

            entity.ToTable("QuyenHang");

            entity.Property(e => e.MaQh).HasColumnName("MaQH");
            entity.Property(e => e.TenQh)
                .HasMaxLength(50)
                .HasColumnName("TenQH");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.MaShipper).HasName("PK__Shipper__5C944AF6CAFB2D3A");

            entity.ToTable("Shipper");

            entity.Property(e => e.MaShipper).HasMaxLength(50);
            entity.Property(e => e.DienThoai).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(100);
            entity.Property(e => e.TenShipper).HasMaxLength(50);
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai);

            entity.ToTable("TrangThai");

            entity.Property(e => e.MaTrangThai).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TenTrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<TrangThaiDh>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PK__TrangTha__AADE4138742DA57A");

            entity.ToTable("TrangThaiDH");

            entity.Property(e => e.MaTrangThai).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(100);
            entity.Property(e => e.TenTrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<VChiTietHoaDon>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vChiTietHoaDon");

            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.TenHh)
                .HasMaxLength(50)
                .HasColumnName("TenHH");
        });

        modelBuilder.Entity<VoHang>(entity =>
        {
            entity.HasKey(e => e.MaVh).HasName("PK__VoHang__27251032007ECC01");

            entity.ToTable("VoHang");

            entity.Property(e => e.MaVh).HasColumnName("MaVH");
            entity.Property(e => e.MaHh).HasColumnName("MaHH");
            entity.Property(e => e.MaKh)
                .HasMaxLength(20)
                .HasColumnName("MaKH");

            entity.HasOne(d => d.MaHhNavigation).WithMany(p => p.VoHangs)
                .HasForeignKey(d => d.MaHh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VoHang__MaHH__4C6B5938");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.VoHangs)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VoHang__MaKH__4B7734FF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

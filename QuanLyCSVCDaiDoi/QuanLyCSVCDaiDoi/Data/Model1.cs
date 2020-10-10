namespace QuanLyCSVCDaiDoi.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<ChiHuy> ChiHuys { get; set; }
        public virtual DbSet<CongTac> CongTacs { get; set; }
        public virtual DbSet<DanhMucCSVC> DanhMucCSVCs { get; set; }
        public virtual DbSet<HocVien> HocViens { get; set; }
        public virtual DbSet<LichCongTac> LichCongTacs { get; set; }
        public virtual DbSet<LichSuaChua> LichSuaChuas { get; set; }
        public virtual DbSet<LoaiCSVC> LoaiCSVCs { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiHuy>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<ChiHuy>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);
        }
    }
}

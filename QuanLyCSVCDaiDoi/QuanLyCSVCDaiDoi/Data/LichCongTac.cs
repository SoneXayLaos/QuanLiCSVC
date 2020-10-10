namespace QuanLyCSVCDaiDoi.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichCongTac")]
    public partial class LichCongTac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLich { get; set; }

        public int? MaCongTac { get; set; }

        public int? MaHocVien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianKetThuc { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public int? MaCanBo { get; set; }

        public virtual ChiHuy ChiHuy { get; set; }

        public virtual CongTac CongTac { get; set; }

        public virtual HocVien HocVien { get; set; }
    }
}

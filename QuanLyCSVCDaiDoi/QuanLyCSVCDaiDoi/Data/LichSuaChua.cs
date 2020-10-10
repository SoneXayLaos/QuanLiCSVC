namespace QuanLyCSVCDaiDoi.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuaChua")]
    public partial class LichSuaChua
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayThongBao { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySua { get; set; }

        public int? MaDonVi { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public int? MaCanBo { get; set; }

        public virtual ChiHuy ChiHuy { get; set; }
    }
}

namespace QuanLyCSVCDaiDoi.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucCSVC")]
    public partial class DanhMucCSVC
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? IDLoai { get; set; }

        [StringLength(100)]
        public string TenVC { get; set; }

        public int? SoPhong { get; set; }

        [StringLength(100)]
        public string TinhTrang { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public virtual LoaiCSVC LoaiCSVC { get; set; }
    }
}

namespace SatGitsin2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ilan")]
    public partial class Ilan
    {
        public int IlanId { get; set; }

        [StringLength(50)]
        public string Baslik { get; set; }

        [StringLength(500)]
        public string Ozellik { get; set; }

        [StringLength(150)]
        public string Foto { get; set; }

        public DateTime? Tarih { get; set; }

        public int? KategoriId { get; set; }

        public int? UyeId { get; set; }

        public int? Gorulme { get; set; }

        public int? Fiyat { get; set; }

        public virtual Uye Uye { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Mesaj Mesaj { get; set; }
    }
}

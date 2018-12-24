namespace SatGitsin2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mesaj")]
    public partial class Mesaj
    {
        public int MesajId { get; set; }

        [StringLength(50)]
        public string Icerik { get; set; }

        public int? UyeId { get; set; }

        public int? IlanId { get; set; }

        public DateTime? Tarih { get; set; }

        public virtual Ilan Ilan { get; set; }

        public virtual Uye Uye { get; set; }
    }
}

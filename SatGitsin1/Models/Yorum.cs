namespace SatGitsin1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorum")]
    public partial class Yorum
    {
        public int YorumId { get; set; }

        [StringLength(50)]
        public string Icerik { get; set; }

        public int? UyeId { get; set; }

        public int? SatisId { get; set; }

        public DateTime? Tarih { get; set; }

        public virtual Satis Sati { get; set; }

        public virtual Uye Uye { get; set; }
    }
}

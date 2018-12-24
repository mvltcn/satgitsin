namespace SatGitsin1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class satgitsinDB : DbContext
    {
        public satgitsinDB()
            : base("name=satgitsinDB4")
        {
        }

        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Satis> Satis { get; set; }
        public virtual DbSet<Uye> Uyes { get; set; }
        public virtual DbSet<Yetki> Yetkis { get; set; }
        public virtual DbSet<Yorum> Yorums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Satis>()
                .HasOptional(e => e.Yorum)
                .WithRequired(e => e.Sati);
        }
    }
}

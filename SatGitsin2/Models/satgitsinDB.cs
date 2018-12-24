namespace SatGitsin2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class satgitsinDB : DbContext
    {
        public satgitsinDB()
            : base("name=satgitsinDB")
        {
        }

        public virtual DbSet<Ilan> Ilan { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Mesaj> Mesaj { get; set; }
        public virtual DbSet<Uye> Uye { get; set; }
        public virtual DbSet<Yetki> Yetki { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ilan>()
                .HasOptional(e => e.Mesaj)
                .WithRequired(e => e.Ilan);
        }
    }
}

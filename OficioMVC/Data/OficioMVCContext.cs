using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Models
{
    public class OficioMVCContext : DbContext
    {
        public OficioMVCContext(DbContextOptions<OficioMVCContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documento>()
            .HasIndex(p => new { p.Numeracao, p.Ano })
            .IsUnique(true);

            modelBuilder.Entity<Documento>()
         .Property(p => p.Assunto)
         .HasColumnType("varchar(250)");

        }

        public DbSet<Documento> Documento { get; set; }

        public DbSet<Siga_profs> Siga_profs { get; set; }
        
    }
}

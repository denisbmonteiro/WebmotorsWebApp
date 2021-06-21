using Microsoft.EntityFrameworkCore;
using WebmotorsWebApp.Models;

namespace WebmotorsWebApp.Data
{
    public class WebmotorsContext : DbContext
    {
        public WebmotorsContext(DbContextOptions<WebmotorsContext> options) : base(options)
        {
            
        }

        public DbSet<Anuncio> Anuncios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anuncio>().ToTable("tb_AnuncioWebmotors");
        }
    }
}
using DeskFlowAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeskFlowAPI.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Chamado> Chamados => Set<Chamado>();
        public DbSet<Categoria> Categorias => Set<Categoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Chamado>(chamado =>
            {
                chamado.ToTable("Chamados");

                chamado.HasOne(c => c.Categoria)
                        .WithMany(ca => ca.Chamados)
                        .HasForeignKey(c => c.CategoriaId);
                
                chamado.HasKey(c => c.Id);
                
                chamado.Property(c => c.Titulo).HasMaxLength(150);
                chamado.Property(c => c.Descricao).HasMaxLength(1000);
                chamado.Property(c => c.Prioridade).HasMaxLength(20);
                chamado.Property(c => c.Status).HasMaxLength(20);
                chamado.Property(c => c.SolicitanteNome).HasMaxLength(100);
                chamado.Property(c => c.Solucao).HasMaxLength(1000);
            });
        }
    }
}
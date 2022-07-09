using ExoApi_FST2_.Models;
using Microsoft.EntityFrameworkCore;

namespace ExoApi_FST2_.Contexts
{
    public class ExoApiContext : DbContext
    {
        public ExoApiContext()
        {
        }
        public ExoApiContext(DbContextOptions<ExoApiContext>options): base(options){}

        // vamos utilizar esse método para configurar o banco de dados
        protected override void
        OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // cada provedor tem sua sintaxe para especificação
                optionsBuilder.UseSqlServer("Data Source = LAPTOP-VVMNGSDN\\SQLEXPRESS; initial catalog = ExoApi; Integrated Security = true");
            }
        }
        // dbset representa as entidades que serão utilizadas nas operações de leitura, criação, atualização e deleção
        public DbSet<Projeto> Projetos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }

}

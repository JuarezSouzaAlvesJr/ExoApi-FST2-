using ExoApi_FST2_.Contexts;
using ExoApi_FST2_.Models;

namespace ExoApi_FST2_.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoApiContext _context;

        public ProjetoRepository(ExoApiContext context)
        {
            _context = context;
        }

        public List<Projeto> Listar()
        {
            return _context.Projetos.ToList();
        }
    }
}

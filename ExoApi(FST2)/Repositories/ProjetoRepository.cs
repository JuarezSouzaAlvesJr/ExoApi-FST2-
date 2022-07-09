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

        public Projeto BuscarPorId(int id)
        {
            return _context.Projetos.Find(id);
        }

        public void Cadastrar(Projeto projeto)
        {
            _context.Projetos.Add(projeto);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Projeto projeto = _context.Projetos.Find(id);

            _context.Remove(projeto);
            _context.SaveChanges();
        }

        public void Atualizar(int id, Projeto projeto)
        {
            Projeto projetoBuscado = _context.Projetos.Find(id);

            if (projetoBuscado != null)
            {
                projetoBuscado.Titulo = projeto.Titulo;
                projetoBuscado.Situacao = projeto.Situacao;
                projetoBuscado.DataDeInicio = projeto.DataDeInicio;
                projetoBuscado.Requisitos = projeto.Requisitos;
            }

            _context.Projetos.Update(projetoBuscado);

            _context.SaveChanges();
        }
    }
}

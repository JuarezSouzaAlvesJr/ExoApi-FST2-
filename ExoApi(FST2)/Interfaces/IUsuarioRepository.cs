using ExoApi_FST2_.Models;

namespace ExoApi_FST2_.Interfaces
{
    public interface IUsuarioRepository
    {
        //método para listar usuários
        List<Usuario> Listar();

        //método para retornar usuário por Id;
        Usuario BuscarPorId(int id);

        //método para cadastrar(não tem retorno)
        void Cadastrar(Usuario usuario);

        void Atualizar(int id, Usuario usuario);

        void Deletar(int id);

        //método login
        Usuario Login(string email, string senha);
    }
}

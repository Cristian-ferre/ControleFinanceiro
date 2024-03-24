using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryUsuario
    {
        Task<bool> UsuarioExiste(string name, string senha); 

        Task<Usuarios> ObterUsuario(int usuarioID);
    }
}

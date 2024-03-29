using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryUsuario
    {
        Task<bool> UsuarioExiste(string senha, string Email); 

        Task<Usuarios> ObterUsuario(string Email, string senha);

        Task<UsuarioDTO> Adicionar (UsuarioDTO usuario);
 
    }
}

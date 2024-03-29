using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryUsuario
    {
        Task<bool> UsuarioExiste(string Email);

        Task<Usuarios> ObterUsuario(string Email);

        Task<UsuarioDTO> Adicionar (UsuarioDTO usuario);
 
    }
}

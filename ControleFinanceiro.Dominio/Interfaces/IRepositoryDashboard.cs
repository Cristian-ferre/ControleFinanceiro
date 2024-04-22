using ControleFinanceiro.Dominio.DTOs;

namespace ControleFinanceiro.Dominio.Interfaces
{
    public interface IRepositoryDashboard
    {
        DashboardDTO ObterDados(DateOnly data, Guid usuarioID);

    }
}

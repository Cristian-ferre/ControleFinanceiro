using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.Entities;

namespace ControleFinanceiro.Dados
{
    public struct LogService
    {
        public static void UsuariosOperacoesLog(string nomeController, string nomeMetodo,string nomeOperacao, bool error, Guid usuarioId, ControleFinanceiroDbContext dbContext )
        {
            try
            {
                var newLog = new UsuariosOperacoesLog
                {
                    NomeController = nomeController,
                    NomeMetodo = nomeMetodo,
                    NomeOperacao = nomeOperacao,
                    Error = error,
                    OperacaoData = DateTime.Now,
                    UsuarioId = usuarioId
                };

                dbContext.UsuariosOperacoesLog.Add(newLog);
                dbContext.SaveChanges();
            }
            catch ( Exception ex )
            {
                Console.WriteLine($"Ocorreu um erro ao registrar o log: {ex.Message}");
            }

        }

    }
}

using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;

namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryReceita : IRepositoryReceita
    {

        private ControleFinanceiroDbContext _context;

        public RepositoryReceita(ControleFinanceiroDbContext context)
        {
            _context = context;

        }

        public Receitas ObterPorId(int receitaId)
        {
            return _context.Receitas.FirstOrDefault(r => r.ReceitaId == receitaId);
        }

        public void Adicionar(Receitas receitas)
        {
            _context.Receitas.Add(receitas);
            _context.SaveChanges();
        }

        public void Atualizar(Receitas receitaAtualizada)
        {
            _context.Receitas.Update(receitaAtualizada);
            _context.SaveChanges();
        }

        public void Remover(Receitas receita)
        {
            _context.Receitas.Remove(receita);
            _context.SaveChanges();
        }


        public IEnumerable<Receitas> ObterTodas(DateOnly dataParaExibir)
        {
            // Converte DateOnly em DateTime com horário definido como meia-noite   
            DateTime dataEscolhida = dataParaExibir.ToDateTime(new TimeOnly(0, 0, 0, 0));

            // Montando ENDPOINT para exibir todas a receitas Variáveis e fixas com base no ano e mes 
            return _context.Receitas
                .Where(r =>
                    (r.ReceitaData.Year == dataEscolhida.Year &&
                    r.ReceitaData.Month == dataEscolhida.Month) ||
                    (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida))
                ).ToList();
        }

    }
}

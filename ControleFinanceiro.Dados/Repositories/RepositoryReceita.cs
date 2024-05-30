using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;

namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryReceita : IRepositoryReceita
    {

        private readonly ControleFinanceiroDbContext _context;

        public RepositoryReceita(ControleFinanceiroDbContext context)
        {
            _context = context;

        }
        public Object Adicionar(ReceitaDTO receitas,Guid usuarioId)
        {
            try
            {
                //despesa.DespesaQuantidadeParcelas == 0 ? 1 : despesa.DespesaQuantidadeParcelas;

                var newReceita = new Receitas
                {
                    ReceitaName = receitas.ReceitaName,
                    ReceitaDescricao = receitas.ReceitaDescricao,
                    TipoValor = receitas.TipoValor,
                    ReceitaQuantidadeParcelas = receitas.ReceitaQuantidadeMeses == 0 ? 1 : receitas.ReceitaQuantidadeMeses,
                    ReceitaDataInclusao = DateTime.Now,
                    UsuarioId = usuarioId,
                   
                    //DespesaName = despesa.DespesaName,
                    //DespesaDescricao = despesa.DespesaDescricao,
                    //TipoValor = despesa.TipoValor,
                    //DespesaQuantidadeParcelas = despesa.DespesaQuantidadeParcelas == 0 ? 1 : despesa.DespesaQuantidadeParcelas,
                    //DespesasDataInclusao = DateTime.Now,
                    //UsuarioId = despesa.UsuarioId,
                    //CategoriaId = despesa.CategoriaId,
                    //FormaPagamentoId = despesa.FormaPagamentoId,
                };

                _context.Receitas.Add(newReceita);
                _context.SaveChanges();

                int count = 1;
                DateTime dataVencimento = receitas.ReceitaDataVencimento; // Inicializa a data de vencimento


                while (count <= newReceita.ReceitaQuantidadeParcelas)
                {
                    var newReceitaParcela = new ReceitaParcelas
                    {
                        
                        ReceitaValor = receitas.ReceitaValor,
                        Status = Dominio.Enums.Status.Pendente,
                        ReceitaDataRecebimento = dataVencimento, // Usa a data de vencimento atual,
                        ReceitaId = newReceita.ReceitaId
                    };
                    _context.ReceitaParcelas.Add(newReceitaParcela);
                    count++;
                    // Soma um mês à data de vencimento para o próximo ciclo
                    dataVencimento = dataVencimento.AddMonths(1);
                }
                _context.SaveChanges();

                LogService.UsuariosOperacoesLog("Receita", "Adicionar", "POST", false, usuarioId, _context);

                return new { success = true, message = $"Receita {newReceita.ReceitaName} Adicionada com sucesso", data = newReceita };
            }
            catch (Exception ex)
            {
                LogService.UsuariosOperacoesLog("Receita", "Adicionar", "POST", true, usuarioId, _context);

                return new { succes = false, message = "ALgo deu errado!!", error = ex.Message };
            }
        }

        public Receitas ObterPorId(int receitaId)
        {
            return _context.Receitas.FirstOrDefault(r => r.ReceitaId == receitaId);
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


        public IEnumerable<Receitas> ObterTodas(DateOnly dataParaExibir, Guid usuarioID)
        {
            //// Converte DateOnly em DateTime com horário definido como meia-noite   
            //DateTime dataEscolhida = dataParaExibir.ToDateTime(new TimeOnly(0, 0, 0, 0));

            //// Montando ENDPOINT para exibir todas a receitas Variáveis e fixas com base no ano e mes 
            //return _context.Receitas
            //    .Where(r => r.UsuarioId == usuarioID && (
            //        (r.ReceitaData.Year == dataEscolhida.Year &&
            //        r.ReceitaData.Month == dataEscolhida.Month) ||
            //        (r.ReceitaData <= dataEscolhida && (r.ReceitaDataFim == null || r.ReceitaDataFim >= dataEscolhida)))
            //    ).ToList();

            throw new NotImplementedException();

        }

    }
}

using ControleFinanceiro.Dados.Context;
using ControleFinanceiro.Dominio.DTOs;
using ControleFinanceiro.Dominio.Entities;
using ControleFinanceiro.Dominio.Interfaces;


namespace ControleFinanceiro.Dados.Repositories
{
    public class RepositoryCategoria : IRepositoryCategoria
    {
        private readonly ControleFinanceiroDbContext _context;

        public RepositoryCategoria(ControleFinanceiroDbContext context)
        {
            _context = context;
        }

        public object Adicionar(CategoriaDTO categorias)
        {
            try
            {
                if (categorias == null)
                {
                    return new { success = false, message = "Dados de categoria inválidos"};
                }

                var Categorias = new Categorias
                {
                    CategoriaNome = categorias.CategoriaNome,
                    CategoriaDescricao = categorias.CategoriaDescricao,
                    UsuarioId = categorias.UsuarioId,
                };

                _context.Categorias.Add(Categorias);
                _context.SaveChanges();

                return new { success = true, message = $"{Categorias.CategoriaNome} Inserido com sucesso" };
            }
            catch
            {
                return new { success = false, message = "Ocorreu um erro interno no servidor" };
            }
        }

        public ICollection<Categorias> ObterTodas(Guid usuarioId)
        {
            try
            {
                var categoria = _context.Categorias.Where(c => (c.UsuarioId == usuarioId || c.CategoriaDefault == true) && c.CategoriaDeletado == false).ToList();

                //v1.0:
                //var categorias =  _context.Categorias.ToList();
                return categoria;
            }
            catch
            {
                return new List<Categorias>();
            }
        }

        public object Remover(Guid usuarioId, int categoriaId)
        {
            throw new NotImplementedException();
        }
    }
}

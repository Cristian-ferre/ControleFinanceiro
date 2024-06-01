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

        public object Adicionar(CategoriaDTO categorias, Guid usuarioId)
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
                    UsuarioId = usuarioId,
                };

                _context.Categorias.Add(Categorias);
                _context.SaveChanges();

                LogService.UsuariosOperacoesLog("Categoria", "Adicionar", "POST", false, usuarioId, _context);
                return new { success = true, message = $"{Categorias.CategoriaNome} Inserido com sucesso" };
            }
            catch
            {
                LogService.UsuariosOperacoesLog("Categoria", "Adicionar", "POST", true, usuarioId, _context);
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
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == categoriaId && c.UsuarioId == usuarioId);

                if (categoria == null)
                {
                    return new { success = false, message = "Categoria não encontrada" };
                }

                if (categoria.CategoriaDefault == true)
                {
                    return new { success = false, message = "Não é possivel remover uma categoria Default" };
                }

                categoria.CategoriaDeletado = true;
                _context.SaveChanges();

                LogService.UsuariosOperacoesLog("Categoria", "Remover", "DELETE", false, usuarioId, _context);
                return new { success = true, message = "Categoria removida com sucesso!!" };
            }
            catch (Exception ex)
            {
                LogService.UsuariosOperacoesLog("Categoria", "Remover", "DELETE", true, usuarioId, _context);

                return new { success = false, message = "ALgo deu errado!!", error = ex.Message };
            }

        }
    }
}

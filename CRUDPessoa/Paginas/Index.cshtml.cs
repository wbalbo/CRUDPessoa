using CRUDPessoa.Dados;
using CRUDPessoa.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUDPessoa.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private PessoasDB core = new();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //ChamaODelete();
            ChamaOInsert();
            //ChamaOUpdate();
            ChamaOSelect();
        }

        private void ChamaODelete()
        {
            core.ExcluirPessoa(1);
        }

        private void ChamaOInsert()
        {
            core.AdicionarPessoa(new Pessoas()
            {
                Pessoa = "Richard",
                Email = "richard@richard.com.br",
                Telefone = "31-9999-9999"
            });
        }

        private void ChamaOUpdate()
        {
            core.AtualizarPessoa(new Pessoas()
            {
                IdPessoa = 2,
                Pessoa = "Richard da Silva Sauro",
                Email = "rss@richard.com.br"
            });
        }

        private void ChamaOSelect()
        {
            var listaDePessoas = core.ListarPessoas();
        }
    }
}
using GestaoClienteWeb.ApiConfig;
using GestaoClienteWeb.Repo;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace GestaoClienteWeb.Pages
{
    public class ConfigModel : PageModel
    {
        private readonly ILogger<ConfigModel> _logger;
        public static List<Situacao> situacaobd;
        public static List<Tipo> tipobd;
        public static string page = "Config";
        private Object obj;

        public ConfigModel(ILogger<ConfigModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            PreencherSituacao();
            PreencherTipo();
        }

        public void OnPost()
        {
            if (Request.Method == "POST")
            {
                string action = Request.QueryString.Value.Replace("?action=", "");

                switch (action)
                {
                    case "CadastrarSituacao":
                        SalvarDado("situacao", MontarJsonSituacao(Request.Form["idSituacao"], Request.Form["situacao"]));
                        break;
                    case "AlterarSituacao":
                        AlterarDado("situacao", Request.Form["idSituacao"], MontarJsonSituacao(Request.Form["idSituacao"], Request.Form["situacao"]));
                        break;
                    case "ExcluirSituacao":
                        ExcluirDado("situacao", Request.Form["idSituacao"]);
                        break;
                    case "CadastrarTipo":
                        SalvarDado("tipo", MontarJsonTipo(Request.Form["idTipo"], Request.Form["tipo"]));
                        break;
                    case "AlterarTipo":
                        AlterarDado("tipo", Request.Form["idTipo"], MontarJsonTipo(Request.Form["idTipo"], Request.Form["tipo"]));
                        break;
                    case "ExcluirTipo":
                        ExcluirDado("tipo", Request.Form["idTipo"]);
                        break;
                }
            }
        }

        private void PreencherSituacao()
        {
            Requests.Listar("situacao", page);
        }

        private void PreencherTipo()
        {
            Requests.Listar("tipo", page);
        }

        private void ExcluirDado(string patch, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Requests.Excluir(patch, id);
            }
        }

        private void SalvarDado(string patch, string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                Requests.Inserir(patch, httpContent);
            }
        }

        private void AlterarDado(string patch, string id, string json)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(json))
            {
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                Requests.Alterar(patch, id, httpContent);
            }
        }

        private string MontarJsonTipo(string id, string tipo) {

            return string.Format(@"""id"":""{0}"",
                                     ""tipo"": ""{1}""",id,tipo);

        }

        private string MontarJsonSituacao(string id, string situacao) {

            return string.Format(@"""id"":""{0}"",
                                     ""situacao"": ""{1}""",id,situacao);

        }
    }
}
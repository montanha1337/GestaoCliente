using GestaoClienteWeb.ApiConfig;
using GestaoClienteWeb.Repo;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Web.Helpers;

namespace GestaoClienteWeb.Pages
{
    public class ClienteModel : PageModel
    {
        private readonly ILogger<ClienteModel> _logger;
        public static List<Cliente> clientebd;
        public static List<Situacao> situacaobd;
        public static List<Tipo> tipobd;
        private string patch = "Cliente";
        private string? id;
        private Cliente cliente;

        public ClienteModel(ILogger<ClienteModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            PreencherGridCliente();
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
                    case "Cadastrar":
                        SalvarCliente("cliente", MudarJson(Request.Form["id"], Request.Form["nome"], Request.Form["cpf"], Request.Form["sexo"], Request.Form["situacao"], Request.Form["tipo"]));
                        break;
                    case "Alterar":
                        AlterarCliente("cliente", Request.Form["id"], MudarJson(Request.Form["id"], Request.Form["nome"], Request.Form["cpf"], Request.Form["sexo"], Request.Form["situacao"], Request.Form["tipo"]));
                        break;
                    case "Excluir":
                        ExcluirCliente("cliente", Request.Form["id"]);
                        break;
                }
            }
        }

        public void OnPut()
        {
            id = Request.Form["id"];
        }

        public void OnDelete()
        {
            id = Request.Form["id"];
        }

        private void PreencherSituacao()
        {
            Requests.Listar("situacao", patch);
        }

        private void PreencherTipo()
        {
            Requests.Listar("tipo", patch);
        }

        private void PreencherGridCliente()
        {
            Requests.Listar("cliente", patch);
        }

        public static string FormataCpf(string cpf)
        {
            cpf = Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
            return cpf;
        }

        private void ExcluirCliente(string patch, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Requests.Excluir(patch, id);
            }
        }

        private void SalvarCliente(string patch, string json)
        {
            if (!string.IsNullOrEmpty(json))
            {                
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                Requests.Inserir(patch, httpContent);
            }
        }

        private void AlterarCliente(string patch, string id, string json)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(json))
            {
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                Requests.Alterar(patch, id, httpContent);
            }
        }

        private string MudarJson(string id,string nome, string cpf, string sexo,string situacao, string tipo)
        {
            return string.Format(@"""id"": ""{0}"",
                      ""nome"": ""{1}"",
                      ""cpf"": ""{2}"",
                      ""sexo"": ""{3}"",
                      ""situacao"": ""{4}"",
                      ""tipo"": ""{5}""", id, nome, cpf.Replace(".","").Replace("-",""), sexo, situacao, tipo);
        }
    }
}

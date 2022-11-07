using GestaoClienteWeb.Pages;
using GestaoClienteWeb.Repo;
using Nancy.Json;

namespace GestaoClienteWeb.ApiConfig
{

    public class Requests
    {
        private static readonly HttpClient client = new HttpClient();
        internal static string url = @"https://localhost:7153/{0}";

        public async static void Inserir(string patch, HttpContent data)
        {
            try
            {
                Object result = await client.PostAsync(string.Format(url, patch),data);
            }
            catch (Exception e)
            {
            }
        }

        public async static void Alterar(string patch,string id, HttpContent data)
        {
            try
            {
                string link = patch + "/"+id;
                Object result = await client.PutAsync(string.Format(url, link), data);
            }
            catch (Exception e)
            {
            }
        }

        public async static void Listar(string patch, string page)
        {
            try
            {
                string link = patch + "/listar";

                string result = await client.GetStringAsync(string.Format(url, link));
                JavaScriptSerializer ser = new JavaScriptSerializer();
                if (page == "Cliente")
                {
                    switch (patch)
                    {
                        case "situacao":
                            ClienteModel.situacaobd = ser.Deserialize<List<Situacao>>(result);
                            break;
                        case "cliente":
                            ClienteModel.clientebd = ser.Deserialize<List<Cliente>>(result);
                            break;
                        case "tipo":
                            ClienteModel.tipobd = ser.Deserialize<List<Tipo>>(result);
                            break;
                    }
                }
                if (page == "Config")
                {
                    switch (patch)
                    {
                        case "situacao":
                            ConfigModel.situacaobd = ser.Deserialize<List<Situacao>>(result);
                            break;
                        case "tipo":
                            ConfigModel.tipobd = ser.Deserialize<List<Tipo>>(result);
                            break;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public async static void Excluir(string patch,string id)
        {
            try
            {
                string link = patch + "/"+ id;
                Object result = await client.DeleteAsync(string.Format(url, link));
            }
            catch (Exception e)
            {
            }
        }
    }
}

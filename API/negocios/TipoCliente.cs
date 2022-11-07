using API.ConfigBD;
using System.Data;

namespace API.negocios
{
    public class TipoCliente
    {
        #region ATRIBUTOS
        Config bd = new Config();
        private string id;
        private string tipo;
        #endregion

        #region METODOS
        public IResult Inserir(string tipo)
        {
            try
            {
                this.tipo = tipo;
                ValidarDados();

                string sql = String.Format("EXECUTE PROCTIPO NULL,{0},'I'", this.tipo);
                bd.ExecutaConsulta(sql);
                return Results.Accepted("Inserido com sucesso.");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public IResult Alterar(string id, string tipo)
        {
            try
            {
                this.id = id;
                this.tipo = tipo;
                ValidarDados();
                string sql = String.Format("EXECUTE PROCTIPO {0},'{1}','A'", this.id, this.tipo);
                bd.ExecutaConsulta(sql);
                return Results.Accepted("Alterado com sucesso");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public IResult Excluir(string id)
        {
            try
            {
                this.id = id;
                ValidarDados();

                string sql = String.Format("EXECUTE PROCTIPO {0},NULL,'E'", this.id);
                bd.ExecutaConsulta(sql);
                return Results.Accepted("Excluído com sucesso");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public string Listar()
        {
            try
            {
                ValidarDados();

                string sql = String.Format("EXECUTE PROCTIPO {0},{1},'L'", this.id, this.tipo);
                DataTable dt = bd.GetDadosDataTable(sql);
                return bd.ConverterDataTableJson(dt);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private void ValidarDados()
        {
            id = string.IsNullOrEmpty(id) ? "NULL" : id;
            tipo = string.IsNullOrEmpty(tipo) ? "NULL" : "'"+tipo+"'";
        }
        #endregion
    }
}

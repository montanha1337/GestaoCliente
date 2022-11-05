using API.ConfigBD;
using System.Data;
using System.Text;

namespace API.negocios
{
    public class SituacaoCliente
    {
        #region ATRIBUTOS
        Config bd = new Config();
        private string id { get; set; }
        private string situacao { get; set; }
        #endregion

        #region METODOS
        public IResult Inserir(string situacao)
        {
            try
            {
                this.situacao = situacao;
                ValidarDados();

                string sql = String.Format("EXECUTE PROCSITUACAO NULL,{0},'I'", this.situacao);
                bd.ExecutaConsulta(sql);
                return Results.Accepted("Inserido com sucesso.");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public IResult Alterar(string id, string situacao)
        {
            try
            {
                this.id = id;
                this.situacao = situacao;
                ValidarDados();

                string sql = String.Format("EXECUTE PROCSITUACAO {0},{1},'A'", this.id, this.situacao);
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

                string sql = String.Format("EXECUTE PROCSITUACAO {0},NULL,'E'", this.id);
                bd.ExecutaConsulta(sql);
                return Results.Accepted();
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public string Listar(string id, string situacao)
        {
            try
            {
                this.id = id;
                this.situacao = situacao;
                ValidarDados();

                string sql = String.Format("EXECUTE PROCSITUACAO {0},{1},'L'", this.id, this.situacao);
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
            situacao = string.IsNullOrEmpty(situacao) ? "NULL" : situacao;
        }
        #endregion
    }
}

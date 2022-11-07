using API.ConfigBD;
using System.Data;
using System.Text.Json;

namespace API.negocios
{
    public class Cliente
    {
        #region ATRIBUTOS
        internal Config bd = new Config();
        string id;
        string nome;
        string cpf;
        string sexo;
        string situacao;
        string tipo;
        #endregion

        #region METODOS
        public IResult Inserir(string nome, string cpf, string sexo, string tipo, string situacao)
        {
            try
            {
                this.nome = nome;
                this.cpf = cpf;
                this.sexo = sexo;
                this.tipo = tipo;
                this.situacao = situacao;
                ValidarDados();

                string message;
                if (!string.IsNullOrEmpty(cpf))
                {
                    string sql = String.Format("EXECUTE PROCCLIENTES NULL,'{0}',{1},{2},{3},{4},'I'", this.nome, this.cpf, this.sexo, this.tipo, this.situacao);
                    bd.ExecutaConsulta(sql);
                    message = "Inserido com sucesso.";
                }
                else
                    message = "Cpf inválido ou não inserido.";

                return Results.Accepted(message);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public IResult Alterar(string id, string nome, string cpf, string sexo, string tipo, string situacao)
        {
            try
            {
                this.id = id;
                this.nome = nome;
                this.cpf = cpf;
                this.sexo = sexo;
                this.tipo = tipo;
                this.situacao = situacao;
                ValidarDados();

                string sql = String.Format("EXECUTE PROCCLIENTES {0},{1},{2},{3},{4},{5},'A'", this.id, this.nome, this.cpf, this.sexo, this.tipo, this.situacao);
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

                string sql = String.Format("EXECUTE PROCCLIENTES {0},NULL,NULL,NULL,NULL,NULL,'E'", this.id);
                bd.ExecutaConsulta(sql);
                return Results.Accepted("Excluído com sucesso");
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }

        public string Listar(string id, string nome, string cpf, string sexo, string tipo, string situacao)
        {
            try
            {
                this.id = id;
                this.nome = nome;
                this.cpf = cpf;
                this.sexo = sexo;
                this.tipo = tipo;
                this.situacao = situacao;
                ValidarDados();

                string sql = String.Format("EXECUTE PROCCLIENTES {0},{1},{2},{3},{4},{5},'L'", this.id, this.nome, this.cpf, this.sexo, this.tipo, this.situacao);
                DataTable dt = bd.GetDadosDataTable(sql);
                return bd.ConverterDataTableJson(dt);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Listar()
        {
            try
            {
                ValidarDados();

                string sql = String.Format("EXECUTE PROCCLIENTES {0},{1},{2},{3},{4},{5},'L'", this.id, this.nome, this.cpf, this.sexo, this.tipo, this.situacao);
                DataTable dt = bd.GetDadosDataTable(sql);
                return bd.ConverterDataTableJson(dt);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        internal bool ValidarCpf(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];


            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;

            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        private void ValidarDados()
        {
            id = string.IsNullOrEmpty(id) ? "NULL" : id;
            nome = string.IsNullOrEmpty(nome) ? "NULL" : nome;
            cpf = string.IsNullOrEmpty(cpf) ? "NULL" : cpf;
            sexo = string.IsNullOrEmpty(sexo) ? "NULL" : sexo;
            tipo = string.IsNullOrEmpty(tipo) ? "NULL" : tipo;
            situacao = string.IsNullOrEmpty(situacao) ? "NULL" : situacao;

            if (cpf != "NULL")
            {
                if (!ValidarCpf(cpf))
                    throw new Exception("CPF inválido.");
            }
        }
        #endregion
    }
}

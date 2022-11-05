using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace API.ConfigBD
{
    public class Config
    {
        #region atributos
        private string string_conexao = "Server=MONTANHA-PC;Database=GestaoCliente;Trusted_Connection=True;";
        #endregion

        #region metodos
        public SqlDataReader ExecutaConsulta(string query_string)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = string_conexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.CommandText = query_string;
            comando.Connection = conexao;

            SqlDataReader reader = comando.ExecuteReader();

            return reader;
        }

        public DataTable GetDadosDataTable(string query_string)
        {
            DataTable dtb = new DataTable();

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = string_conexao;
            try
            {
                conexao.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query_string, conexao);

                 adapter.Fill(dtb);

                conexao.Dispose();
                adapter.Dispose();
            }
            catch
            {
            }
            return dtb;
        }

        public string ConverterDataTableJson(DataTable tabela)
        {
            var JSONString = new StringBuilder();
            if (tabela.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    JSONString.Append("{\n");
                    for (int j = 0; j < tabela.Columns.Count; j++)
                    {
                        if (j < tabela.Columns.Count - 1)
                            JSONString.Append("\"" + tabela.Columns[j].ColumnName.ToString() + "\":" + "\"" + tabela.Rows[i][j].ToString() + "\",\n");
                        else if (j == tabela.Columns.Count - 1)
                            JSONString.Append("\"" + tabela.Columns[j].ColumnName.ToString() + "\":" + "\"" + tabela.Rows[i][j].ToString() + "\"\n");
                    }
                    if (i == tabela.Rows.Count - 1)
                        JSONString.Append("}");
                    else
                        JSONString.Append("},");
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.EnderecoRepository
{
    public class EnderecoRepository : IRepository
    {
        Conexao conexao;

        public EnderecoRepository()
        {
            conexao = new Conexao();
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "DELETE FROM enderecos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Atualizar(Endereco endereco)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "UPDATE enderecos SET unidade_federativa = @UNIDADE_FEDERATIVA, cidade = @CIDADE, logradouro = @LOGRADOURO, cep = @CEP, numero = @NUMERO, complemento = @COMPLEMENTO WHERE id = @ID";
            comando.Parameters.AddWithValue("@UNIDADE_FEDERATIVA", endereco.UnidadeFederativa);
            comando.Parameters.AddWithValue("@CIDADE", endereco.Cidade);
            comando.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
            comando.Parameters.AddWithValue("@CEP", endereco.CEP);
            comando.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            comando.Parameters.AddWithValue("@ID", endereco.Id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();

            return quantidadeAfetada == 1;
        }

        public int Inserir(Endereco endereco)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "INSERT INTO enderecos (unidade_federativa, cidade, logradouro, cep, numero, complemento) OUTPUT INSERTED.ID VALUES (@UNIDADE_FEDERATIVA, @CIDADE, @LOGRADOURO, @CEP, @NUMERO, @COMPLEMENTO)";
            comando.Parameters.AddWithValue("@UNIDADE_FEDERATIVA", endereco.UnidadeFederativa);
            comando.Parameters.AddWithValue("@CIDADE", endereco.Cidade);
            comando.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
            comando.Parameters.AddWithValue("@CEP", endereco.CEP);
            comando.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            return id;
        }

        public Endereco ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM enderecos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if (tabela.Rows.Count != 1)
            {
                return null;
            }

            DataRow row = tabela.Rows[0];

            Endereco endereco = new Endereco();
            endereco.Id = Convert.ToInt32(row["id"]);
            endereco.UnidadeFederativa = row["unidade_federativa"].ToString();
            endereco.Cidade = row["cidade"].ToString();
            endereco.Logradouro = row["logradouro"].ToString();
            endereco.CEP = row["cep"].ToString();
            endereco.Complemento = row["complemento"].ToString();
            endereco.Numero = Convert.ToInt32(row["numero"]);

            return endereco;
        }

        public List<Endereco> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM enderecos WHERE cidade LIKE @BUSCA";
            comando.Parameters.AddWithValue("@BUSCA", $"%{busca}%");

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Endereco> enderecos = new List<Endereco>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow row = tabela.Rows[i];

                Endereco endereco = new Endereco();
                endereco.Id = Convert.ToInt32(row["id"]);
                endereco.UnidadeFederativa = row["unidade_federativa"].ToString();
                endereco.Cidade = row["cidade"].ToString();
                endereco.Logradouro = row["logradouro"].ToString();
                endereco.CEP = row["cep"].ToString();
                endereco.Complemento = row["complemento"].ToString();
                endereco.Numero = Convert.ToInt32(row["numero"]);

                enderecos.Add(endereco);
            }
            return enderecos;
        }
    }
}

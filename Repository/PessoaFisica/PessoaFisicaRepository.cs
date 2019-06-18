using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public class PessoaFisicaRepository : IRepository
    {
        Conexao conexao;

        public PessoaFisicaRepository()
        {
            conexao = new Conexao();
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "DELETE FROM clientes_pessoa_fisica WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Atualizar(PessoaFisica pessoaFisica)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "UPDATE clientes_pessoa_fisica SET nome = @NOME, cpf = @CPF, data_nascimento = @DATA_NASCIMENTO, rg = @RG, sexo = @SEXO WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", pessoaFisica.Nome);
            comando.Parameters.AddWithValue("@CPF", pessoaFisica.CPF);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", pessoaFisica.DataNascimento.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@RG", pessoaFisica.RG);
            comando.Parameters.AddWithValue("@SEXO", pessoaFisica.Sexo);
            comando.Parameters.AddWithValue("@ID", pessoaFisica.Id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            return quantidadeAfetada == 1;
        }

        public int Inserir(PessoaFisica pessoaFisica)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "INSERT INTO clientes_pessoa_fisica (nome, cpf, data_nascimento, rg, sexo) OUTPUT INSERTED.ID VALUES (@NOME, @CPF, @DATA_NASCIMENTO, @RG, @SEXO)";
            comando.Parameters.AddWithValue("@NOME", pessoaFisica.Nome);
            comando.Parameters.AddWithValue("@CPF", pessoaFisica.CPF);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", pessoaFisica.DataNascimento.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@RG", pessoaFisica.RG);
            comando.Parameters.AddWithValue("@SEXO", pessoaFisica.Sexo);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public PessoaFisica ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM clientes_pessoa_fisica WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 1)
            {
                DataRow row = tabela.Rows[0];
                PessoaFisica pessoaFisica = new PessoaFisica();
                pessoaFisica.Id = Convert.ToInt32(row["id"]);
                pessoaFisica.Nome = row["nome"].ToString();
                pessoaFisica.RG = row["rg"].ToString();
                pessoaFisica.CPF = row["cpf"].ToString();
                pessoaFisica.Sexo = row["sexo"].ToString();
                pessoaFisica.DataNascimento = Convert.ToDateTime(row["data_nascimento"]);

                return pessoaFisica;
            }

            return null;
        }

        public List<PessoaFisica> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            busca = $"%{busca}%";
            comando.CommandText = "SELECT * FROM clientes_pessoa_fisica WHERE nome LIKE @BUSCA";
            comando.Parameters.AddWithValue("@BUSCA", busca);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            List<PessoaFisica> pessoasFisicas = new List<PessoaFisica>();
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow row = tabela.Rows[i];
                PessoaFisica pessoaFisica = new PessoaFisica();
                pessoaFisica.Id = Convert.ToInt32(row["id"]);
                pessoaFisica.Nome = row["nome"].ToString();
                pessoaFisica.RG = row["rg"].ToString();
                pessoaFisica.CPF = row["cpf"].ToString();
                pessoaFisica.Sexo = row["sexo"].ToString();
                pessoaFisica.DataNascimento = Convert.ToDateTime(row["data_nascimento"]);

                pessoasFisicas.Add(pessoaFisica);
            }
            return pessoasFisicas;
        }
    }
}
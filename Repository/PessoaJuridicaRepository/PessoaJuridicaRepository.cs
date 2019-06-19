using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.PessoaJuridicaRepository
{
    public class PessoaJuridicaRepository : IRepository
    {
        Conexao conexao;

        public PessoaJuridicaRepository()
        {
            conexao = new Conexao();
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "DELETE FROM clientes_pessoa_juridica WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public bool Atualizar(PessoaJuridica pessoaJuridica)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "UPDATE clientes_pessoa_juridica SET cnpj = @CNPJ, razao_social = @RAZAO_SOCIAL, inscricao_estadual = @INSCRICAO_ESTADUAL WHERE id = @ID";
            comando.Parameters.AddWithValue("@CNPJ", pessoaJuridica.CNPJ);
            comando.Parameters.AddWithValue("@RAZAO_SOCIAL", pessoaJuridica.RazaoSocial);
            comando.Parameters.AddWithValue("@INSCRICAO_ESTADUAL", pessoaJuridica.InscricaoEstadual);
            comando.Parameters.AddWithValue("@ID", pessoaJuridica.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(PessoaJuridica pessoaJuridica)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "INSERT INTO clientes_pessoa_juridica (cnpj, razao_social, inscricao_estadual) OUTPUT INSERTED.ID VALUES (@CNPJ, @RAZAO_SOCIAL, @INSCRICAO_ESTADUAL)";
            comando.Parameters.AddWithValue("@CNPJ", pessoaJuridica.CNPJ);
            comando.Parameters.AddWithValue("@RAZAO_SOCIAL", pessoaJuridica.RazaoSocial);
            comando.Parameters.AddWithValue("@INSCRICAO_ESTADUAL", pessoaJuridica.InscricaoEstadual);
            int Id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return Id;
        }

        public PessoaJuridica ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM clientes_pessoa_juridica WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 1)
            {
                DataRow row = tabela.Rows[0];
                PessoaJuridica pessoaJuridica = new PessoaJuridica();
                pessoaJuridica.Id = id;
                pessoaJuridica.CNPJ = row["cnpj"].ToString();
                pessoaJuridica.RazaoSocial = row["razao_social"].ToString();
                pessoaJuridica.InscricaoEstadual = row["inscricao_estadual"].ToString();

                return pessoaJuridica;
            }
            return null;
        }

        public List<PessoaJuridica> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM clientes_pessoa_juridica WHERE cnpj LIKE @BUSCA";
            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<PessoaJuridica> pessoasJuridicas = new List<PessoaJuridica>();

            for(int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow row = tabela.Rows[i];
                PessoaJuridica pessoaJuridica = new PessoaJuridica();
                pessoaJuridica.Id = Convert.ToInt32(row["id"].ToString());
                pessoaJuridica.CNPJ = row["cnpj"].ToString();
                pessoaJuridica.RazaoSocial = row["razao_social"].ToString();
                pessoaJuridica.InscricaoEstadual = row["inscricao_estadual"].ToString();

                pessoasJuridicas.Add(pessoaJuridica);
            }
            return pessoasJuridicas;
        }
    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface IRepository
    {
        int Inserir(PessoaFisica pessoaFisica);

        bool Apagar(int id);

        bool Atualizar(PessoaFisica pessoaFisica);

        PessoaFisica ObterPeloId(int id);

        List<PessoaFisica> ObterTodos(string busca);
    }
}
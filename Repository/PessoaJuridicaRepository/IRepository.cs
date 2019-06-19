using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PessoaJuridicaRepository
{
    interface IRepository
    {
        int Inserir(PessoaJuridica pessoaJuridica);

        bool Apagar(int id);

        bool Atualizar(PessoaJuridica pessoaJuridica);

        PessoaJuridica ObterPeloId(int id);

        List<PessoaJuridica> ObterTodos(string busca);
    }
}

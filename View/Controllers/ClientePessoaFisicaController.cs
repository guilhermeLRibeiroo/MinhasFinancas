using Model;
using Repository;
using Repository.PessoaFisicaRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClientePessoaFisicaController : Controller
    {
        // GET: ClientePessoaFisica
        public ActionResult Index(string busca)
        {
            PessoaFisicaRepository repositorio = new PessoaFisicaRepository();
            ViewBag.ClientesPessoaFisica = repositorio.ObterTodos(busca);

            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Store(string nome, string cpf, string rg, string dataNascimento, string sexo)
        {
            PessoaFisicaRepository repositorio = new PessoaFisicaRepository();
            PessoaFisica pessoa = new PessoaFisica();
            pessoa.Nome = nome;
            pessoa.CPF = cpf;
            pessoa.RG = rg;
            pessoa.DataNascimento = Convert.ToDateTime(dataNascimento);
            pessoa.Sexo = sexo;
            repositorio.Inserir(pessoa);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            PessoaFisicaRepository repositorio = new PessoaFisicaRepository();
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            PessoaFisicaRepository repositorio = new PessoaFisicaRepository();
            PessoaFisica pessoa = repositorio.ObterPeloId(id);
            ViewBag.PessoaFisicaEditar = pessoa;
            return View();
        }

        public ActionResult Update(int id, string nome, string cpf, string rg, string dataNascimento, string sexo)
        {
            PessoaFisicaRepository repositorio = new PessoaFisicaRepository();
            PessoaFisica pessoa = new PessoaFisica();
            pessoa.Id = id;
            pessoa.Nome = nome;
            pessoa.CPF = cpf;
            pessoa.RG = rg;
            pessoa.DataNascimento = Convert.ToDateTime(dataNascimento);
            pessoa.Sexo = sexo;
            repositorio.Atualizar(pessoa);
            return RedirectToAction("Index");
        }
    }
}
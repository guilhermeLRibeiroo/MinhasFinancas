using Model;
using Repository.EnderecoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class EnderecoController : Controller
    {
        // GET: Endereco
        public ActionResult Index(string busca)
        {
            EnderecoRepository repositorio = new EnderecoRepository();
            @ViewBag.ListaEnderecos = repositorio.ObterTodos(busca);

            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Store(string unidadeFederativa, string cidade, string logradouro, string cep, double numero, string complemento)
        {
            Endereco endereco = new Endereco();
            endereco.UnidadeFederativa = unidadeFederativa;
            endereco.Cidade = cidade;
            endereco.Logradouro = logradouro;
            endereco.CEP = cep;
            endereco.Numero = (int)numero;
            endereco.Complemento = complemento;

            EnderecoRepository repositorio = new EnderecoRepository();
            repositorio.Inserir(endereco);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            EnderecoRepository repositorio = new EnderecoRepository();
            ViewBag.EnderecoEditar = repositorio.ObterPeloId(id);
            return View();
        }

        public ActionResult Update(int id, string unidadeFederativa, string cidade, string logradouro, string cep, double numero, string complemento)
        {
            Endereco endereco = new Endereco();
            endereco.UnidadeFederativa = unidadeFederativa;
            endereco.Cidade = cidade;
            endereco.Logradouro = logradouro;
            endereco.CEP = cep;
            endereco.Numero = (int)numero;
            endereco.Complemento = complemento;
            endereco.Id = id;
            EnderecoRepository repositorio = new EnderecoRepository();
            repositorio.Atualizar(endereco);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            EnderecoRepository repositorio = new EnderecoRepository();
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}
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
            ViewBag.Enderecos = repositorio.ObterTodos(busca);

            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Store(string unidadeFederativa, string cidade, string logradouro, string cep, int numero, string complemento)
        {
            Endereco endereco = new Endereco();
            endereco.UnidadeFederativa = unidadeFederativa;
            endereco.Cidade = cidade;
            endereco.Logradouro = logradouro;
            endereco.CEP = cep;
            endereco.Numero = numero;
            endereco.Complemento = complemento;

            EnderecoRepository repositorio = new EnderecoRepository();
            repositorio.Inserir(endereco);
            return RedirectToAction("Index");
        }
    }
}
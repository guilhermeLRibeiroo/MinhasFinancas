using Repository.PessoaJuridicaRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClientePessoaJuridicaController : Controller
    {
        // GET: ClientePessoaJuridica
        public ActionResult Index()
        {
            PessoaJuridicaRepository repositorio = new PessoaJuridicaRepository();
            ViewBag.ClientesPessoaJuridica = repositorio.ObterTodos("");
            return View();
        }
    }
}
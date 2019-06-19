using Model;
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

        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Store(string cnpj, string razaoSocial, string inscricaoEstadual)
        {
            PessoaJuridica pj = new PessoaJuridica();
            pj.CNPJ = cnpj;
            pj.RazaoSocial = razaoSocial;
            pj.InscricaoEstadual = inscricaoEstadual;

            PessoaJuridicaRepository repositorio = new PessoaJuridicaRepository();
            repositorio.Inserir(pj);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            PessoaJuridicaRepository repositorio = new PessoaJuridicaRepository();
            PessoaJuridica pessoa = repositorio.ObterPeloId(id);
            ViewBag.PessoaJuridicaEditar = pessoa;
            return View();
        }

        public ActionResult Update(string cnpj, string razaoSocial, string inscricaoEstadual, int id)
        {
            PessoaJuridica pj = new PessoaJuridica();
            pj.CNPJ = cnpj;
            pj.RazaoSocial = razaoSocial;
            pj.InscricaoEstadual = inscricaoEstadual;
            pj.Id = id;

            PessoaJuridicaRepository repositorio = new PessoaJuridicaRepository();
            repositorio.Atualizar(pj);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            PessoaJuridicaRepository repositorio = new PessoaJuridicaRepository();
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}
using System;
using carfel_checkpoint_tarde.Interfaces;
using carfel_checkpoint_tarde.Models;
using carfel_checkpoint_tarde.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace carfel_checkpoint_tarde.Controllers
{
    public class DepoimentoController : Controller
    {
        private IDepoimentoRepositorio depoimentoRepositorio;

        public DepoimentoController()
        {
            depoimentoRepositorio = new DepoimentoRepositorioSerializacao();
        }

        [HttpGet]
        public IActionResult Index(){
            ViewData["Depoimentos"] = depoimentoRepositorio.Listar().Where(d => d.Status == "Aprovado").OrderByDescending(d => d.DataCriacao).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form){
            DepoimentoModel depoimento = new DepoimentoModel();
            depoimento.Mensagem = form["Mensagem"];
            depoimento.Status = "Em Espera";
            depoimento.DataCriacao = DateTime.Now;
            depoimento.Usuario = new UsuarioModel();
            depoimento.Usuario.ID = int.Parse(HttpContext.Session.GetString("idUsuario"));
            depoimento.Usuario.Nome = HttpContext.Session.GetString("nomeUsuario");
            depoimento.Usuario.Email = HttpContext.Session.GetString("emailUsuario");

            depoimentoRepositorio.Cadastrar(depoimento);

            TempData["Mensagem"] = "<div class='alert alert-success'>Depoimento Cadastrado. Em avaliação do administrador</div>";

            return RedirectToAction("Index");
        }
    }
}
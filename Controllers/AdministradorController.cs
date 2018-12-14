using System.Collections.Generic;
using System.Linq;
using carfel_checkpoint_tarde.Interfaces;
using carfel_checkpoint_tarde.Models;
using carfel_checkpoint_tarde.Repositorios;
using carfel_checkpoint_tarde.ViewModels.Administrador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace carfel_checkpoint_tarde.Controllers
{
    public class AdministradorController : Controller
    {
        private IUsuarioRepositorio usuarioRepositorio;
        private IDepoimentoRepositorio depoimentoRepositorio;

        public AdministradorController()
        {
            usuarioRepositorio = new UsuarioRepositorioSerializacao();
            depoimentoRepositorio = new DepoimentoRepositorioSerializacao();
        }

        [HttpGet]
        public IActionResult Usuarios(){
            if(HttpContext.Session.GetString("tipoUsuario") != "Administrador"){
                return RedirectToAction("Login", "Usuario");
            }

            ViewData["Usuarios"] = usuarioRepositorio.Listar();

            return View();
        }

        [HttpGet]
        public IActionResult Depoimentos(){
            if(HttpContext.Session.GetString("tipoUsuario") != "Administrador"){
                return RedirectToAction("Login", "Usuario");
            }
            
            ViewData["Depoimentos"] = depoimentoRepositorio.Listar();

            return View();
        }

        [HttpGet]
        public IActionResult AlterarDepoimento(int id, string status){
            if(HttpContext.Session.GetString("tipoUsuario") != "Administrador"){
                return RedirectToAction("Login", "Usuario");
            }
            
            DepoimentoModel depoimento = depoimentoRepositorio.BuscarPorId(id);

            if(depoimento == null){
                TempData["Mensagem"] = "Depoimento não encontrado";
                return RedirectToAction("Depoimentos");
            }

            depoimento.Status = status;

            depoimentoRepositorio.Alterar(depoimento);

            TempData["Mensagem"] = "Depoimento alterado";

            return RedirectToAction("Depoimentos");
        }
    
        [HttpGet]
        public IActionResult DashBoard(){
            //Se o usuário não for administrador redireciona para o login
            if(HttpContext.Session.GetString("tipoUsuario") != "Administrador"){
                return RedirectToAction("Login", "Usuario");
            }
            
            //Cria um objeto viewmodel
            DashBoardViewModel dashBoardViewModel = new DashBoardViewModel();

            //Pega os usuarios da base de dados e atribui a lista
            List<UsuarioModel> lsUsuarios = usuarioRepositorio.Listar();
            //Pega os depoimento da base de dados e atribui a lista
            List<DepoimentoModel> lsDepoimentos = depoimentoRepositorio.Listar();

            //Count da lista
            dashBoardViewModel.QuantUsuarios = lsUsuarios.Count;
            dashBoardViewModel.QuantDepoimentos = lsDepoimentos.Count;
            //Count() do Linq, no Linq count é um metodo, 
            //é possível incluir uma expressão lambda
            dashBoardViewModel.QuantDepoimentosAprovados = lsDepoimentos.Count(d => d.Status == "Aprovado");
            dashBoardViewModel.QuantDepoimentosReprovados = lsDepoimentos.Count(d => d.Status == "Reprovado");

            dashBoardViewModel.ListaUsuarios = usuarioRepositorio.Listar().Take(5).OrderByDescending(d => d.DataCriacao).ToList();
            dashBoardViewModel.ListaDepoimentos = depoimentoRepositorio.Listar().Take(5).OrderByDescending(d => d.DataCriacao).ToList();

            //Passa para a view os dados da ViewModel
            return View(dashBoardViewModel);
        }
    }
}
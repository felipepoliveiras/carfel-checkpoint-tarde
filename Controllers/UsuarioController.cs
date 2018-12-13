using carfel_checkpoint_tarde.Interfaces;
using carfel_checkpoint_tarde.Models;
using carfel_checkpoint_tarde.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace carfel_checkpoint_tarde.Controllers
{
    public class UsuarioController : Controller
    {

        /// <summary>
        /// Repositório de usuários do controller
        /// </summary>
        private IUsuarioRepositorio UsuarioRepositorio {get; set;}

        public UsuarioController()
        {
            UsuarioRepositorio = new UsuarioRepositorioSerializacao();
        }

        [HttpGet]
        public IActionResult Cadastrar() => View();

        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //Coleta as informações do POST
            UsuarioModel usuario = new UsuarioModel();
            usuario.Nome = form["nome"];
            usuario.Email = form["email"];
            usuario.Senha = form["senha"];

            //Persiste o usuario no banco de dados
            UsuarioRepositorio.Cadastrar(usuario);

            return RedirectToAction("Login");
        }

        [HttpGet] // <- Atributo
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            string email = form["email"];
            string senha = form["senha"];

            //Busca o usuario do banco de dados
            UsuarioModel usuarioBuscado = UsuarioRepositorio.Login(email, senha);

            if (usuarioBuscado != null)
            {
                //Ao fazer o login armazenar na session idUsuario, emailUsuario, tipoUsuario.
                //Caso seja Usuario Comum direcionar para página de depoimentos com a possibilidade de escrever um depoimento.
                //Caso seja Administrador redirecionar para página Administrador/Dashboard

                //Caso o usuario seja encontrado,
                //Aplica-o na sessão e redireciona para home
                HttpContext.Session.SetString("idUsuario", usuarioBuscado.ID.ToString());
                HttpContext.Session.SetString("emailUsuario", usuarioBuscado.Email);
                HttpContext.Session.SetString("nomeUsuario", usuarioBuscado.Nome);
                if (usuarioBuscado.Administrador)
                {
                    HttpContext.Session.SetString("tipoUsuario", "Administrador");
                    return RedirectToAction("Dashboard", "Administrador");
                }
                else
                {
                    HttpContext.Session.SetString("tipoUsuario", "Comum");
                    return RedirectToAction("Index", "Depoimento");
                }
            }
            else
            {
                ViewBag.Mensagem = "<div class='alert alert-danger'>Usuário Inválido</div>";
                //Reabre a tela de login
                return View();
            }
         }

        
         [HttpGet]
         //Remove o usuário da sessão
         public IActionResult Sair(){
             //Remove os dados da sessão
             HttpContext.Session.Clear();

             return RedirectToAction("Index", "Pages");
         }
    }
}
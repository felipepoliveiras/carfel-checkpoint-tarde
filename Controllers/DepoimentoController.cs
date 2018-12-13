using carfel_checkpoint_tarde.Interfaces;
using carfel_checkpoint_tarde.Repositorios;
using Microsoft.AspNetCore.Mvc;

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

            return View();
        }
    }
}
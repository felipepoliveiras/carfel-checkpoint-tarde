using Microsoft.AspNetCore.Mvc;

namespace carfel_checkpoint_tarde.Controllers
{
    public class PagesController : Controller
    {
        [HttpGet]
        public IActionResult Index(){
            return View();
        }

        [HttpGet]
        public IActionResult Sobre(){
            return View();
        }

        [HttpGet]
        public IActionResult Contato(){
            return View();
        }

        [HttpGet]
        public IActionResult Faq(){
            return View();
        }
    }
}
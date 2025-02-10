using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class CochesController : Controller
    {

        private ICoche coche;

        public CochesController(ICoche coche)
        {
            this.coche = coche;
        }
        public IActionResult Index()
        {
            return View(this.coche);
        }

        [HttpPost]
        public IActionResult Index(string accion)
        {
            if(accion.ToLower() == "acelerar")
            {
                this.coche.Acelerar();
            } else
            {
                this.coche.Frenar();
            }
            return View(this.coche);
        }
    }
}

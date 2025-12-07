using dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using negocio;
namespace discos.Controllers
{
    public class DiscoController : Controller
    {

        // GET: DiscoController
        public ActionResult Index()
        {
            DiscoNegocio negocio = new DiscoNegocio();

            return View(negocio.listar());
        }
       

        // GET: DiscoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DiscoController/Create
        public ActionResult Create()
        {
            EstiloNegocio estiloNegocio = new EstiloNegocio();
            TipoEdicionNegocio tipoNegocio = new TipoEdicionNegocio();

            ViewBag.Estilos = new SelectList(estiloNegocio.listar(), "Id", "Descripcion");
            ViewBag.TipoEdiciones = new SelectList(tipoNegocio.listar(), "Id", "Descripcion");

            return View();
        }
        [HttpPost]
        public ActionResult Create(Disco nuevo)
        {
            try
            {
                DiscoNegocio negocio = new DiscoNegocio();
                negocio.agregar(nuevo);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nuevo);
            }
        }

        // GET: DiscoController/Edit/5
        public ActionResult Edit(int id)
        {
            DiscoNegocio negocio = new DiscoNegocio();
            Disco disco = negocio.buscarPorId(id);
            return View(disco);
        }

        // POST: DiscoController/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Disco disco)
        {
            try
            {
                // Inicializar Estilo y TipoEdicion si vienen null
                if (disco.Estilo == null)
                    disco.Estilo = new Estilo { Id = 1 };
                if (disco.TipoEdicion == null)
                    disco.TipoEdicion = new TipoEdicion { Id = 1 };

                DiscoNegocio negocio = new DiscoNegocio();
                negocio.modificar(disco);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(disco);
            }
        }

        //// GET: DiscoController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: DiscoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                DiscoNegocio negocio = new DiscoNegocio();
                negocio.eliminar(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Podés loguear el error si querés
                return View("Error"); // o simplemente return View();
            }
        }


    }
}

using ExpressSupport.DAL;
using ExpressSupport.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ExpressSupport.Controllers
{
    public class CategoriesSoftwareController : Controller
    {
        public readonly DataBaseContext _context;

        public CategoriesSoftwareController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return _context.CategoriesSoftware != null ?
                        View(await _context.CategoriesSoftware.ToListAsync()) :
                        Problem("Entity set 'DataBaseContext.CategoriesSoftware'  is null.");//IF ternario. El signo ? = ENTONCES, los : = SINO 
        }

        // GET: CategoriesSoftware/Create (Obtener) //GET = SELECT
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriesSoftware/Create (Crear) //POST = CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategorySoftware categorySoftware)//Captura el objeto country con sus propiedades
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(categorySoftware);//Método Add() crea una BD
                    await _context.SaveChangesAsync();//Aquí va a la capa MODEL y GUARDA el país en la tabla Countries
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoría de software con el mismo nombre");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(categorySoftware);
        }

    }
}





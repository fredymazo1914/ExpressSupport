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

        // GET: CategoriesSoftware/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CategoriesSoftware == null)
            {
                return NotFound();
            }

            var categorySoftware = await _context.CategoriesSoftware.FindAsync(id);
            if (categorySoftware == null)
            {
                return NotFound();
            }
            return View(categorySoftware);
        }

        // POST: CategoriesSoftware/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreateDate,ModifieDate")] CategorySoftware categorySoftware)
        {
            if (id != categorySoftware.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    categorySoftware.ModifiedDate = DateTime.Now;//Se automatiza la fecha de moficación de la tabla Countries
                    _context.Update(categorySoftware);//Método Update() actualiza obj en BD
                    await _context.SaveChangesAsync();//Aquí se hace el update en BD
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!CountryExists(country.Id))
                    if (!CategorySoftwareExists(categorySoftware.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoría con el mismo nombre");
                    }
                }


                return RedirectToAction(nameof(Index));
            }
            return View(categorySoftware);
        }

        // GET: CategoriesSoftware/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CategoriesSoftware == null)
            {
                return NotFound();
            }

            var categorySoftware = await _context.CategoriesSoftware
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorySoftware == null)
            {
                return NotFound();
            }

            return View(categorySoftware);
        }

        // POST: CategoriesSoftware/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CategoriesSoftware == null)
            {
                return Problem("Entity set 'DataBaseContext.CategoriesSoftware'  is null.");
            }
            var categorySoftware = await _context.CategoriesSoftware.FindAsync(id);
            if (categorySoftware != null)
            {
                _context.CategoriesSoftware.Remove(categorySoftware);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool CategorySoftwareExists(Guid id)
        {
            return (_context.CategoriesSoftware?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}





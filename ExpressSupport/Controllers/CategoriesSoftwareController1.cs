using ExpressSupport.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpressSupport.Controllers
{
    public class CategoriesSoftwareController1 : Controller
    {
        public readonly DataBaseContext _context;

        public CategoriesSoftwareController1 (DataBaseContext context)
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
    }
}
  
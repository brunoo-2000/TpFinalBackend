
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TpfinalBack.Models;
using TpfinalBack.Data;
using TpfinalBack.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

[SessionAuthorize]
[Route("Direcciones/{action=Index}/{id?}")]
public class DireccionController : Controller
{
    private readonly ApplicationDbContext _context;

    public DireccionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: DIRECCIONS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Direccion.Include(d => d.Cliente).ToListAsync());
    }

    // GET: DIRECCIONS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var direccion = await _context.Direccion
            .FirstOrDefaultAsync(m => m.Id == id);
        if (direccion == null)
        {
            return NotFound();
        }

        return View(direccion);
    }

    // GET: DIRECCIONS/Create
    public IActionResult Create(int? clienteId)
    {
        ViewBag.Clientes = new SelectList(_context.Cliente.OrderBy(c => c.Nombre), "Id", "Nombre");
        var direccion = new Direccion();
        if(clienteId.HasValue)
        {
            direccion.ClienteId = clienteId.Value;
        }
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Calle,Numero,Ciudad,ClienteId,Cliente")] Direccion direccion)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Add(direccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar la dirección.");
            }
        }
        return View(direccion);
    }

    // GET: DIRECCIONS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var direccion = await _context.Direccion.FindAsync(id);
        if (direccion == null)
        {
            return NotFound();
        }
        return View(direccion);
    }

    // POST: DIRECCIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Calle,Numero,Ciudad,ClienteId,Cliente")] Direccion direccion)
    {
        if (id != direccion.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(direccion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionExists(direccion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(direccion);
    }

    // GET: DIRECCIONS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var direccion = await _context.Direccion
            .FirstOrDefaultAsync(m => m.Id == id);
        if (direccion == null)
        {
            return NotFound();
        }

        return View(direccion);
    }

    // POST: DIRECCIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var direccion = await _context.Direccion.FindAsync(id);
        if (direccion != null)
        {
            _context.Direccion.Remove(direccion);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DireccionExists(int? id)
    {
        return _context.Direccion.Any(e => e.Id == id);
    }
}

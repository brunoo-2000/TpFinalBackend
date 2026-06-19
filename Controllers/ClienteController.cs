
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TpfinalBack.Models;
using TpfinalBack.Data;
using TpfinalBack.Filters;

[SessionAuthorize]
[Route("Clientes/{action=Index}/{id?}")]
public class ClienteController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClienteController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: CLIENTES
    public async Task<IActionResult> Index()
    {
        return View(await _context.Cliente.Include(c => c.Direcciones).ToListAsync());
    }

    // GET: CLIENTES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var cliente = await _context.Cliente
            .Include(c => c.Direcciones)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (cliente == null) return NotFound();

        return View(cliente);
    }

    // GET: CLIENTES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CLIENTES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Cuit,Telefono")] Cliente cliente, string? Calle, string? Numero, string? Ciudad)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(Calle) && !string.IsNullOrWhiteSpace(Numero) && !string.IsNullOrWhiteSpace(Ciudad))
            {
                var direccion = new TpfinalBack.Models.Direccion
                {
                    Calle = Calle,
                    Numero = Numero,
                    Ciudad = Ciudad,
                    ClienteId = cliente.Id
                };
                _context.Direccion.Add(direccion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    // GET: CLIENTES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var cliente = await _context.Cliente
            .Include(c => c.Direcciones)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null) return NotFound();

        return View(cliente);
    }

    // POST: CLIENTES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Cuit,Telefono")] Cliente cliente, string? Calle, string? Numero, string? Ciudad)
    {
        if (id != cliente.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cliente);

                if (!string.IsNullOrWhiteSpace(Calle) && !string.IsNullOrWhiteSpace(Numero) && !string.IsNullOrWhiteSpace(Ciudad))
                {
                    var direccion = new TpfinalBack.Models.Direccion
                    {
                        Calle = Calle,
                        Numero = Numero,
                        Ciudad = Ciudad,
                        ClienteId = cliente.Id
                    };
                    _context.Direccion.Add(direccion);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        cliente.Direcciones = await _context.Direccion.Where(d => d.ClienteId == cliente.Id).ToListAsync();
        return View(cliente);
    }

    // GET: CLIENTES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var cliente = await _context.Cliente
            .FirstOrDefaultAsync(m => m.Id == id);

        if (cliente == null) return NotFound();

        ViewBag.TienePedidos = await _context.Pedido.AnyAsync(p => p.ClienteId == id);
        return View(cliente);
    }

    // POST: CLIENTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (await _context.Pedido.AnyAsync(p => p.ClienteId == id))
        {
            var cliente = await _context.Cliente.FindAsync(id);
            ViewBag.TienePedidos = true;
            return View(cliente);
        }

        var clienteAEliminar = await _context.Cliente
            .Include(c => c.Direcciones)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clienteAEliminar != null)
        {
            _context.Direccion.RemoveRange(clienteAEliminar.Direcciones);
            _context.Cliente.Remove(clienteAEliminar);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // POST: CLIENTES/EliminarDireccion/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarDireccion(int id, int clienteId)
    {
        var direccion = await _context.Direccion.FindAsync(id);
        if (direccion != null && direccion.ClienteId == clienteId)
        {
            _context.Direccion.Remove(direccion);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Edit), new { id = clienteId });
    }

    private bool ClienteExists(int? id)
    {
        return _context.Cliente.Any(e => e.Id == id);
    }
}

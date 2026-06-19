
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TpfinalBack.Models;
using TpfinalBack.Data;
using TpfinalBack.Filters;

[SessionAuthorize]
public class DetallePedidoController : Controller
{
    private readonly ApplicationDbContext _context;

    public DetallePedidoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: DETALLEPEDIDOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.DetallePedido.ToListAsync());
    }

    // GET: DETALLEPEDIDOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var detallepedido = await _context.DetallePedido
            .FirstOrDefaultAsync(m => m.Id == id);
        if (detallepedido == null)
        {
            return NotFound();
        }

        return View(detallepedido);
    }

    // GET: DETALLEPEDIDOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DETALLEPEDIDOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Cantidad,CostoUnitario,SubTotal,PedidoId,Pedido,ProductoId,Producto")] DetallePedido detallepedido)
    {
        if (ModelState.IsValid)
        {
            _context.Add(detallepedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(detallepedido);
    }

    // GET: DETALLEPEDIDOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var detallepedido = await _context.DetallePedido.FindAsync(id);
        if (detallepedido == null)
        {
            return NotFound();
        }
        return View(detallepedido);
    }

    // POST: DETALLEPEDIDOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Cantidad,CostoUnitario,SubTotal,PedidoId,Pedido,ProductoId,Producto")] DetallePedido detallepedido)
    {
        if (id != detallepedido.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(detallepedido);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePedidoExists(detallepedido.Id))
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
        return View(detallepedido);
    }

    // GET: DETALLEPEDIDOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var detallepedido = await _context.DetallePedido
            .FirstOrDefaultAsync(m => m.Id == id);
        if (detallepedido == null)
        {
            return NotFound();
        }

        return View(detallepedido);
    }

    // POST: DETALLEPEDIDOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var detallepedido = await _context.DetallePedido.FindAsync(id);
        if (detallepedido != null)
        {
            _context.DetallePedido.Remove(detallepedido);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DetallePedidoExists(int? id)
    {
        return _context.DetallePedido.Any(e => e.Id == id);
    }
}

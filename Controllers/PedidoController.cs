
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TpfinalBack.Models;
using TpfinalBack.Data;
using TpfinalBack.Filters;

[SessionAuthorize]
[Route("Pedidos/{action=Index}/{id?}")]
public class PedidoController : Controller
{
    private readonly ApplicationDbContext _context;

    public PedidoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PEDIDOS
    public async Task<IActionResult> Index(string? busqueda)
    {
        ViewBag.Busqueda = busqueda;
        ViewBag.Usuarios = await _context.Usuario
            .Select(u => u.Username)
            .OrderBy(u => u)
            .ToListAsync();

        var pedidos = _context.Pedido
            .Include(p => p.Cliente)
            .Include(p => p.Usuario)
            .Where(p => p.Confirmado)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(busqueda))
            pedidos = pedidos.Where(p => p.Usuario.Username.Contains(busqueda));

        return View(await pedidos.OrderByDescending(p => p.fechaDespacho).ToListAsync());
    }

    // GET: PEDIDOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var pedido = await _context.Pedido
            .Include(p => p.Cliente)
            .Include(p => p.Usuario)
            .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (pedido == null) return NotFound();

        return View(pedido);
    }

}

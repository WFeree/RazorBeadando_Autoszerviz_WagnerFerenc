using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;
using RazorBeadando_Autoszerviz_WagnerFerenc.Data;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Pages.UgyfelPages;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;
    public DetailsModel(AppDbContext db) => _db = db;

    public Ugyfel Ugyfel { get; set; } = new();
    public decimal SumPrice => Ugyfel.Javitasok.Sum(s => s.Koltseg);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var u = await _db.Ugyfelek
            .Include(u => u.Javitasok)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (u == null) return NotFound();
        Ugyfel = u;
        return Page();
    }
}

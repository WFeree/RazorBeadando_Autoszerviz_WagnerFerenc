using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Data;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Pages.JavitasPages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;

    public List<Javitas> Javitasok { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public Allapot? StatusFilter { get; set; }

    public SelectList StatusOptions { get; set; } = default!;


    public async Task OnGetAsync()
    {
        StatusOptions = new SelectList(Enum.GetValues<Allapot>());

        var query = _db.Javitasok.Include(s => s.Ugyfel).AsQueryable();
        if (StatusFilter.HasValue)
            query = query.Where(s => s.Allapot == StatusFilter);

        Javitasok = await query.OrderByDescending(s => s.Hozzaadva).ToListAsync();
    }


    public async Task<IActionResult> OnPostAdvanceAsync(int id)
    {
        var javitas = await _db.Javitasok.FindAsync(id);
        if (javitas == null) return NotFound();

        switch (javitas.Allapot)
        {
            case Allapot.Felvéve:
                javitas.Allapot = Allapot.Folyamatban;
                break;
            case Allapot.Folyamatban:
                if (javitas.Koltseg <= 0)
                {
                    TempData["Error"] = "Állítson be árat (> 0) mielőtt késznek jelölné.";
                    return RedirectToPage(new { StatusFilter });
                }
                javitas.Allapot = Allapot.Kész;
                break;
        }

        await _db.SaveChangesAsync();
        return RedirectToPage(new { StatusFilter }); // keep the filter
    }
}
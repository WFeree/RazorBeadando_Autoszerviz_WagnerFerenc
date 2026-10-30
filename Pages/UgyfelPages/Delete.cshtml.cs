using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;
using RazorBeadando_Autoszerviz_WagnerFerenc.Data;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Pages.UgyfelPages;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;
    public DeleteModel(AppDbContext db) => _db = db;

    public Ugyfel Ugyfel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var u = await _db.Ugyfelek.FindAsync(id);
        if (u == null) return NotFound();
        Ugyfel = u;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var ugyfel = await _db.Ugyfelek.FindAsync(id);
        if (ugyfel == null) return NotFound();

        if (await _db.Javitasok.AnyAsync(s => s.UgyfelId == id))
        {
            TempData["Error"] = $"{ugyfel.Nev} nem lehet törölni: van hozzá rendelt javítás.";
            return RedirectToPage("Index");
        }

        _db.Ugyfelek.Remove(ugyfel);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}

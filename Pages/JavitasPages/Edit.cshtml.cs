using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Data;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Pages.JavitasPages;

public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;

    [BindProperty] public Javitas Javitas { get; set; } = new();
    public SelectList ClientList { get; set; } = default!;

    private async Task LoadClientsAsync() =>
        ClientList = new SelectList(
            await _db.Ugyfelek.OrderBy(c => c.Nev).ToListAsync(), "Id", "Nev");

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var s = await _db.Javitasok.FindAsync(id);
        if (s == null) return NotFound();
        Javitas = s;
        await LoadClientsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Javitas.Ugyfel");
        if (Javitas.Allapot == Allapot.Kész && Javitas.Koltseg <= 0)
            ModelState.AddModelError("Javitas.Koltseg", "A költségnek nagyobbnak kell lennie, mint 0 a Kész állapot esetén.");

        if (!ModelState.IsValid) { await LoadClientsAsync(); return Page(); }

        Javitas.Rendszam = Javitas.Rendszam.ToUpper();
        _db.Attach(Javitas).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}

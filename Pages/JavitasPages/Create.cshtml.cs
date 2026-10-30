using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Data;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Pages.JavitasPages;

public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public CreateModel(AppDbContext db) => _db = db;

    [BindProperty] public Javitas Javitasok { get; set; } = new();
    public SelectList ClientList { get; set; } = default!;

    private async Task LoadClientsAsync() =>
        ClientList = new SelectList(
            await _db.Ugyfelek.OrderBy(c => c.Nev).ToListAsync(), "Id", "Nev");

    public async Task OnGetAsync() => await LoadClientsAsync();

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Javitasok.Ugyfel");

        if (Javitasok.Allapot == Allapot.Kész && Javitasok.Koltseg <= 0)
            ModelState.AddModelError("Javitasok.Koltseg", "A költségnek nagyobbnak kell lennie, mint 0 a Kész állapot esetén.");

        if (!ModelState.IsValid)
        {
            await LoadClientsAsync();
            return Page();
        }

        Javitasok.Rendszam = Javitasok.Rendszam.ToUpper();
        Javitasok.Hozzaadva = DateTime.Now;
        _db.Javitasok.Add(Javitasok);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
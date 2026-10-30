using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;
using RazorBeadando_Autoszerviz_WagnerFerenc.Data;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Pages.UgyfelPages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;

    public List<Ugyfel> Ugyfelek { get; set; } = new();

    public async Task OnGetAsync() =>
        Ugyfelek = await _db.Ugyfelek.OrderBy(u => u.Nev).ToListAsync();
}

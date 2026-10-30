using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;
using RazorBeadando_Autoszerviz_WagnerFerenc.Data;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Pages.UgyfelPages;

public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public CreateModel(AppDbContext db) => _db = db;

    [BindProperty] public Ugyfel Ugyfel { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _db.Ugyfelek.Add(Ugyfel);
        await _db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}

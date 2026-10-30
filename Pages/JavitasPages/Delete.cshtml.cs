using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBeadando_Autoszerviz_WagnerFerenc.Models;
using RazorBeadando_Autoszerviz_WagnerFerenc.Data;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Pages.JavitasPages;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Javitas Javitas { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var javitas = await _context.Javitasok.FirstOrDefaultAsync(m => m.Id == id);
        if (javitas is null)
        {
            return NotFound();
        }
        else
        {
            Javitas = javitas;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var javitas = await _context.Javitasok.FindAsync(id);
        if (javitas != null)
        {
            Javitas = javitas;
            _context.Javitasok.Remove(Javitas);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

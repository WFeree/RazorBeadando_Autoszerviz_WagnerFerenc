using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Models
{
    public enum Allapot
    {
        Felvéve, Folyamatban, Kész
    }
    public class Javitas
    {
        public int Id { get; set; }

        [Display(Name = "Ügyfél")]
        public int UgyfelId { get; set; }
        public Ugyfel? Ugyfel { get; set; }

        [Required(ErrorMessage = "A rendszám megadása kötelező.")]
        [RegularExpression(@"^[A-Za-z]{3,4}-\d{3}$", ErrorMessage = "Formátum: ABC-123 vagy ABCD-123")]
        [StringLength(10, ErrorMessage = "A rendszám maximum 10 karakter lehet.")]
        public string Rendszam { get; set; } = "";

        [StringLength(300, ErrorMessage = "A leírás maximum 300 karakter lehet.")]
        public string? Leiras { get; set; }

        [Precision(18, 2), Range(0, 100000000)]
        public decimal Koltseg { get; set; }

        public Allapot Allapot { get; set; } = Allapot.Felvéve;

        [DataType(DataType.Date)]
        [Display(Name = "Hozzáadás dátuma")]
        public DateTime Hozzaadva { get; set; } = DateTime.Now;
    }
}

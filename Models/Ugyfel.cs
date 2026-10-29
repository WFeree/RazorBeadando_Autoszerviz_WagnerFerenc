using System.ComponentModel.DataAnnotations;

namespace RazorBeadando_Autoszerviz_WagnerFerenc.Models
{
    public class Ugyfel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A név megadása kötelező."), StringLength(100, ErrorMessage = "A név maximum 100 karakter lehet.")]
        public string Nev { get; set; } = "";

        [StringLength(20, ErrorMessage = "A telefonszám maximum 20 karakter lehet.")]
        public string? Telefonszam { get; set; }

        [StringLength(100, ErrorMessage = "Az email maximum 100 karakter lehet.")]
        public string? Email { get; set; }

        public List<Javitas> Javitasok { get; set; } = new List<Javitas>();
    }
}

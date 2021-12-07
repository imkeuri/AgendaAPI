using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class ContactoTelefono
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string NumeroTelefono { get; set; }

        [Required]
        public int Id_Contacto { get; set; }

        [ForeignKey("Id_Contacto")]
        public Contacto Contacto { get; set; }

    }
}

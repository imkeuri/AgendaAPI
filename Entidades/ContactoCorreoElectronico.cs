using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entidades
{
    public class ContactoCorreoElectronico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(140)]
        public string Correo { get; set; }

        [Required]
        public int Id_Contacto { get; set; }

        [ForeignKey("Id_Contacto")]
        public Contacto Contacto { get; set; }
    }
}

using Entidades;
using System;
using System.Collections.Generic;

namespace DTOs
{
    public class ContactoDTO
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<ContactoTelefono> Telefonos { get; set; }
        public List<ContactoCorreoElectronico> CorreoElectronicos { get; set; }


    }
}

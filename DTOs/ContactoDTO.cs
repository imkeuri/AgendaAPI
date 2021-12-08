using Entidades;
using System;
using System.Collections.Generic;

namespace DTOs
{
    public class ContactoDTO
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public List<ContactoTelefonoDTO> Telefonos { get; set; }
        public List<ContactoCorreoElectronicoDTO> CorreoElectronicos { get; set; }


    }
}

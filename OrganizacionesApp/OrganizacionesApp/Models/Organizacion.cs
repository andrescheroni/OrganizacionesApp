using System;
using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Organizacion
    {
        public int OrganizacionID { get; set; }
        public byte[] Imagen { get; set; }
        public string RazonSocial { get; set; }
        public string Descripcion { get; set; }
        public string CUIT { get; set; }
        public string NombreResponsable { get; set; }
        public string ApellidoResponsable { get; set; }
        public string DNIResponsable { get; set; }
        public string Direccion { get; set; }
        public int? LocalidadID { get; set; }
        public int? ProvinciaID { get; set; }
        public int? PaisID { get; set; }
        public string Mail { get; set; }
        public string PasswordHash { get; set; }
        public string Telefono { get; set; }
        public bool Verificacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaVerificacion { get; set; }

        //Propiedades de navegación
        public Localidad Localidad { get; set; }
        public Provincia Provincia { get; set; }
        public Pais Pais { get; set; }
        public ICollection<Oportunidad> Oportunidades { get; set; }
    }
}
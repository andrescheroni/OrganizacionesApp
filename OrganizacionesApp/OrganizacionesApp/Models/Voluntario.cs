using System;
using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Voluntario
    {
        public int VolutnarioID { get; set; }
        public byte[] Imagen { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int DNI { get; set; }
        public string Direccion { get; set; }
        public int? LocalidadID { get; set; }
        public int? ProvinciaID { get; set; }
        public int? PaisID { get; set; }
        public string Mail { get; set; }
        public string PasswordHash { get; set; }
        public string Telefono { get; set; }
        public int EntornoID { get; set; }
        public int IntensidadID { get; set; }
        public int SocialID { get; set; }
        public bool Verificacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaVerificacion { get; set; }

        //Propiedades de navegación
        public Entorno Entorno { get; set; }
        public Intensidad Intensidad { get; set; }
        public Social Social { get; set; }
        public Localidad Localidad { get; set; }
        public Provincia Provincia { get; set; }
        public Pais Pais { get; set; }
        public ICollection<Postulacion> Postulaciones { get; set; }
    }
}

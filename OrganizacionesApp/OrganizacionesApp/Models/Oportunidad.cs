using System;
using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Oportunidad
    {
        public int OportunidadID { get; set; }
        public byte[] Imagen { get; set; }
        public int OrganizacionID { get; set; }
        public string OrganizacionRazonSocial { get; set; }
        public string Actividad { get; set; }
        public string Descripcion { get; set; }
        public string Rol { get; set; }
        public int EntornoID { get; set; }
        public string EntornoTipoEntorno { get; set; }
        public int IntensidadID { get; set; }
        public string IntensidadTipoIntensidad { get; set; }
        public int SocialID { get; set; }
        public string SocialTipoSocial { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? LocalidadID { get; set; }
        public string LocalidadTipoLocalidad { get; set; }
        public int? ProvinciaID { get; set; }
        public string ProvinciaNombreProvincia { get; set; }
        public int? PaisID { get; set; }
        public string PaisNombrePais { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public bool Baja { get; set; }

        //Propiedades de navegación
        public Organizacion Organizacion { get; set; }
        public Entorno Entorno { get; set; }
        public Intensidad Intensidad { get; set; }
        public Social Social { get; set; }
        public Localidad Localidad { get; set; }
        public Provincia Provincia { get; set; }
        public Pais Pais { get; set; }
        public ICollection<Postulacion> Postulaciones { get; set; }
    }
}



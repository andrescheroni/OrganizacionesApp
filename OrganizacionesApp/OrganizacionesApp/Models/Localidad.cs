using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Localidad
    {
        public int LocalidadID { get; set; }
        public int ProvinciaID { get; set; }
        public string TipoLocalidad { get; set; }

        //Propiedades de navegación
        public Provincia Provincia { get; set; }
        public ICollection<Oportunidad> Oportunidades { get; set; }
        public ICollection<Voluntario> Voluntarios { get; set; }
        public ICollection<Organizacion> Organizaciones { get; set; }
    }
}



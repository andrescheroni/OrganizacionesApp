using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Provincia
    {
        public int ProvinciaID { get; set; }
        public int PaisID { get; set; }
        public string NombreProvincia { get; set; }

        //Propiedades de navegación
        public Pais Pais { get; set; }
        public ICollection<Oportunidad> Oportunidades { get; set; }
        public ICollection<Voluntario> Voluntarios { get; set; }
        public ICollection<Organizacion> Organizaciones { get; set; }
        public ICollection<Localidad> Localidades { get; set; }

    }
}



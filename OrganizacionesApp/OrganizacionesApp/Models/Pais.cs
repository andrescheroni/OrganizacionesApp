using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Pais
    {
        public int PaisID { get; set; }
        public string NombrePais { get; set; }

        //Propiedades de navegación
        public ICollection<Oportunidad> Oportunidades { get; set; }
        public ICollection<Voluntario> Voluntarios { get; set; }
        public ICollection<Organizacion> Organizaciones { get; set; }
        public ICollection<Provincia> Provincias { get; set; }
    }
}



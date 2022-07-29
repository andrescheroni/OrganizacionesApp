using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Intensidad
    {
        public int IntensidadID { get; set; }
        public string TipoIntensidad { get; set; }

        //Propiedades de navegación
        public ICollection<Oportunidad> Oportunidades { get; set; }
        public ICollection<Voluntario> Voluntarios { get; set; }
    }
}



using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Entorno
    {
        public int EntornoID { get; set; }
        public string TipoEntorno { get; set; }

        //Propiedades de navegación
        public ICollection<Oportunidad> Oportunidades { get; set; }
        public ICollection<Voluntario> Voluntarios { get; set; }
    }
}



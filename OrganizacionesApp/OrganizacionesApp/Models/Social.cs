using System.Collections.Generic;

namespace OrganizacionesApp.Models
{
    public class Social
    {
        public int SocialID { get; set; }
        public string TipoSocial { get; set; }

        //Propiedades de navegación
        public ICollection<Oportunidad> Oportunidades { get; set; }
        public ICollection<Voluntario> Voluntarios { get; set; }
    }
}



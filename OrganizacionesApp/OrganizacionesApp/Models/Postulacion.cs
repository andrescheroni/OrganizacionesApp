using System;

namespace OrganizacionesApp.Models
{
    public class Postulacion
    {
        public int PostulacionID { get; set; }
        public int OportunidadID { get; set; }
        public string OportunidadActividad { get; set; }
        public int? VoluntarioID { get; set; }
        public string VoluntarioNombre { get; set; }
        public string VoluntarioApellido { get; set; }        
        public byte[] VoluntarioImagen { get; set; }
        //public string VoluntarioNombreCompleto => $"{VoluntarioNombre} {VoluntarioApellido}";
        public DateTime FechaPostulacion { get; set; }
        public int EstadoPostulacionID { get; set; }
        public string EstadoPostulacionEstado { get; set; }        
        public string Mensaje { get; set; }

        //Propiedades de navegación		
        public Oportunidad Oportunidad { get; set; }
        public Voluntario Voluntario { get; set; }
        public EstadoPostulacion Estado { get; set; }
    }
}



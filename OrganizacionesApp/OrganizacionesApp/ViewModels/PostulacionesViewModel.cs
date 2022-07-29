using MvvmHelpers;
using System;

namespace OrganizacionesApp.ViewModels
{
    public class PostulacionesViewModel : BaseViewModel
    {
        public int PostulacionID { get; set; }
        public int OportunidadID { get; set; }
        public string OportunidadActividad { get; set; }
        public int? VoluntarioID { get; set; }
        public string VoluntarioNombre { get; set; }
        public string VoluntarioApellido { get; set; }
        public byte[] VoluntarioImagen { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public int EstadoPostulacionID { get; set; }
        public string EstadoPostulacionEstado { get; set; }
        public string Mensaje { get; set; }
    }
}

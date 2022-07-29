using MvvmHelpers;
using System;
using OrganizacionesApp.Models;

namespace OrganizacionesApp.ViewModels
{
    public class PostulacionViewModel : BaseViewModel
    {
        public int PostulacionID { get; set; }
        public int OportunidadID { get; set; }
        public Oportunidad Oportunidad { get; set; }
        public string OportunidadActividad { get; set; }
        public int? VoluntarioID { get; set; }
        public string VoluntarioNombre { get; set; }
        public string VoluntarioApellido { get; set; }        
        public byte[] VoluntarioImagen { get; set; }
        public string VoluntarioNombreCompleto => $"{VoluntarioNombre} {VoluntarioApellido}";
        public int VoluntarioLocalidadID { get; set; }
        public string VoluntarioLocalidadTipoLocalidad
        { get { return App.GetLocalidadByID(VoluntarioLocalidadID); } }
        public int VoluntarioProvinciaID { get; set; }
        public string VoluntarioProvinciaNombreProvincia 
        { get { return App.GetProvinciaByID(VoluntarioProvinciaID); } }
        public int VoluntarioPaisID { get; set; }
        public string VoluntarioPaisNombrePais
        { get { return App.GetPaisByID(VoluntarioPaisID); } }
        public int VoluntarioEdad
        { get { return CalcularEdad(VoluntarioFechaNacimiento); } }
        public string VoluntarioMail { get; set; }
        public string VoluntarioTelefono { get; set; }        
        public DateTime VoluntarioFechaNacimiento { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public int EstadopostulacionID { get; set; }
        public string EstadopostulacionEstado { get; set; }        
        public string Mensaje { get; set; }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;

            // Año bisiesto
            if (fechaNacimiento > hoy.AddYears(-edad))
                edad--;

            return edad;
        }
    }
}

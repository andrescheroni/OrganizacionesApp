using MvvmHelpers;
using System;

namespace OrganizacionesApp.ViewModels
{
    public class OportunidadViewModel : BaseViewModel
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
        public int LocalidadID { get; set; }
        public string LocalidadTipoLocalidad { get; set; }
        public int ProvinciaID { get; set; }
        public string ProvinciaNombreProvincia { get; set; }
        public int PaisID { get; set; }
        public string PaisNombrePais { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public bool Baja { get; set; }
    }
}

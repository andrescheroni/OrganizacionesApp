using Xamarin.Essentials;

namespace OrganizacionesApp
{
    public static class Constants
    {
        // URL of REST service
        //public static string RestUrl = "https://YOURPROJECT.azurewebsites.net:8081/api/todoitems/{0}";

        // URL of REST service (Android does not use localhost)
        // Use http cleartext for local deployment. Change to https for production
        // public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:5001/api/voluntarios/{0}" : "http://localhost:5000/api/voluntarios/{0}";
        public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:5001/api" : "http://localhost:5000/api";
        public static string VoluntariosPath = "/voluntarios/{0}";
        public static string OrganizacionesPath = "/organizaciones/{0}";
        public static string PostulacionesPath = "/postulaciones/{0}";
        public static string PostulacionesOportunidadPath = "/postulaciones/oportunidad/{0}";
        public static string OportunidadesPath = "/oportunidades/{0}";
        public static string OportunidadesOrganizacionPath = "/oportunidades/organizacion/{0}";
        public static string EntornosPath = "/entornos/{0}";
        public static string EstadoPostulacionesPath = "/estadopostulaciones/{0}";
        public static string IntensidadesPath = "/intensidades/{0}";
        public static string LocalidadesPath = "/localidades/{0}";
        public static string LocalidadesProvinciaPath = "/localidades/provincia/{0}";
        public static string PaisesPath = "/paises/{0}";
        public static string ProvinciasPath = "/provincias/{0}";
        public static string ProvinciasPaisPath = "/provincias/pais/{0}";
        public static string SocialesPath = "/sociales/{0}";
    }
}
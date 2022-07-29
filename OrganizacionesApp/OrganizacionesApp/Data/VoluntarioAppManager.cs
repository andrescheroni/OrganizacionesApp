using System.Collections.Generic;
using System.Threading.Tasks;
using OrganizacionesApp.Models;
using OrganizacionesApp.ViewModels;

namespace OrganizacionesApp.Data
{
    public class VoluntarioAppManager
    {
        readonly IRestService restService;


        public VoluntarioAppManager(IRestService service)
        {
            restService = service;           
        }

        //Voluntarios
        public Task<List<Voluntario>> GetVoluntarioAllAsync()
        {
            return restService.VoluntarioGetAllAsync();
        }

        public Task<Voluntario> GetVoluntarioByIDAsync(int id)
        {
            return restService.VoluntarioGetByIDAsync(id);
        }

        public Task SaveVoluntarioAsync(Voluntario item, bool isNewItem = false)
        {
            return restService.VoluntarioSaveAsync(item, isNewItem);
        }

        public Task DeleteVoluntarioAsync(Voluntario item)
        {
            return restService.VoluntarioDeleteAsync(item.VolutnarioID);
        }

        //Organizaciones
        public Task<List<Organizacion>> GetOrganizacionAllAsync()
        {
            return restService.OrganizacionGetAllAsync();
        }

        public Task<OrganizacionViewModel> GetOrganizacionByIDAsync(int id)
        {
            return restService.OrganizacionGetByIDAsync(id);
        }

        public Task<bool> SaveOrganizacionAsync(Organizacion item, bool isNewItem = false)
        {
            return restService.OrganizacionSaveAsync(item, isNewItem);
            
        }

        public Task DeleteOrganizacionAsync(Organizacion item)
        {
            return restService.OrganizacionDeleteAsync(item.OrganizacionID);
        }

        //Oportunidades
        public Task<List<OportunidadViewModel>> GetOportunidadAllAsync(bool Activas)
        {
            return restService.OportunidadGetAllAsync(Activas);
        }

        public Task<List<OportunidadViewModel>> GetOportunidadByOrganizacionIDAsync(int id, bool Activas)
        {
            return restService.OportunidadGetByOrganizacionIDAsync(id, Activas);
        }

        public Task<OportunidadViewModel> GetOportunidadByIDAsync(int id)
        {
            return restService.OportunidadGetByIDAsync(id);
        }

        public Task SaveOportunidadAsync(Oportunidad item, bool isNewItem = false)
        {
            if (item.Social != null)
            {
                item.SocialID = item.Social.SocialID;
            }
            if (item.Intensidad != null)
            {
                item.IntensidadID = item.Intensidad.IntensidadID;
            }
            if (item.Entorno != null)
            {
                item.EntornoID = item.Entorno.EntornoID;
            }
            if (item.Localidad != null)
            {
                item.LocalidadID = item.Localidad.LocalidadID;
            }
            if (item.Provincia != null)
            {
                item.ProvinciaID = item.Provincia.ProvinciaID;
            }
            if (item.Pais != null)
            {
                item.PaisID = item.Pais.PaisID;
            }
            if (item.Pais != null)
            {
                item.PaisID = item.Pais.PaisID;
            }
            if (item.FechaInicio < System.DateTime.Today)
            {
                item.FechaInicio = System.DateTime.Today;
            }
            if (item.FechaFin < System.DateTime.Today)
            {
                item.FechaFin = System.DateTime.Today;
            }

            item.FechaPublicacion = System.DateTime.Today;
            item.OrganizacionID = 5; // Para testeo, reemplazar luego con organizacionID real

            return restService.OportunidadSaveAsync(item, isNewItem);
        }

        public Task DeleteOportunidadAsync(Oportunidad item)
        {
            return restService.OportunidadDeleteAsync(item.OportunidadID);
        }

        public Task DownOportunidadAsync(int id)
        {
            return restService.OportunidadDownAsync(id);
        }

        //Postulaciones
        public Task<List<PostulacionViewModel>> GetPostulacionAllAsync()
        {   
            return restService.PostulacionGetAllAsync();
        }

        public Task<PostulacionViewModel> GetPostulacionByIDAsync(int id)
        {
            return restService.PostulacionGetByIDAsync(id);
        }

        public Task<List<PostulacionViewModel>> GetPostulacionByOportunidadIDAllAsync(int id)
        {
            return restService.PostulacionGetByOportunidadIDAsync(id);
        }

        public Task SavePostulacionAsync(Postulacion item, bool isNewItem = false)
        {
            return restService.PostulacionSaveAsync(item, isNewItem);
        }

        public Task DeletePostulacionAsync(Postulacion item)
        {
            return restService.PostulacionDeleteAsync(item.PostulacionID);
        }

        public Task ChangeStatusPostulacionAsync(int item, int status, string mensaje)
        {
            return restService.PostulacionChangeStatusAsync(item, status, mensaje);
        }

        //Entornos
        public Task<List<Entorno>> GetEntornoAllAsync()
        {
            return restService.EntornoGetAllAsync();
        }

        public Task<Entorno> GetEntornoByIDAsync(int id)
        {
            return restService.EntornoGetByIDAsync(id);
        }

        //Intensidades
        public Task<List<Intensidad>> GetIntensidadAllAsync()
        {
            return restService.IntensidadGetAllAsync();
        }

        public Task<Intensidad> GetIntensidadByIDAsync(int id)
        {
            return restService.IntensidadGetByIDAsync(id);
        }

        //Sociales
        public Task<List<Social>> GetSocialAllAsync()
        {
            return restService.SocialGetAllAsync();
        }

        public Task<Social> GetSocialByIDAsync(int id)
        {
            return restService.SocialGetByIDAsync(id);
        }

        //Localidades
        public Task<List<Localidad>> GetLocalidadAllAsync()
        {
            return restService.LocalidadGetAllAsync();
        }

        public Task<List<Localidad>> GetLocalidadByProvinciaAllAsync(int id)
        {
            return restService.LocalidadByProvinciaGetAllAsync(id);
        }

        public Task<Localidad> GetLocalidadByIDAsync(int id)
        {
            return restService.LocalidadGetByIDAsync(id);
        }

        //Provincias
        public Task<List<Provincia>> GetProvinciaAllAsync()
        {
            return restService.ProvinciaGetAllAsync();
        }

        public Task<List<Provincia>> GetProvinciaByPaisAllAsync(int id)
        {
            return restService.ProvinciaByPaisGetAllAsync(id);
        }

        public Task<Provincia> GetProvinciaByIDAsync(int id)
        {
            return restService.ProvinciaGetByIDAsync(id);
        }

        //Paises
        public Task<List<Pais>> GetPaisAllAsync()
        {
            return restService.PaisGetAllAsync();
        }

        public Task<Pais> GetPaisByIDAsync(int id)
        {
            return restService.PaisGetByIDAsync(id);
        }

        //EstadoPostulaciones
        public Task<List<EstadoPostulacion>> GetEstadoPostulacionAllAsync()
        {
            return restService.EstadoPostulacionGetAllAsync();
        }

        public Task<EstadoPostulacion> GetEstadoPostulacionByIDAsync(int id)
        {
            return restService.EstadoPostulacionGetByIDAsync(id);
        }


    }
}
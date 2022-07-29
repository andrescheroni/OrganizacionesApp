using System.Collections.Generic;
using System.Threading.Tasks;
using OrganizacionesApp.Models;
using OrganizacionesApp.ViewModels;

namespace OrganizacionesApp.Data
{
    public interface IRestService
    {
        //Voluntarios
        Task<List<Voluntario>> VoluntarioGetAllAsync();

        Task<Voluntario> VoluntarioGetByIDAsync(int id);

        Task<bool> VoluntarioSaveAsync(Voluntario item, bool isNewItem);

        Task VoluntarioDeleteAsync(int id);

        //Organizaciones
        Task<List<Organizacion>> OrganizacionGetAllAsync();

        Task<OrganizacionViewModel> OrganizacionGetByIDAsync(int id);

        Task<bool> OrganizacionSaveAsync(Organizacion item, bool isNewItem);

        Task OrganizacionDeleteAsync(int id);

        //Oportunidades
        Task<List<OportunidadViewModel>> OportunidadGetAllAsync(bool Activas);

        Task<OportunidadViewModel> OportunidadGetByIDAsync(int id);

        Task<List<OportunidadViewModel>> OportunidadGetByOrganizacionIDAsync(int id, bool Activas);

        Task<bool> OportunidadSaveAsync(Oportunidad item, bool isNewItem);

        Task OportunidadDeleteAsync(int id);

        Task OportunidadDownAsync(int id);

        //Postulaciones
        Task<List<PostulacionViewModel>> PostulacionGetAllAsync();

        Task<PostulacionViewModel> PostulacionGetByIDAsync(int id);

        Task<List<PostulacionViewModel>> PostulacionGetByOportunidadIDAsync(int id);
        
        Task<bool> PostulacionSaveAsync(Postulacion item, bool isNewItem);

        Task PostulacionDeleteAsync(int id);

        Task PostulacionChangeStatusAsync(int id, int estado, string mensaje);

        //Entornos
        Task<List<Entorno>> EntornoGetAllAsync();

        Task<Entorno> EntornoGetByIDAsync(int id);

        //Intensidades
        Task<List<Intensidad>> IntensidadGetAllAsync();

        Task<Intensidad> IntensidadGetByIDAsync(int id);

        //Sociales
        Task<List<Social>> SocialGetAllAsync();

        Task<Social> SocialGetByIDAsync(int id);

        //Localidades
        Task<List<Localidad>> LocalidadGetAllAsync();

        Task<List<Localidad>> LocalidadByProvinciaGetAllAsync(int id);

        Task<Localidad> LocalidadGetByIDAsync(int id);

        //Provincias
        Task<List<Provincia>> ProvinciaGetAllAsync();

        Task<List<Provincia>> ProvinciaByPaisGetAllAsync(int id);

        Task<Provincia> ProvinciaGetByIDAsync(int id);

        //Paises
        Task<List<Pais>> PaisGetAllAsync();

        Task<Pais> PaisGetByIDAsync(int id);

        //EstadoPostulaciones
        Task<List<EstadoPostulacion>> EstadoPostulacionGetAllAsync();

        Task<EstadoPostulacion> EstadoPostulacionGetByIDAsync(int id);
    }
}

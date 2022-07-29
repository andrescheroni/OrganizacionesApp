using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OrganizacionesApp.Models;
using OrganizacionesApp.ViewModels;

namespace OrganizacionesApp.Data
{
    public class RestService : IRestService
    {
        readonly HttpClient client;
        readonly JsonSerializerSettings serializerSettings;

        public RestService()
        {
            HttpClientHandler handler = GetInsecureHandler();
            client = new HttpClient(handler);
            serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            };
            return handler;
        }

        //Voluntarios
        public async Task<List<Voluntario>> VoluntarioGetAllAsync()
        {
            List<Voluntario> VoluntarioItems = new List<Voluntario>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.VoluntariosPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    VoluntarioItems = JsonConvert.DeserializeObject<List<Voluntario>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return VoluntarioItems;
        }

        public async Task<Voluntario> VoluntarioGetByIDAsync(int id)
        {
            Voluntario VoluntarioItem = new Voluntario();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.VoluntariosPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    VoluntarioItem = JsonConvert.DeserializeObject<Voluntario>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return VoluntarioItem;
        }

        public async Task<bool> VoluntarioSaveAsync(Voluntario item, bool isNewItem)
        {
            bool status = false;

            try
            {
                string json = JsonConvert.SerializeObject(item, serializerSettings);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.VoluntariosPath, string.Empty));
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.VoluntariosPath, item.VolutnarioID));
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Item successfully saved.");
                    status = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return status;
        }

        public Task VoluntarioDeleteAsync(int id)
        {
            throw new NotImplementedException();
            //Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.VoluntariosPath, id));

            //try
            //{
            //    HttpResponseMessage response = await client.DeleteAsync(uri);
            //
            //    if (response.IsSuccessStatusCode)
            //    {
            //        Debug.WriteLine(@"\Item successfully deleted.");
            //    }
            //
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(@"\tERROR {0}", ex.Message);
            //}
        }

        //Organizaciones
        public async Task<List<Organizacion>> OrganizacionGetAllAsync()
        {
            List<Organizacion> OrganizacionItems = new List<Organizacion>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OrganizacionesPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    OrganizacionItems = JsonConvert.DeserializeObject<List<Organizacion>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return OrganizacionItems;
        }

        public async Task<OrganizacionViewModel> OrganizacionGetByIDAsync(int id)
        {
            OrganizacionViewModel OrganizacionItem = new OrganizacionViewModel();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OrganizacionesPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    OrganizacionItem = JsonConvert.DeserializeObject<OrganizacionViewModel>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return OrganizacionItem;
        }

        public async Task<bool> OrganizacionSaveAsync(Organizacion item, bool isNewItem)
        {
            bool status = false;

            try
            {
                string json = JsonConvert.SerializeObject(item, serializerSettings);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");                

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OrganizacionesPath, string.Empty));
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OrganizacionesPath, item.OrganizacionID));
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {                    
                    Debug.WriteLine(@"\Item successfully saved.");
                    status = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return status;
        }

        public Task OrganizacionDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        //Oportunidades

        public async Task<List<OportunidadViewModel>> OportunidadGetAllAsync(bool Activas)
        {
            List<OportunidadViewModel> OportunidadItems = new List<OportunidadViewModel>();
            List<OportunidadViewModel> OportunidadItemsFiltrada = new List<OportunidadViewModel>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OportunidadesPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    OportunidadItems = JsonConvert.DeserializeObject<List<OportunidadViewModel>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            foreach (var oportunidad in OportunidadItems)
            {
                if(((oportunidad.FechaFin.Date < DateTime.Today.Date) || oportunidad.Baja) != Activas)
                {
                    OportunidadItemsFiltrada.Add(oportunidad);
                }
            }

            return OportunidadItemsFiltrada;
        }

        public async Task<OportunidadViewModel> OportunidadGetByIDAsync(int id)
        {
            OportunidadViewModel OportunidadItem = new OportunidadViewModel();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OportunidadesPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    OportunidadItem = JsonConvert.DeserializeObject<OportunidadViewModel>(content, serializerSettings);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return OportunidadItem;
        }

        public async Task<List<OportunidadViewModel>> OportunidadGetByOrganizacionIDAsync(int id, bool Activas)
        {
            List<OportunidadViewModel> OportunidadItems = new List<OportunidadViewModel>();
            List<OportunidadViewModel> OportunidadItemsFiltrada = new List<OportunidadViewModel>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OportunidadesOrganizacionPath, id));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    OportunidadItems = JsonConvert.DeserializeObject<List<OportunidadViewModel>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            foreach (var oportunidad in OportunidadItems)
            {
                if (((oportunidad.FechaFin.Date < DateTime.Today.Date) || oportunidad.Baja) != Activas)
                {
                    OportunidadItemsFiltrada.Add(oportunidad);
                }
            }

            return OportunidadItemsFiltrada;
        }

        public async Task<bool> OportunidadSaveAsync(Oportunidad item, bool isNewItem)
        {
            bool status = false;

            try
            {
                string json = JsonConvert.SerializeObject(item, serializerSettings);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OportunidadesPath, string.Empty));
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OportunidadesPath, item.OportunidadID));
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Item successfully saved.");
                    status = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return status;
        }

        public Task OportunidadDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task OportunidadDownAsync(int id)
        {
            try
           {                                
                string jsonpatch = "[{'op': 'replace', 'path': '/Baja', 'value': 'True'}]";
                StringContent content = new StringContent(jsonpatch, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;                

                Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.OportunidadesPath, id));

                var request =
                    new HttpRequestMessage(new HttpMethod("PATCH"), uri)
                    {
                        Content = content
                    };

                response = await client.SendAsync(request);                

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Item successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        //Postulaciones
        public async Task<List<PostulacionViewModel>> PostulacionGetAllAsync()
        {
            List<PostulacionViewModel> PostulacionItems = new List<PostulacionViewModel>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.PostulacionesPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    PostulacionItems = JsonConvert.DeserializeObject<List<PostulacionViewModel>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return PostulacionItems;
        }

        public async Task<PostulacionViewModel> PostulacionGetByIDAsync(int id)
        {
            PostulacionViewModel PostulacionItem = new PostulacionViewModel();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.PostulacionesPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    PostulacionItem = JsonConvert.DeserializeObject<PostulacionViewModel>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return PostulacionItem;
        }

        public async Task<List<PostulacionViewModel>> PostulacionGetByOportunidadIDAsync(int id)
        {
            List<PostulacionViewModel> PostulacionItems = new List<PostulacionViewModel>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.PostulacionesOportunidadPath, id));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    PostulacionItems = JsonConvert.DeserializeObject<List<PostulacionViewModel>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return PostulacionItems;
        }

        public async Task<bool> PostulacionSaveAsync(Postulacion item, bool isNewItem)
        {
            bool status = false;

            try
            {
                string json = JsonConvert.SerializeObject(item, serializerSettings);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.PostulacionesPath, string.Empty));
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.PostulacionesPath, item.PostulacionID));
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Item successfully saved.");
                    status = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return status;
        }

        public Task PostulacionDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task PostulacionChangeStatusAsync(int id, int estado, string mensaje)
        {
            try
            {
                string jsonpatch = "[{'op': 'replace', 'path': '/EstadoPostulacionID', 'value': '" + estado + "'}, {'op': 'replace', 'path': '/mensaje', 'value': '" + mensaje + "'}]";
                StringContent content = new StringContent(jsonpatch, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.PostulacionesPath, id));

                var request =
                    new HttpRequestMessage(new HttpMethod("PATCH"), uri)
                    {
                        Content = content
                    };

                response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Item successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        //Entornos
        public async Task<List<Entorno>> EntornoGetAllAsync()
        {
            List<Entorno> EntornoItems = new List<Entorno>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.EntornosPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EntornoItems = JsonConvert.DeserializeObject<List<Entorno>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return EntornoItems;
        }

        public async Task<Entorno> EntornoGetByIDAsync(int id)
        {
            Entorno EntornoItem = new Entorno();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.EntornosPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EntornoItem = JsonConvert.DeserializeObject<Entorno>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return EntornoItem;
        }

        //Intensidades
        public async Task<List<Intensidad>> IntensidadGetAllAsync()
        {
            List<Intensidad> IntensidadItems = new List<Intensidad>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.IntensidadesPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    IntensidadItems = JsonConvert.DeserializeObject<List<Intensidad>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return IntensidadItems;
        }

        public async Task<Intensidad> IntensidadGetByIDAsync(int id)
        {
            Intensidad IntensidadItem = new Intensidad();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.IntensidadesPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    IntensidadItem = JsonConvert.DeserializeObject<Intensidad>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return IntensidadItem;
        }

        //Sociales
        public async Task<List<Social>> SocialGetAllAsync()
        {
            List<Social> SocialItems = new List<Social>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.SocialesPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    SocialItems = JsonConvert.DeserializeObject<List<Social>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return SocialItems;
        }

        public async Task<Social> SocialGetByIDAsync(int id)
        {
            Social SocialItem = new Social();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.SocialesPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    SocialItem = JsonConvert.DeserializeObject<Social>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return SocialItem;
        }

        //Localidades
        public async Task<List<Localidad>> LocalidadGetAllAsync()
        {
            List<Localidad> LocalidadItems = new List<Localidad>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.LocalidadesPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    LocalidadItems = JsonConvert.DeserializeObject<List<Localidad>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return LocalidadItems;
        }

        public async Task<List<Localidad>> LocalidadByProvinciaGetAllAsync(int id)
        {
            List<Localidad> LocalidadItems = new List<Localidad>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.LocalidadesProvinciaPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    LocalidadItems = JsonConvert.DeserializeObject<List<Localidad>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return LocalidadItems;
        }

        public async Task<Localidad> LocalidadGetByIDAsync(int id)
        {
            Localidad LocalidadItem = new Localidad();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.LocalidadesPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    LocalidadItem = JsonConvert.DeserializeObject<Localidad>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return LocalidadItem;
        }

        //Provincias
        public async Task<List<Provincia>> ProvinciaGetAllAsync()
        {
            List<Provincia> ProvinciaItems = new List<Provincia>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.ProvinciasPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ProvinciaItems = JsonConvert.DeserializeObject<List<Provincia>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ProvinciaItems;
        }

        public async Task<List<Provincia>> ProvinciaByPaisGetAllAsync(int id)
        {
            List<Provincia> ProvinciaItems = new List<Provincia>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.ProvinciasPaisPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ProvinciaItems = JsonConvert.DeserializeObject<List<Provincia>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ProvinciaItems;
        }

        public async Task<Provincia> ProvinciaGetByIDAsync(int id)
        {
            Provincia ProvinciaItem = new Provincia();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.ProvinciasPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ProvinciaItem = JsonConvert.DeserializeObject<Provincia>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return ProvinciaItem;
        }

        //Paises
        public async Task<List<Pais>> PaisGetAllAsync()
        {
            List<Pais> PaisItems = new List<Pais>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.PaisesPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    PaisItems = JsonConvert.DeserializeObject<List<Pais>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return PaisItems;
        }

        public async Task<Pais> PaisGetByIDAsync(int id)
        {
            Pais PaisItem = new Pais();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.PaisesPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    PaisItem = JsonConvert.DeserializeObject<Pais>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return PaisItem;
        }

        //EstadoPostulaciones
        public async Task<List<EstadoPostulacion>> EstadoPostulacionGetAllAsync()
        {
            List<EstadoPostulacion> EstadoPostulacionItems = new List<EstadoPostulacion>();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.EstadoPostulacionesPath, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EstadoPostulacionItems = JsonConvert.DeserializeObject<List<EstadoPostulacion>>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return EstadoPostulacionItems;
        }

        public async Task<EstadoPostulacion> EstadoPostulacionGetByIDAsync(int id)
        {
            EstadoPostulacion EstadoPostulacionItem = new EstadoPostulacion();

            Uri uri = new Uri(string.Format(Constants.RestUrl + Constants.EstadoPostulacionesPath, id.ToString()));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    EstadoPostulacionItem = JsonConvert.DeserializeObject<EstadoPostulacion>(content, serializerSettings);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return EstadoPostulacionItem;
        }

    }
}

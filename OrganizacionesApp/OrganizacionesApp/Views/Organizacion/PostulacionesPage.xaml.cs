
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrganizacionesApp.Models;
using OrganizacionesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostulacionesPage : ContentPage
    {
        readonly int OrgID = 5;
        readonly OportunidadViewModel OportunidadIndividual;

        public PostulacionesPage()
        {
            InitializeComponent();
        }

        public PostulacionesPage(OportunidadViewModel oportunidad)
        {
            InitializeComponent();
            OportunidadIndividual = oportunidad;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await RefreshOportunidades();            
        }

        async Task RefreshPostulaciones(int id)
        {           
            if (id != -1) {
                
                List<PostulacionViewModel> listadoPostulaciones = await App.VoluntarioManager.GetPostulacionByOportunidadIDAllAsync(id);
                List<PostulacionViewModel> listadoPostulacionesFiltrado = new List<PostulacionViewModel>();

                foreach (PostulacionViewModel postulacion in listadoPostulaciones)
                {               
                    switch (postulacion.EstadopostulacionID)
                    {
                        case 1:
                            postulacion.EstadopostulacionEstado = "Pendiente";                                                        
                            listadoPostulacionesFiltrado.Add(postulacion);                            
                            break;
                        case 2:
                            postulacion.EstadopostulacionEstado = "Aceptada";
                            if (!pendientesCheckBox.IsChecked)
                            {
                                listadoPostulacionesFiltrado.Add(postulacion);
                            }
                            break;
                        case 3:
                            postulacion.EstadopostulacionEstado = "Rechazada";
                            if (!pendientesCheckBox.IsChecked)
                            {
                                listadoPostulacionesFiltrado.Add(postulacion);
                            }
                            break;
                    }
                }
                    listaPostulaciones.ItemsSource = listadoPostulacionesFiltrado;
            }            
        }

        async Task RefreshOportunidades()
        {
            List<OportunidadViewModel> listadoOportunidades = new List<OportunidadViewModel>();

            if (OportunidadIndividual == null)
            {
                listadoOportunidades = await App.VoluntarioManager.GetOportunidadByOrganizacionIDAsync(OrgID, true);                
            }
            else
            {
                listadoOportunidades.Add(OportunidadIndividual);
            }
            
            oportunidadesPicker.ItemsSource = listadoOportunidades;
            
            if (listadoOportunidades.Count != 0)
            {
                oportunidadesPicker.SelectedIndex = 0;
            }

        }

        private async void OnOportunidadesPickerSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (oportunidadesPicker.SelectedIndex != -1)
            {
                await RefreshPostulaciones(((OportunidadViewModel)oportunidadesPicker.SelectedItem).OportunidadID);
            }            
        }

        private async void OnPendientesCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (oportunidadesPicker.SelectedIndex != -1)
            {
                await RefreshPostulaciones(((OportunidadViewModel)oportunidadesPicker.SelectedItem).OportunidadID);
            }
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new PostulacionPage
            {                
                BindingContext = e.CurrentSelection.SingleOrDefault() as PostulacionViewModel
            });
        }
    }
}
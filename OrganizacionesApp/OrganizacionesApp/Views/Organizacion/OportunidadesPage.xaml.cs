using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OrganizacionesApp.Models;
using OrganizacionesApp.Services;
using OrganizacionesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OportunidadesPage : ContentPage    {

        byte[] imageHolder;
        public OportunidadesPage()
        {
            InitializeComponent();
            FechaInicioDatePicker.MinimumDate = DateTime.Today.AddDays(-1);                        
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            await RefreshOportunidades();
            
            EntornoPicker.ItemsSource = await App.VoluntarioManager.GetEntornoAllAsync();
            IntensidadPicker.ItemsSource = await App.VoluntarioManager.GetIntensidadAllAsync();
            SocialPicker.ItemsSource = await App.VoluntarioManager.GetSocialAllAsync();
            PaisPicker.ItemsSource = await App.VoluntarioManager.GetPaisAllAsync();            
        }

        async Task RefreshOportunidades()
        {
            listaOportunidadesActivas.ItemsSource = await App.VoluntarioManager.GetOportunidadAllAsync(true);
            listaOportunidadesFinalizadas.ItemsSource = await App.VoluntarioManager.GetOportunidadAllAsync(false);
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new OportunidadPage
            {
                BindingContext = e.CurrentSelection.FirstOrDefault() as OportunidadViewModel
            });
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {               
            var oportunidadItem = (Oportunidad)BindingContext;
            
            if (imageHolder == null)
            {
                oportunidadItem.Imagen = null;
            }
            else
            {
                oportunidadItem.Imagen = imageHolder;
            }
            
            await App.VoluntarioManager.SaveOportunidadAsync(oportunidadItem, true);
            await RefreshOportunidades();
            await Navigation.PopAsync();
        }

        void OnClearButtonClicked(object sender, EventArgs e)
        {              
            ActividadEntry.Text = string.Empty;
            DescripcionEntry.Text = string.Empty;
            RolEntry.Text = string.Empty;
            EntornoPicker.SelectedIndex = -1;
            IntensidadPicker.SelectedIndex = -1;
            SocialPicker.SelectedIndex = -1;
            FechaInicioDatePicker.Date = DateTime.Today;
            FechaFinDatePicker.Date = DateTime.Today;
            PaisPicker.SelectedIndex = -1;
            ProvinciaPicker.SelectedIndex = -1;
            LocalidadPicker.SelectedIndex = -1;
            imagen.Source = ImageSource.FromResource("OrganizacionesApp.Resources.Images.noimage.png");
            imageHolder = null;
        }

        void OnAddItemClicked(object sender, EventArgs e)
        {
            BindingContext = new Oportunidad();
        }

        async void OnSelectedPais(object sender, EventArgs e)
        {
            if (PaisPicker.SelectedIndex != -1)
            {
                int id = ((Pais)PaisPicker.SelectedItem).PaisID;
                ProvinciaPicker.ItemsSource = await App.VoluntarioManager.GetProvinciaByPaisAllAsync(id);
            }
        }

        async void OnSelectedProvincia(object sender, EventArgs e)
        {
            if (ProvinciaPicker.SelectedIndex != -1)
            {
                int id = ((Provincia)ProvinciaPicker.SelectedItem).ProvinciaID;
                LocalidadPicker.ItemsSource = await App.VoluntarioManager.GetLocalidadByProvinciaAllAsync(id);
            }            
        }

        async void OnImageTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            Stream imageStream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();            

            if (imageStream != null)
            {
                imageHolder = ReadFully(imageStream);                
                imagen.Source = ImageSource.FromStream(() => new MemoryStream(imageHolder));                                
            }

            (sender as Button).IsEnabled = true;
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }

        }

    }
}
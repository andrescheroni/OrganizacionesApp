using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizacionesApp.Models;
using OrganizacionesApp.Services;
using OrganizacionesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OportunidadEditPage : ContentPage
    {
        byte[] imageHolder;
        
        public OportunidadEditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {            
            base.OnAppearing();
            var oportunidadItem = (OportunidadViewModel)BindingContext;
            PopulatePickers(oportunidadItem);
        }

        void PopulatePickers(OportunidadViewModel oportunidadItem)
        {
            var entornos = App.EntornoList;

            EntornoPicker.ItemsSource = entornos;
            EntornoPicker.SelectedIndex = 0;

            foreach (var entorno in entornos.Select((value, i) => new { i, value }))
            {
                var value = entorno.value;
                var index = entorno.i;

                if (value.EntornoID == oportunidadItem.EntornoID)
                {                    
                    EntornoPicker.SelectedIndex = index;                    
                }
            }

            IntensidadPicker.ItemsSource = App.IntensidadList;

            foreach (var intensidad in App.IntensidadList.Select((value, i) => new { i, value }))
            {
                var value = intensidad.value;
                var index = intensidad.i;

                if (value.IntensidadID == oportunidadItem.IntensidadID)
                {
                    IntensidadPicker.SelectedIndex = index;
                }
            }

            SocialPicker.ItemsSource = App.SocialList;

            foreach (var social in App.SocialList.Select((value, i) => new { i, value }))
            {
                var value = social.value;
                var index = social.i;

                if (value.SocialID == oportunidadItem.SocialID)
                {
                    SocialPicker.SelectedIndex = index;
                }
            }

            PaisPicker.ItemsSource = App.PaisList;

            foreach (var pais in App.PaisList.Select((value, i) => new { i, value }))
            {
                var value = pais.value;
                var index = pais.i;

                if (value.PaisID == oportunidadItem.PaisID)
                {
                    PaisPicker.SelectedIndex = index;
                }
            }

            var provincias = App.GetProvinciaByPaisID(oportunidadItem.PaisID);
            //ProvinciaPicker.ItemsSource = provincias;

            foreach (var provincia in provincias.Select((value, i) => new { i, value }))
            {
                var value = provincia.value;
                var index = provincia.i;

                if (value.ProvinciaID == oportunidadItem.ProvinciaID)
                {
                    ProvinciaPicker.SelectedIndex = index;
                }
            }

            var localidades = App.GetLocalidadByProvinciaID(oportunidadItem.ProvinciaID);
            //LocalidadPicker.ItemsSource = localidades;

            foreach (var localidad in localidades.Select((value, i) => new { i, value }))
            {
                var value = localidad.value;
                var index = localidad.i;

                if (value.LocalidadID == oportunidadItem.LocalidadID)
                {
                    LocalidadPicker.SelectedIndex = index;
                }
            }

        }

        void OnSelectedPais(object sender, EventArgs e)
        {
            if (PaisPicker.SelectedIndex != -1)
            {
                int id = ((Pais)PaisPicker.SelectedItem).PaisID;
                ProvinciaPicker.ItemsSource = App.GetProvinciaByPaisID(id);
            }
        }

        void OnSelectedProvincia(object sender, EventArgs e)
        {
            if (ProvinciaPicker.SelectedIndex != -1)
            {
                int id = ((Provincia)ProvinciaPicker.SelectedItem).ProvinciaID;
                LocalidadPicker.ItemsSource = App.GetLocalidadByProvinciaID(id);
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

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var currentContext = (OportunidadViewModel)BindingContext;

            var oportunidadItem = new Oportunidad
            {
                OportunidadID = currentContext.OportunidadID,
                OrganizacionID = currentContext.OrganizacionID,
                OrganizacionRazonSocial = currentContext.OrganizacionRazonSocial,
                Actividad = ActividadEntry.Text,
                Descripcion = DescripcionEntry.Text,
                Rol = RolEntry.Text,
                EntornoID = ((Entorno)EntornoPicker.SelectedItem).EntornoID,
                EntornoTipoEntorno = ((Entorno)EntornoPicker.SelectedItem).TipoEntorno,
                IntensidadID = ((Intensidad)IntensidadPicker.SelectedItem).IntensidadID,
                IntensidadTipoIntensidad = ((Intensidad)IntensidadPicker.SelectedItem).TipoIntensidad,
                SocialID = ((Social)SocialPicker.SelectedItem).SocialID,
                SocialTipoSocial = ((Social)SocialPicker.SelectedItem).TipoSocial,
                FechaInicio = FechaInicioDatePicker.Date,
                FechaFin = FechaFinDatePicker.Date,
                LocalidadID = ((Localidad)LocalidadPicker.SelectedItem).LocalidadID,
                LocalidadTipoLocalidad = ((Localidad)LocalidadPicker.SelectedItem).TipoLocalidad,
                ProvinciaID = ((Provincia)ProvinciaPicker.SelectedItem).ProvinciaID,
                ProvinciaNombreProvincia = ((Provincia)ProvinciaPicker.SelectedItem).NombreProvincia,
                PaisID = ((Pais)PaisPicker.SelectedItem).PaisID,
                PaisNombrePais = ((Pais)PaisPicker.SelectedItem).NombrePais,
                FechaPublicacion = System.DateTime.Today,
                Baja = false
            };

            if (imageHolder == null)
            {
                oportunidadItem.Imagen = null;
            }
            else
            {
                oportunidadItem.Imagen = imageHolder;
            }

            await App.VoluntarioManager.SaveOportunidadAsync(oportunidadItem, false);            
            await Navigation.PopAsync();
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {            
            await Navigation.PopAsync();
        }
    }
}
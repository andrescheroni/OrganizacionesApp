using System;
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
    public partial class PerfilGeneralPage : ContentPage
    {
        byte[] imageHolder;
        readonly int OrgID = 5;
        public PerfilGeneralPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            OrganizacionViewModel organizacionItem = await App.VoluntarioManager.GetOrganizacionByIDAsync(OrgID);

            BindingContext = organizacionItem;

            imageHolder = organizacionItem.Imagen;

            PopulatePickers(organizacionItem);
        }

        void PopulatePickers(OrganizacionViewModel organizacionItem)
        {
                        
            PaisPicker.ItemsSource = App.PaisList;

            foreach (var pais in App.PaisList.Select((value, i) => new { i, value }))
            {
                var value = pais.value;
                var index = pais.i;

                if (value.PaisID == organizacionItem.PaisID)
                {
                    PaisPicker.SelectedIndex = index;
                }
            }
                        
            var provincias = App.GetProvinciaByPaisID(organizacionItem.PaisID);
            
            foreach (var provincia in provincias.Select((value, i) => new { i, value }))
            {
                var value = provincia.value;
                var index = provincia.i;

                if (value.ProvinciaID == organizacionItem.ProvinciaID)
                {
                    ProvinciaPicker.SelectedIndex = index;
                }
            }

            var localidades = App.GetLocalidadByProvinciaID(organizacionItem.ProvinciaID);
            
            foreach (var localidad in localidades.Select((value, i) => new { i, value }))
            {
                var value = localidad.value;
                var index = localidad.i;

                if (value.LocalidadID == organizacionItem.LocalidadID)
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
                ProvinciaPicker.ItemsSource = App.GetProvinciaByPaisID(id); //await App.VoluntarioManager.GetProvinciaByPaisAllAsync(id);
            }
        }

        void OnSelectedProvincia(object sender, EventArgs e)
        {
            if (ProvinciaPicker.SelectedIndex != -1)
            {
                int id = ((Provincia)ProvinciaPicker.SelectedItem).ProvinciaID;
                LocalidadPicker.ItemsSource = App.GetLocalidadByProvinciaID(id); //await App.VoluntarioManager.GetLocalidadByProvinciaAllAsync(id);
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
            bool status;

            var currentContext = (OrganizacionViewModel)BindingContext;

            var organizacionItem = new Models.Organizacion
            {
                OrganizacionID = currentContext.OrganizacionID,                
                RazonSocial = currentContext.RazonSocial,
                Descripcion = currentContext.Descripcion,
                CUIT = currentContext.CUIT,
                NombreResponsable = currentContext.NombreResponsable,
                ApellidoResponsable = currentContext.ApellidoResponsable,
                DNIResponsable = currentContext.DNIResponsable,
                Direccion = currentContext.Direccion,
                LocalidadID = currentContext.LocalidadID,
                ProvinciaID = currentContext.ProvinciaID,
                PaisID = currentContext.PaisID,
                Mail = currentContext.Mail,
                PasswordHash = currentContext.PasswordHash,
                Telefono = currentContext.Telefono,
                Verificacion = currentContext.Verificacion,
                FechaRegistro = currentContext.FechaRegistro,
                FechaVerificacion = currentContext.FechaVerificacion
            };

            if (imageHolder == null)
            {
                organizacionItem.Imagen = null;
            }
            else
            {
                organizacionItem.Imagen = imageHolder;
            }
            
            status = await App.VoluntarioManager.SaveOrganizacionAsync(organizacionItem, false);

            if (status != true)
            {
                await DisplayAlert("Error", "Ocurrió un error al grabar", "OK");
            } else
            {
                await DisplayAlert("Exito", "Las modificaciones se grabaron con éxito", "OK");
            }            

            await Navigation.PopAsync();            
        }
               


        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnChangePasswordButtonClicked(object sender, EventArgs e)
        {
            var currentContext = (OrganizacionViewModel)BindingContext;

            string currentPassword = await DisplayPromptAsync("Cambiar password", "Ingrese su password actual");

            if (PasswordHasher.HashPassword(currentPassword) != currentContext.PasswordHash)
            {
                await DisplayAlert("Error", "El password no es correcto", "OK");
                return;
            }

            string newPassword = await DisplayPromptAsync("Cambiar password", "Ingrese su nuevo password");

            string newPasswordCheck = await DisplayPromptAsync("Cambiar password", "Ingrese nuevamente su nuevo password");

            if (newPassword != newPasswordCheck)
            {
                await DisplayAlert("Error", "Los passwords no coinciden", "OK");
                return;
            }

            currentContext.PasswordHash = PasswordHasher.HashPassword(newPassword);
        }
    }
}
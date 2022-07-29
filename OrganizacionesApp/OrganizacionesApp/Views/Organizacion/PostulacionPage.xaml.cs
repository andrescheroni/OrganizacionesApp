using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizacionesApp.Models;
using OrganizacionesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostulacionPage : ContentPage
    {
        public PostulacionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {            
            base.OnAppearing();
            var postulacionItem = (PostulacionViewModel)BindingContext;
            if (postulacionItem.EstadopostulacionID != 1)
            {
                buttonAceptar.IsEnabled = false;
                buttonRechazar.IsEnabled = false;
                mensaje.IsEnabled = false;
            }
        }

        private async void OnAcceptButtonClicked(object sender, EventArgs e)
        {
            var postulacionItem = (PostulacionViewModel)BindingContext;

            bool answer = await DisplayAlert("Alerta", "¿Está seguro de que quiere aceptar esta Postulación? (Esta acción no se puede deshacer)", "Si", "No");

            if (answer)
            {
                await App.VoluntarioManager.ChangeStatusPostulacionAsync(postulacionItem.PostulacionID, 2, mensaje.Text);
                await Navigation.PopAsync();
            }
        }

        private async void OnRejectButtonClicked(object sender, EventArgs e)
        {
            var postulacionItem = (PostulacionViewModel)BindingContext;

            bool answer = await DisplayAlert("Alerta", "¿Está seguro de que quiere rechazar esta Postulación? (Esta acción no se puede deshacer)", "Si", "No");

            if (answer)
            {
                await App.VoluntarioManager.ChangeStatusPostulacionAsync(postulacionItem.PostulacionID, 3, mensaje.Text);
                await Navigation.PopAsync();
            }
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

using System;
using OrganizacionesApp.Models;
using OrganizacionesApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views.Organizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OportunidadPage : ContentPage
    {

        OportunidadViewModel oportunidadItem;

        public OportunidadPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            oportunidadItem = (OportunidadViewModel)BindingContext;
            if (oportunidadItem.Baja == true || oportunidadItem.FechaFin.Date < DateTime.Today.Date)
            {
                buttonModificar.IsEnabled = false;
                buttonDarDeBaja.IsEnabled = false;
            }
        }


        async void OnModifyButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OportunidadEditPage
            {
                BindingContext = this.BindingContext
            }) ;
        }

        async void OnAppsButtonClicked(object sender, EventArgs e)
        {
            oportunidadItem = (OportunidadViewModel)BindingContext;

            await Navigation.PushAsync(new PostulacionesPage(oportunidadItem));
        }

        async void OnDownButtonClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Alerta","¿Está seguro de que quiere dar de baja esta Oportunidad? (Esta acción no se puede deshacer)", "Si", "No");

            if (answer)
            {                
                await App.VoluntarioManager.DownOportunidadAsync(oportunidadItem.OportunidadID);
                await Navigation.PopAsync();
            }            
        }




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizacionesApp.Views.Organizacion;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(OportunidadesPage)}");
        }

        private async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(RegistrationPage}");
        }

    }
}
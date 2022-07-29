using System;
using OrganizacionesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoluntarioListPage : ContentPage
    {
        public VoluntarioListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.VoluntarioManager.GetVoluntarioAllAsync();
        }

        async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VoluntarioPage(true)
            {
                BindingContext = new Voluntario
                {
                    VolutnarioID = Int16.Parse(Guid.NewGuid().ToString())
                }
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new VoluntarioPage
            {
                BindingContext = e.SelectedItem as Voluntario
            });
        }



    }
}
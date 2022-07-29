using System;
using OrganizacionesApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoluntarioPage : ContentPage
    {
        readonly bool isNewItem;
        public VoluntarioPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var voluntarioItem = (Voluntario)BindingContext;            
            await App.VoluntarioManager.SaveVoluntarioAsync(voluntarioItem, isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var todoItem = (Voluntario)BindingContext;
            await App.VoluntarioManager.DeleteVoluntarioAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
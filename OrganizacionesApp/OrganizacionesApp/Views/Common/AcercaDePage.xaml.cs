
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrganizacionesApp.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcercaDePage : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        public AcercaDePage()
        {
            InitializeComponent();
        }
    }
}
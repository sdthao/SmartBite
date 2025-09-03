using SmartBite.Maui.ViewModels;

namespace SmartBite.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}

using SmartBite.Maui.ViewModels;

namespace SmartBite.Maui.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;

        NavigationPage.SetHasNavigationBar(this, false);
    }
}
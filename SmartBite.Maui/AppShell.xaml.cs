namespace SmartBite.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("login", typeof(Views.LoginView));
            Routing.RegisterRoute("aitool", typeof(Views.AIToolView));
            Routing.RegisterRoute("camera", typeof(Views.CameraView));
            Routing.RegisterRoute("options", typeof(Views.OptionsView));
            Routing.RegisterRoute("journal", typeof(Views.UserJournalView));
            Routing.RegisterRoute("register", typeof(Views.RegisterUserView));
            Routing.RegisterRoute("editfood", typeof(Views.EditFoodItemsView));
        }
    }
}

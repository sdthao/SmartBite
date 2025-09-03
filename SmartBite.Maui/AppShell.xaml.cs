namespace SmartBite.Maui
{
    public partial class AppShell : Shell
    {
        private readonly IServiceProvider _services;

        public AppShell(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;

            Routing.RegisterRoute("login", typeof(Views.LoginView));
            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("register", typeof(Views.RegisterUserView));

            //Dispatcher.Dispatch(async () =>
            //{
            //    await GoToAsync("login");
            //});
        }
    }
}

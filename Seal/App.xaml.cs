using System;
using Ninject;
using Seal.Navigation;
using Seal.Ninject;
using Seal.Pages.AdminDashboard;
using Seal.RestaurantAdminPages;
using Seal.ViewModels;
using Seal.ViewModels.RestaurantSignUpVMs;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace Seal
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public StandardKernel Kernel { get; set; }
        public App()
        {
            InitializeComponent();


            // Main page root
            var mainPage = new NavigationPage(new GetStarted()) { BarBackgroundColor = Color.WhiteSmoke, BarTextColor = Color.Black };

            var module = new AppModule(mainPage.Navigation);
            Kernel = new StandardKernel(module);

            // Platform specific kernel initialization
            // Kernel.Load(platformModules);

            var appNavigationService = Kernel.Get<INavigationService>();

            // view and view model mappings
            module.Map(appNavigationService);
            mainPage.BindingContext = Kernel.Get<GetStartedVM>();
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

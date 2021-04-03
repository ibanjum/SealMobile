using System;
using Seal.GraphicalInterface.Manage;
using Seal.ViewModels;
using Urho;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace Seal.Pages.AdminDashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminDashboardPage : TabbedPage
    {

        public AdminDashboardPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            urhoSurface.BackgroundColor = Xamarin.Forms.Color.FromHex("#E8E8E8");

            var app = await urhoSurface.Show<ManageFloorPlanGUI>(new ApplicationOptions(assetsFolder: "Data")
            {
                Orientation = ApplicationOptions.OrientationType.LandscapeAndPortrait
            });

            var ViewModel = BindingContext as AdminDashboardVM;
            await ViewModel.LoadFloorPlanSceneAndCategories(app);
            Urho.Application.InvokeOnMain(() => app.CreatCameraAndPort());
        }
    }
}

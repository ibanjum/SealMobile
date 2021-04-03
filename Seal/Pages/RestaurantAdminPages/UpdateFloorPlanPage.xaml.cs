using Seal.GraphicalInterface.UpdateEdit;
using Seal.ViewModels;
using Urho;
using Urho.Gui;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Xamarin.Forms.Color;

namespace Seal.RestaurantAdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateFloorPlanPage : ContentPage
    {
        private FloorPlanGUI _floorPlanGUI;

        public UpdateFloorPlanPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            urhoSurface.BackgroundColor = Color.FromHex("#E8E8E8");

            var app = await urhoSurface.Show<FloorPlanGUI>(new ApplicationOptions(assetsFolder: "Data")
            {
                Orientation = ApplicationOptions.OrientationType.LandscapeAndPortrait
            });
            _floorPlanGUI = app;
            app.SaveButton.Pressed += SaveButton_Pressed;
        }

        private void SaveButton_Pressed(PressedEventArgs obj)
        {
            var ViewModel = BindingContext as UpdateFloorPlanVM;
            ViewModel.SaveCommand.Execute(_floorPlanGUI);
        }
    }
}

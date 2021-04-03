using System.Windows.Input;
using Seal.DataClients;
using Seal.GraphicalInterface.UpdateEdit;
using Seal.Models;
using Seal.Navigation;
using Seal.Utilities;
using Xamarin.Forms;

namespace Seal.ViewModels
{
    public class UpdateFloorPlanVM : BaseViewModel
    {
        public ICommand SaveCommand { get; private set; }

        private readonly IDetailRestaurantDataClient _detailRestaurantDC;
        private readonly INavigationService _navService;

        public UpdateFloorPlanVM(INavigationService navService, IDetailRestaurantDataClient detailRestaurantDC) : base(navService)
        {
            _detailRestaurantDC = detailRestaurantDC;
            _navService = navService;
            SaveCommand = new Command(SavePressed);
        }

        private async void SavePressed(object obj)
        {
            if (obj is FloorPlanGUI floorPlanGUI)
            {
                var dir = floorPlanGUI.FileSystem.UserDocumentsDir;
                if (!System.IO.File.Exists(dir + "FloorPlan.xml"))
                {
                    System.IO.File.Create(dir + "FloorPlan.xml");
                }
                floorPlanGUI.scene.SaveXml(dir + "FloorPlan.xml");
                var stream = System.IO.File.OpenRead(dir + "FloorPlan.xml");
                FloorPlan floorPlan = new FloorPlan()
                {
                    SceneByteArray = XmlUtility.GetFileAsBytes(stream)
                };
                var floorPlanTask = await _detailRestaurantDC.PostScene<FloorPlan>(floorPlan);

                if (floorPlanTask.Success)
                {
                    Device.BeginInvokeOnMainThread(Navigate);
                }
            }
        }

        private async void Navigate()
        {
            await _navService.NavigateTo<SetUpFinishVM>();
        }
    }
}

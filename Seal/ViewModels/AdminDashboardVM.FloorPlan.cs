using System.Threading.Tasks;
using Seal.GraphicalInterface.Manage;
using Seal.Models;
using Seal.Utilities;
using Xamarin.Forms;

namespace Seal.ViewModels
{
    public partial class AdminDashboardVM
    {
        private ManageFloorPlanGUI _floorPlanGUI;
        private string _directory;

        public async Task LoadFloorPlanSceneAndCategories(ManageFloorPlanGUI floorPlanGUI)
        {
            var floorPlanTask = await _detailRestaurantDataClient.GetScene<FloorPlan>();
            if (floorPlanTask.Success && floorPlanTask.Data != null)
            {
                _floorPlanGUI = floorPlanGUI;
                _directory = floorPlanGUI.FileSystem.UserDocumentsDir;
                if (!System.IO.File.Exists(_directory + "FloorPlan.xml"))
                {
                    System.IO.File.Create(_directory + "FloorPlan.xml");
                }
                var xml = XmlUtility.GetFileAsXML(floorPlanTask.Data.SceneByteArray);
                xml.Save(_directory + "FloorPlan.xml");

                Urho.Application.InvokeOnMain(() => _floorPlanGUI.scene.LoadXml(_directory + "FloorPlan.xml"));
                LoadCategoriesCommand.Execute(null);
            }
        }
    }
}

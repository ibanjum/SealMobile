using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Seal.DataClients;
using Seal.Models.DTOs;
using Seal.Navigation;
using Xamarin.Forms;

namespace Seal.ViewModels.RestaurantSignUpVMs
{
    public class BasicInfoVM : BaseViewModel
    {
        private RestaurantBasicInfoDTO _basicInfo;
        public RestaurantBasicInfoDTO BasicInfo
        {
            get { return _basicInfo; }
            set
            {
                _basicInfo = value;
                OnPropertyChanged(nameof(BasicInfo));
            }
        }
        public string BasicInfoTitle { get; private set; }
        public string BasicInfoDescription { get; private set; }
        public string ButtonText { get; private set; }
        public ICommand ButtonCommand { get; set; }

        private readonly IRestaurantSignUpDataClient _restaurantSignUpDM;
        private readonly INavigationService _navService;

        public BasicInfoVM(IRestaurantSignUpDataClient restaurantSignUpDM, INavigationService navService) : base(navService)
        {
            _navService = navService;
            _restaurantSignUpDM = restaurantSignUpDM;

            BasicInfoTitle = "Restaurant Address";
            BasicInfoDescription = "Where is your business located?";
            ButtonText = "Next";
            ButtonCommand = new Command(NextButtonPressed);
            LoadBasicInfo();
        }


        private async void LoadBasicInfo()
        {
            var getBasicInfoTask = await _restaurantSignUpDM.GetBasicInfo<RestaurantBasicInfoDTO>();
            if (getBasicInfoTask.Success)
            {
                BasicInfo = getBasicInfoTask.Data;
            }

        }
        private async void NextButtonPressed()
        {
            var postBasicInfoTask = await _restaurantSignUpDM.PostBasicInfo<RestaurantBasicInfoDTO>(BasicInfo);
            if (postBasicInfoTask.Success)
            {
                await _navService.NavigateTo<RestaurantHoursVM>();
            }
        }
    }
}

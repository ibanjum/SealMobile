using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Seal.Navigation;
using Seal.Resources;
using Seal.RestaurantAdminPages;
using Xamarin.Forms;

namespace Seal.ViewModels.RestaurantSignUpVMs
{
    public class GetStartedVM : BaseViewModel
    {
        public ImageSource GetStartedImage { get; private set; }
        public string GetStatedTitle { get; private set; }
        public string GetStatedDescription { get; private set; }
        public string GetStartedButtonText { get; private set; }
        public ICommand GetStartedButtonCommand { get; private set; }

        private readonly INavigationService _navService;

        public GetStartedVM(INavigationService navService) : base(navService)
        {
            _navService = navService;
            GetStartedImage = ImageNames.GetGetStartedImage;
            GetStatedTitle = AppResources.GetStartedTitle;
            GetStatedDescription = AppResources.GetStatedDescription;
            GetStartedButtonText = AppResources.NextButton;
            GetStartedButtonCommand = new Command(GetStartedPressed);
        }

        private async void GetStartedPressed()
        {
            await _navService.NavigateTo<BasicInfoVM>();
        }
    }
}

using System.Windows.Input;
using Seal.Navigation;
using Xamarin.Forms;

namespace Seal.ViewModels
{
    public class SetUpFinishVM : BaseViewModel
    {
        public string CongratsLottie => "reward.json";
        public string Title => "Congratulations!";
        public string Description => "Your restaurant is up and live, thank you for joining us.";
        public string ButtonText => "Let's Start Managing";

        public ICommand NextCommand { get; private set; }

        private readonly INavigationService _navService;

        public SetUpFinishVM(INavigationService navService) : base(navService)
        {
            _navService = navService;
            NextCommand = new Command(NextPressed);
        }

        private async void NextPressed()
        {
            await _navService.NavigateTo<AdminDashboardVM>();
        }
    }
}

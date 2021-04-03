using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Seal.Navigation;
using Xamarin.Forms;

namespace Seal.ViewModels
{
    public class MenuOptionsVM : BaseViewModel
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string MenuPhoneButtonText { get; private set; }
        public string IndividualButtonText { get; private set; }
        public string SkipButtonText { get; private set; }

        public ICommand SelectPhotoCommand { get; private set; }
        public ICommand IndividualCommand { get; private set; }
        public ICommand SkipCommand { get; private set; }

        INavigationService _navService;
        public MenuOptionsVM(INavigationService navService) : base(navService)
        {
            _navService = navService;

            Title = "Chose menu type";
            Description = "How would you like to add your food menu?";
            MenuPhoneButtonText = "Select photo";
            IndividualButtonText = "Add items individualy";
            SkipButtonText = "Skip";

            SelectPhotoCommand = new Command(SelectPhotoPressed);
            IndividualCommand = new Command(IndividualPressed);
            SkipCommand = new Command(SkipPressed);
        }

        private async void SkipPressed()
        {
            await _navService.NavigateTo<UpdateFloorPlanVM>();
        }

        private async void IndividualPressed()
        {
            await _navService.NavigateTo<AddMenuItemsVM>();
        }

        private async void SelectPhotoPressed()
        {

        }
    }
}

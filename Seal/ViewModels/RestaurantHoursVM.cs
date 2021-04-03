using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Seal.DataClients;
using Seal.Models;
using Seal.Navigation;
using Xamarin.Forms;

namespace Seal.ViewModels
{
    public class RestaurantHoursVM : BaseViewModel
    {
        public string PageTitle { get; private set; }
        public string PageDescription { get; private set; }
        public string ButtonText { get; private set; }
        public string AddButtonText { get; private set; }

        double opacity;
        public double Opacity
        {
            get { return opacity; }
            set
            {
                opacity = value;
                OnPropertyChanged(nameof(Opacity));
            }
        }

        bool isNotAlwaysOpen;
        public bool IsNotAlwaysOpen
        {
            get { return isNotAlwaysOpen; }
            set
            {
                Opacity = Opacity == 0.5 ? 1 : 0.5;
                isNotAlwaysOpen = value;
                OnPropertyChanged(nameof(IsNotAlwaysOpen));
            }
        }

        public ObservableCollection<BusinessHoursModel> BusinessHours { get; set; }

        BusinessHoursModel selectedBusinessHours;
        public BusinessHoursModel SelectedBusinessHours
        {
            get { return selectedBusinessHours; }
            set
            {
                if (selectedBusinessHours != value)
                {
                    selectedBusinessHours = value;
                }
            }
        }
        public ICommand ButtonCommand { get; }
        public ICommand AddButtonCommand { get; }
        public ICommand DayButtonCommand { get; }
        public ICommand DeleteButtonCommand { get; }

        INavigationService _navService;
        IRestaurantSignUpDataClient _restaurantSignUpDC;
        public RestaurantHoursVM(IRestaurantSignUpDataClient restaurantSignUpDC, INavigationService navService) : base(navService)
        {
            _navService = navService;
            _restaurantSignUpDC = restaurantSignUpDC;

            PageTitle = "Restaurant Hours";
            PageDescription = "When do the doors close and open?";
            AddButtonText = "Add more";
            ButtonText = "Next";
            Opacity = 1;

            BusinessHours = new ObservableCollection<BusinessHoursModel>()
            {
                new BusinessHoursModel()
            };

            DayButtonCommand = new Command(DayPressed);
            AddButtonCommand = new Command(AddPress);
            ButtonCommand = new Command(ButtonPressed);
            DeleteButtonCommand = new Command(DeletePressed);
        }

        private void DayPressed(object obj)
        {
            if (obj is DayModel dayModel)
            {
                dayModel.Selected = !dayModel.Selected;
            }
        }

        private void DeletePressed(object obj)
        {
            if (obj is BusinessHoursModel businessHoursModel)
            {
                BusinessHours.Remove(businessHoursModel);
            }
        }

        private void AddPress()
        {
            BusinessHours.Add(new BusinessHoursModel());
        }

        private async void ButtonPressed()
        {
            var dublicateHours = CheckDublicateHours();
            if (dublicateHours.Count >= 1)
            {
                var popup = new PopupModel("Dublicate hours", "Hours overlap on the following days: " + string.Join(", ", dublicateHours.ToArray()), "Ok");
                await _navService.DisplayPopup(popup);
            }
            else
            {
                var dto = new BusinessHoursDTOs(BusinessHours);
                var hourTask = await _restaurantSignUpDC.PostHours<BusinessHoursDTOs>(dto);
                if (hourTask.Success)
                {
                    await _navService.NavigateTo<AdditionalInfoVM>();
                }
            }
        }

        private List<string> CheckDublicateHours()
        {
            DayModel day1;
            List<string> dublicateHours = new List<string>();
            for (int i = 0; i < BusinessHours.Count; i++)
            {
                for (int j = i + 1; j < BusinessHours.Count; j++)
                {
                    if (i != j)
                    {
                        for (int d = 0; d < 7; d++)
                        {
                            day1 = BusinessHours[i].Days[d];
                            var day2 = BusinessHours[j].Days[d];
                            if (day1.Selected && day2.Selected)
                            {
                                var day1Open = BusinessHours[i].Opens;
                                var day1Closes = BusinessHours[i].Closes;

                                var day2Open = BusinessHours[j].Opens;
                                var day2Closes = BusinessHours[j].Closes;

                                if ((day1Open >= day2Open && day1Closes <= day2Closes)
                                    || (day1Open <= day2Open && day1Closes >= day2Closes))
                                {
                                    dublicateHours.Add(day1.Name);
                                }
                            }
                        }
                    }
                }
            }
            return dublicateHours;
        }
    }
}

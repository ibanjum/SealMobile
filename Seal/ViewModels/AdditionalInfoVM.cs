using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Seal.DataClients;
using Seal.Models;
using Seal.Models.DTOs;
using Seal.Navigation;
using Seal.Utilities;
using Xamarin.Forms;

namespace Seal.ViewModels
{
    public class AdditionalInfoVM : BaseViewModel
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string NextButtonText { get; private set; }

        string cusine;
        public string Cusine
        {
            get { return cusine; }
            set
            {
                cusine = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public AdditionalInfo Info { get; set; } = new AdditionalInfo();
        public List<string> Cusines { get; set; }

        public ICommand NextButtonCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }

        private readonly IRestaurantSignUpDataClient _restaurantSignUpDM;
        private readonly INavigationService _navService;

        public AdditionalInfoVM(IRestaurantSignUpDataClient restaurantSignUpDM, INavigationService navService) : base(navService)
        {
            _navService = navService;
            _restaurantSignUpDM = restaurantSignUpDM;
            NextButtonText = "Next";
            Title = "Add your restaurant Photo";
            Description = "This photo will be displayed on the main page of the restaurant";



            NextButtonCommand = new Command(NextPressed);
            AddImageCommand = new Command(AddImagePressed);
            InitializeCusines();
        }

        private async void AddImagePressed()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await _navService.DisplayPopup(new PopupModel("No Camera", ":( No camera available.", "OK"));
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            Info.MainImage = ImageUtility.GetImageStreamAsBytes(file.GetStream());
            Image = ImageSource.FromStream(() => file.GetStream());

        }

        private void InitializeCusines()
        {
            Cusines = new List<string>()
            {
                "American",
                "Thai",
                "Asian",
                "Italian"
            };
        }

        private async void NextPressed()
        {
            Info.Cusines = new List<string>();
            Info.Cusines.Add(Cusine);
            var postTask = await _restaurantSignUpDM.GetAddtionalInfo<AdditionalInfo>(Info);
            if (postTask.Success)
            {
                await _navService.NavigateTo<MenuOptionsVM>();
            }
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AddMenuItemsVM : BaseViewModel
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string PreviewButtonText { get; private set; }
        public string SelectCategoryText { get; private set; }
        public string AddCategpryButtonText { get; private set; }
        public string AddAnotherButtonText { get; private set; }

        public MenuItemGroup Items { get; private set; }
            = new MenuItemGroup(string.Empty, new ObservableCollection<Models.MenuItem>() { new Models.MenuItem() });

        public List<string> Categories { get; private set; }

        public ICommand PreviewCommand { get; private set; }
        public ICommand AddCategoryCommand { get; private set; }
        public ICommand AddAnotherCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ImageCommand { get; private set; }

        INavigationService _navService;
        IRestaurantSignUpDataClient _restaurantSignUpDC;
        public AddMenuItemsVM(IRestaurantSignUpDataClient restaurantSignUpDC, INavigationService navService) : base(navService)
        {
            _navService = navService;
            _restaurantSignUpDC = restaurantSignUpDC;

            Title = "Add Menu Items";
            Description = "What kind of food does your restaurant serve?";
            SelectCategoryText = "Select Category";
            AddCategpryButtonText = "Add another category";
            PreviewButtonText = "Preview";
            AddAnotherButtonText = "Add Another";

            InitializeCategories();

            PreviewCommand = new Command(PreviewPressed);
            AddCategoryCommand = new Command(AddCategoryPressed);
            AddAnotherCommand = new Command(AddAnotherPressed);
            DeleteCommand = new Command(DeletePressed);
            ImageCommand = new Command(ImagePressed);
        }

        private async void ImagePressed(object obj)
        {
            if (obj is Models.MenuItem item)
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

                item.ImageBytes = ImageUtility.GetImageStreamAsBytes(file.GetStream());
                item.Image = ImageSource.FromStream(() => file.GetStream());
            }
        }

        private void DeletePressed(object obj)
        {
            if (obj is Models.MenuItem item)
            {
                Items.Remove(item);
            }
        }

        private void AddAnotherPressed()
        {
            Items.Add(new Models.MenuItem());
        }

        private async void AddCategoryPressed()
        {
            var dto = new MenuItemsDTO(Items);
            var itemsTask = await _restaurantSignUpDC.PostItems<MenuItemsDTO>(dto);
            if (itemsTask.Success)
            {
                await _navService.NavigateTo<AddMenuItemsVM>();
            }
        }

        private async void PreviewPressed()
        {
            var dto = new MenuItemsDTO(Items);
            var itemsTask = await _restaurantSignUpDC.PostItems<MenuItemsDTO>(dto);
            if (itemsTask.Success)
            {
                await _navService.NavigateTo<UpdateFloorPlanVM>();
            }
        }

        private void InitializeCategories()
        {
            Categories = new List<string>()
            {
                "Appetizers",
                "Breakfast",
                "Lunch",
                "Dinner",
                "Drinks",
                "Desserts",
                "Vegetarian"
            };
        }
    }
}

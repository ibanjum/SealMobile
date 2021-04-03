using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Seal.Models;
using Xamarin.Forms;

namespace Seal.ViewModels
{
    public partial class AdminDashboardVM
    {
        public string Title { get; private set; }
        public string NextButtonText { get; private set; }

        public ICommand AddMoreCommand { get; private set; }
        public ICommand CategoryCommand { get; private set; }
        public ICommand LoadCategoriesCommand { get; private set; }

        bool isLoadingCategories;
        public bool IsLoadingCategories
        {
            get { return isLoadingCategories; }
            set
            {
                isLoadingCategories = value;
                OnPropertyChanged(nameof(IsLoadingCategories));
            }
        }

        string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public DetailRestaurantModel Restaurant { get; private set; } = new DetailRestaurantModel() { Items = new ObservableCollection<MenuItemGroup>() };

        private void InitializeInfo()
        {
            Title = "Menu";
            NextButtonText = "Next";

            AddMoreCommand = new Command(AddMorePressed);
            CategoryCommand = new Command(CategoryPressed);
            LoadCategoriesCommand = new Command(async () => await LoadCategories());
        }

        private async Task LoadCategories()
        {
            IsLoadingCategories = true;
            try
            {
                var restaurantTask = await _detailRestaurantDataClient.GetDetailRestaurant<DetailRestaurantModel>();
                if (restaurantTask.Success && restaurantTask.Data != null)
                {
                    Restaurant.Items.Clear();
                    Restaurant.ConvertDTO(restaurantTask.Data);
                    Address = string.Format("{0} {1}, {2}, {3} {4}, {5}", Restaurant.Address1, Restaurant.Address2, Restaurant.City, Restaurant.State, Restaurant.ZipCode, Restaurant.Country);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoadingCategories = false;
            }
        }

        private async void AddMorePressed()
        {
            await _navService.NavigateTo<AddMenuItemsVM>();
        }

        private void CategoryPressed(object obj)
        {
            if (obj is MenuItemGroup group)
            {
                group.CategorySelected = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Seal.Models.DTOs;
using Seal.Utilities;
using Xamarin.Forms;

namespace Seal.Models
{
    public class DetailRestaurantModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }

        public ObservableCollection<MenuItemGroup> Items { get; set; }

        public List<string> Cusines { get; set; }

        public DetailRestaurantModel() { }

        public void ConvertDTO(DetailRestaurantDTO dto)
        {
            Name = dto.Name;
            Address1 = dto.Address1;
            Address2 = dto.Address2;
            City = dto.City;
            State = dto.State;
            ZipCode = dto.ZipCode;
            Country = dto.Country;
            Cusines = dto.Cusines;
            Image = ImageUtility.GetImageAsSource(dto.Image); ;

            Dictionary<string, ObservableCollection<MenuItem>> map = new Dictionary<string, ObservableCollection<MenuItem>>();
            foreach (var item in dto.Items)
            {
                if (!map.ContainsKey(item.Category))
                {
                    map.Add(item.Category, new ObservableCollection<MenuItem>());
                }

                map[item.Category].Add(new MenuItem()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    Image = ImageUtility.GetImageAsSource(item.Image)
                });
            }

            foreach (var hash in map)
            {
                Items.Add(new MenuItemGroup(hash.Key, hash.Value));
            }
        }
    }
}

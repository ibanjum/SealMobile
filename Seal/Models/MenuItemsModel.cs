using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Seal.Models.DTOs;
using Seal.Utilities;
using Xamarin.Forms;

namespace Seal.Models
{

    public class MenuItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public byte[] ImageBytes { get; set; }

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

        public MenuItem()
        {
            Image = "placeholder_menuItem";
        }
    }

    public class MenuItemGroup : ObservableCollection<MenuItem>
    {
        public string Category { get; set; }
        bool categorySelected;
        public bool CategorySelected
        {
            get { return categorySelected; }
            set
            {
                categorySelected = false;
                categorySelected = value;
            }
        }
        public MenuItemGroup(string category, ObservableCollection<MenuItem> items) : base(items)
        {
            Category = category;
        }
    }
}

using System;
namespace Seal.Models
{
    public class ItemModel
    {
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
        public string ImageName { get; set; }
        public ItemModel(string name)
        {
            Name = name;
            ImageName = name;
        }
    }
}

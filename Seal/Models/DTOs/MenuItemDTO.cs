using System.Collections.Generic;
using Newtonsoft.Json;

namespace Seal.Models.DTOs
{
    public class MenuItemsDTO
    {
        [JsonProperty("items")]
        public List<MenuItemDTO> Items { get; set; }

        public MenuItemsDTO() { }

        public MenuItemsDTO(MenuItemGroup itemsGroup)
        {
            Items = new List<MenuItemDTO>();
            foreach (var item in itemsGroup)
            {
                Items.Add(new MenuItemDTO
                {
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    Image = item.ImageBytes,
                    Category = itemsGroup.Category
                });
            }
        }
    }

    public class MenuItemDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("image")]
        public byte[] Image { get; set; }
    }
}

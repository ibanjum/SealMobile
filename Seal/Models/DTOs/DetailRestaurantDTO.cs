using System.Collections.Generic;
using Newtonsoft.Json;

namespace Seal.Models.DTOs
{
    public class DetailRestaurantDTO
    {
        [JsonProperty("restaurantname")]
        public string Name { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipcode")]
        public string ZipCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("image")]
        public byte[] Image { get; set; }

        [JsonProperty("items")]
        public List<MenuItemDTO> Items { get; set; }

        [JsonProperty("cusines")]
        public List<string> Cusines { get; set; }

    }
}

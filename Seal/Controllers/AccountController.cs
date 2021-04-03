using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Seal.Models;
using Xamarin.Forms;

namespace Seal.Controllers
{
    public class AccountController
    {
        public static Uri BaseAddress = new Uri("http://10.227.105.59:8080");

        public static async Task<string> Login(User user)
        {
            var usermodel = JsonConvert.SerializeObject(user);

            ResponseResult result = new ResponseResult();
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var content = new StringContent(usermodel, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/login", content);

                if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();

                    var streamReader = new StreamReader(contentStream);

                    var jsonReader = new JsonTextReader(streamReader);

                    JsonSerializer serializer = new JsonSerializer();
                    JToken jtoken = serializer.Deserialize<JToken>(jsonReader);

                    result = jtoken.ToObject<ResponseResult>();

                }
            }
            return null;
        }
        public static async Task<string> Register(User user)
        {
            var usermodel = JsonConvert.SerializeObject(user);

            ResponseResult result = new ResponseResult();
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var content = new StringContent(usermodel, Encoding.UTF8, "application/json");
                var response = client.PostAsync("/register", content).Result;

                if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();

                    using (var streamReader = new StreamReader(contentStream))
                    {
                        using (var jsonReader = new JsonTextReader(streamReader))
                        {
                            JsonSerializer serializer = new JsonSerializer();

                            result = serializer.Deserialize<ResponseResult>(jsonReader);


                        }
                    }
                }
            }
            return null;
        }
        public static async Task<string> RegisterRestaurant(Request restaurant)
        {

            var usermodel = JsonConvert.SerializeObject(restaurant);

            ResponseResult result = new ResponseResult();
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var content = new StringContent(usermodel, Encoding.UTF8, "application/json");
                var response = client.PostAsync("/restaurantignup", content).Result;

                if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();

                    using (var streamReader = new StreamReader(contentStream))
                    {
                        using (var jsonReader = new JsonTextReader(streamReader))
                        {
                            JsonSerializer serializer = new JsonSerializer();

                            result = serializer.Deserialize<ResponseResult>(jsonReader);

                        }
                    }
                }
            }
            return null;
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Yelp.Api.Models;
using System.Net.Http;
using Yelp.Api;
using System.Collections.Generic;
using System.Text;
using Seal.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;

namespace Seal.Controllers
{

    public class DataController
    {
        public static string APIKey = "AHGjP6OGHnGXyVVYRO_2JHWuOAyOVQC2Fw-Tam-L_efvg5x_PXh7ufed1-DVoEEHgArvxWYmo_0ns__Q6bJ1fLOg0MQuRPL-ia7rVvChSLwu50q5YXJOEMxFzlz3XnYx";
        public static Uri BaseAddress = new Uri("http://10.227.105.59:8080");

        async public static Task<List<BusinessResponse>> GetBusinesses(string category, bool onlyOpen, int radius, string city)
        {
            var request = new SearchRequest();
            request.Location = city;
            request.Term = category;
            request.MaxResults = 50;
            request.OpenNow = onlyOpen;
            if (radius != 0)
            {
                request.Radius = radius;
            }

            var client = new Client(APIKey);
            SearchResponse results = await client.SearchBusinessesAllAsync(request);
            if (results.Error == null)
            {
                var ids = results.Businesses.Select(x => x.Id);
                PostBusinessIds(ids);
                return (List<BusinessResponse>)results.Businesses;
            }

            return null;
        }
        async public static Task<List<BusinessResponse>> GetBusiness(string category, string term, string address)
        {
            var request = new SearchRequest();
            request.Term = term;
            request.Location = address;
            request.MaxResults = 10;

            var client = new Client(APIKey);
            SearchResponse results = await client.SearchBusinessesAllAsync(request);
            if (results.Error == null)
            {
                var ids = results.Businesses.Select(x => x.Id);
                PostBusinessIds(ids);
                return (List<BusinessResponse>)results.Businesses;
            }
            return null;
        }
        async public static Task<BusinessResponse> GetBusinessDetail(string id)
        {
            var client = new Client(APIKey);
            BusinessResponse result = await client.GetBusinessAsync(id);

            return result;
        }
        async private static void PostBusinessIds(IEnumerable<string> ids)
        {
            var jsonids = JsonConvert.SerializeObject(ids);
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpContent content = new StringContent(jsonids, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/postids", content);
            }
        }
        async public static Task<string> PostRestaurant(BusinessResponse data, string username)
        {
            var jsonids = JsonConvert.SerializeObject(data);
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpContent content = new StringContent(jsonids, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/addrestaurant/" + username, content);
                if (response.IsSuccessStatusCode)
                {
                    return "Successfull!";
                }
            }
            return "Error";
        }
        public static async Task<Request[]> GetRestaurantRequests()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                var response = await client.GetAsync("/requests");
                if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();

                    var streamReader = new StreamReader(contentStream);

                    var jsonReader = new JsonTextReader(streamReader);

                    JsonSerializer serializer = new JsonSerializer();
                    Request[] requests = serializer.Deserialize<Request[]>(jsonReader);
                    return requests;
                }
            }
            return null;
        }
    }
}

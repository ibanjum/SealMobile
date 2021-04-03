using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Seal.Models;
using Seal.Models.DTOs;
using Seal.Navigation;

namespace Seal.Controllers
{
    public class ServiceController : IServiceController
    {
        INavigationService _navService;
        public ServiceController(INavigationService navService)
        {
            _navService = navService;
        }

        public async Task<ResponseResult<T>> GetAsync<T>(string endpoint, object data, params string[] endpointParams)
        {
            return await MakeRequest<T>(HttpMethod.Get, endpoint, data, endpointParams);
        }

        public async Task<ResponseResult<T>> PostAsync<T>(string endpoint, object data, params string[] endpointParams)
        {
            return await MakeRequest<T>(HttpMethod.Post, endpoint, data, endpointParams);
        }

        public async Task<ResponseResult<T>> DeleteAsync<T>(string endpoint, params string[] endpointParams)
        {
            return await MakeRequest<T>(HttpMethod.Delete, endpoint, endpointParams);
        }

        public async Task<ResponseResult<T>> MakeRequest<T>(HttpMethod method, string endpoint, object data, params string[] endpointParams)
        {
            ResponseResult<T> result = new ResponseResult<T>();
            HttpClient Client = new HttpClient();

            var url = "http://10.227.105.45:8080" + endpoint;
            if (endpointParams != null && endpointParams.Length > 0)
                url = string.Format(url, endpointParams);

            using (var request = new HttpRequestMessage(method, url))
            {

                if (data != null && method == HttpMethod.Post)
                {
                    var dataString = JsonConvert.SerializeObject(data);
                    request.Content = new StringContent(dataString, Encoding.UTF8, "application/json");
                }

                try
                {
                    using (var response = await Client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.Content is object && response.Content.Headers.ContentType.MediaType == "application/json")
                            {
                                var contentStream = await response.Content.ReadAsStreamAsync();

                                using (var streamReader = new StreamReader(contentStream))
                                {
                                    using (var jsonReader = new JsonTextReader(streamReader))
                                    {
                                        JsonSerializer serializer = new JsonSerializer();

                                        if (data == null)
                                            result.Data = serializer.Deserialize<T>(jsonReader);
                                        result.Success = true;

                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var popup = new PopupModel("Exception", ex.ToString(), "Ok");
                    await _navService.DisplayPopup(popup);
                }
            }

            return result;
        }
    }
    public interface IServiceController
    {
        Task<ResponseResult<T>> GetAsync<T>(string endpoint, object data, params string[] endpointParams);

        Task<ResponseResult<T>> PostAsync<T>(string endpoint, object data, params string[] endpointParams);
    }
}

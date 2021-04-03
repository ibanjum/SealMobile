using System.Threading.Tasks;
using Seal.Controllers;
using Seal.Models;
using Seal.Models.DTOs;
using Seal.Resources;

namespace Seal.DataClients
{
    public class RestaurantSignUpDataClient : IRestaurantSignUpDataClient
    {
        private readonly IServiceController _serviceController;

        public RestaurantSignUpDataClient(IServiceController serviceController)
        {
            _serviceController = serviceController;
        }

        public async Task<ResponseResult<RestaurantBasicInfoDTO>> GetBasicInfo<T>()
        {
            var resp = await _serviceController.GetAsync<RestaurantBasicInfoDTO>(EndPoints.GetRestaurantBasicInfo, null, "3");

            return resp;
        }

        public async Task<ResponseResult<RestaurantBasicInfoDTO>> PostBasicInfo<T>(RestaurantBasicInfoDTO infoDTO)
        {
            var resp = await _serviceController.PostAsync<RestaurantBasicInfoDTO>(EndPoints.PostRestaurantBasicInfo, infoDTO, "3");

            return resp;
        }

        public async Task<ResponseResult<BusinessHoursDTOs>> PostHours<T>(BusinessHoursDTOs hoursDTO)
        {
            var resp = await _serviceController.PostAsync<BusinessHoursDTOs>(EndPoints.PostRestaurantHours, hoursDTO, "3");

            return resp;
        }

        public async Task<ResponseResult<MenuItemsDTO>> PostItems<T>(MenuItemsDTO itemsDTO)
        {
            var resp = await _serviceController.PostAsync<MenuItemsDTO>(EndPoints.PostRestaurantMenuItems, itemsDTO, "3");

            return resp;
        }

        public async Task<ResponseResult<AdditionalInfo>> GetAddtionalInfo<T>(AdditionalInfo data)
        {
            var resp = await _serviceController.PostAsync<AdditionalInfo>(EndPoints.PostRestaurantAdditionalInfo, data, "3");

            return resp;
        }
    }
    public interface IRestaurantSignUpDataClient
    {
        Task<ResponseResult<RestaurantBasicInfoDTO>> PostBasicInfo<T>(RestaurantBasicInfoDTO infoDTO);
        Task<ResponseResult<RestaurantBasicInfoDTO>> GetBasicInfo<T>();
        Task<ResponseResult<BusinessHoursDTOs>> PostHours<T>(BusinessHoursDTOs hoursDTO);
        Task<ResponseResult<MenuItemsDTO>> PostItems<T>(MenuItemsDTO itemsDTO);
        Task<ResponseResult<AdditionalInfo>> GetAddtionalInfo<T>(AdditionalInfo data);
    }
}

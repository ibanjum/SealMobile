using System;
using System.Threading.Tasks;
using Seal.Controllers;
using Seal.Models;
using Seal.Models.DTOs;
using Seal.Resources;

namespace Seal.DataClients
{
    public class DetailRestaurantDataClient : IDetailRestaurantDataClient
    {
        private readonly IServiceController _serviceController;

        public DetailRestaurantDataClient(IServiceController serviceController)
        {
            _serviceController = serviceController;
        }

        public async Task<ResponseResult<DetailRestaurantDTO>> GetDetailRestaurant<T>()
        {
            var resp = await _serviceController.GetAsync<DetailRestaurantDTO>(EndPoints.GetDetailRestaurant, null, "3");
            return resp;
        }

        public async Task<ResponseResult<FloorPlan>> PostScene<T>(FloorPlan floorPlan)
        {
            var resp = await _serviceController.PostAsync<FloorPlan>(EndPoints.PostFloorPlan, floorPlan, "3");

            return resp;
        }

        public async Task<ResponseResult<FloorPlan>> GetScene<T>()
        {
            var resp = await _serviceController.GetAsync<FloorPlan>(EndPoints.GetFloorPlan, null, "3");

            return resp;
        }
    }

    public interface IDetailRestaurantDataClient
    {
        Task<ResponseResult<DetailRestaurantDTO>> GetDetailRestaurant<T>();
        Task<ResponseResult<FloorPlan>> PostScene<T>(FloorPlan floorPlan);
        Task<ResponseResult<FloorPlan>> GetScene<T>();
    }
}

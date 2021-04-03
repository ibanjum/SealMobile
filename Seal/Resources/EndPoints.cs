using System;
namespace Seal.Resources
{
    public class EndPoints
    {
        public static string GetRestaurantBasicInfo = "/GetRestaurantBasicInfo?id={0}";
        public static string PostRestaurantBasicInfo = "/PostRestaurantBasicInfo?id={0}";
        public static string PostRestaurantHours = "/PostRestaurantHours?id={0}";
        public static string PostRestaurantMenuItems = "/PostRestaurantMenuItems?id={0}";
        public static string GetDetailRestaurant = "/GetDetailRestaurant?id={0}";
        public static string PostRestaurantAdditionalInfo = "/PostRestaurantAdditionalInfo?id={0}";

        public static string PostFloorPlan = "/PostFloorPlan?id={0}";
        public static string GetFloorPlan = "/GetFloorPlan?id={0}";
    }
}

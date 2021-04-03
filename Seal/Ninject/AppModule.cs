using System;
using Ninject.Modules;
using Seal.Controllers;
using Seal.DataClients;
using Seal.Navigation;
using Seal.Pages.AdminDashboard;
using Seal.RestaurantAdminPages;
using Seal.ViewModels;
using Seal.ViewModels.RestaurantSignUpVMs;
using Xamarin.Forms;

namespace Seal.Ninject
{
    public class AppModule : NinjectModule
    {
        private readonly INavigation navigation;

        public AppModule(INavigation nav)
        {
            navigation = nav;
        }
        public override void Load()
        {
            // service dependencies

            //Navigation Service
            Bind<INavigation>().ToMethod(x => navigation).InSingletonScope();
            Bind<INavigationService>().To<NavigationService>().InSingletonScope();

            //Service
            Bind<IServiceController>().To<ServiceController>().InSingletonScope();

            //Data Clients
            Bind<IRestaurantSignUpDataClient>().To<RestaurantSignUpDataClient>().InSingletonScope();
            Bind<IDetailRestaurantDataClient>().To<DetailRestaurantDataClient>().InSingletonScope();

            // ViewModels
            Bind<GetStartedVM>().ToSelf();
            Bind<BasicInfoVM>().ToSelf();
            Bind<RestaurantHoursVM>().ToSelf();
            Bind<MenuOptionsVM>().ToSelf();
            Bind<AddMenuItemsVM>().ToSelf();
            Bind<AdditionalInfoVM>().ToSelf();
            Bind<UpdateFloorPlanVM>().ToSelf();
            Bind<SetUpFinishVM>().ToSelf();
            Bind<AdminDashboardVM>().ToSelf();
        }

        public void Map(INavigationService navigationService)
        {
            // Register view mappings
            navigationService.RegisterMapping(
                typeof(GetStartedVM),
                typeof(GetStarted));
            navigationService.RegisterMapping(
               typeof(BasicInfoVM),
               typeof(BasicInfoPage));

            navigationService.RegisterMapping(
               typeof(RestaurantHoursVM),
               typeof(RestaurantHoursPage));

            navigationService.RegisterMapping(
               typeof(MenuOptionsVM),
               typeof(MenuOptionsPage));

            navigationService.RegisterMapping(
               typeof(AddMenuItemsVM),
               typeof(AddMenuItemPage));

            navigationService.RegisterMapping(
               typeof(AdditionalInfoVM),
               typeof(AdditionInfoPage));

            navigationService.RegisterMapping(
               typeof(UpdateFloorPlanVM),
               typeof(UpdateFloorPlanPage));

            navigationService.RegisterMapping(
               typeof(SetUpFinishVM),
               typeof(SetUpFinishPage));

            navigationService.RegisterMapping(
               typeof(AdminDashboardVM),
               typeof(AdminDashboardPage));
        }
    }
}

using Seal.DataClients;
using Seal.Navigation;

namespace Seal.ViewModels
{
    public partial class AdminDashboardVM : BaseViewModel
    {

        private readonly INavigationService _navService;
        private readonly IDetailRestaurantDataClient _detailRestaurantDataClient;

        public AdminDashboardVM(IDetailRestaurantDataClient detailRestaurantDataClient, INavigationService navService) : base(navService)
        {
            _navService = navService;
            _detailRestaurantDataClient = detailRestaurantDataClient;

            InitializeInfo();
        }
    }
}

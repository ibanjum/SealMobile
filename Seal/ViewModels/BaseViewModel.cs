using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Seal.Navigation;

namespace Seal.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel(INavigationService navService)
        {
        }

        public Task Init()
        {
            return Task.CompletedTask;
        }


        public virtual void OnNavigatedTo()
        {
            // called when view appearing
        }

        public virtual void NavigateBack()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel
    {
        public BaseViewModel(INavigationService navService) : base(navService)
        {
        }

        public abstract Task Init(TParameter parameter);
    }
}

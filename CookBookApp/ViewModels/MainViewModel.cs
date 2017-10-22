

namespace CookBookApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
 
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
   

        public MainViewModel()
        {
            CurrentViewModel = new FirstViewModel();
        }                                             
    }
}

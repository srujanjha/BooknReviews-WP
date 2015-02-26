using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private NewYorkTimesViewModel _newYorkTimesModel;
       private TheGuardianViewModel _theGuardianModel;
       private NYRBViewModel _nYRBModel;
       private RTBookReviewsViewModel _rTBookReviewsModel;
       private TheIndependentViewModel _theIndependentModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = NewYorkTimesModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public NewYorkTimesViewModel NewYorkTimesModel
        {
            get { return _newYorkTimesModel ?? (_newYorkTimesModel = new NewYorkTimesViewModel()); }
        }
 
        public TheGuardianViewModel TheGuardianModel
        {
            get { return _theGuardianModel ?? (_theGuardianModel = new TheGuardianViewModel()); }
        }
 
        public NYRBViewModel NYRBModel
        {
            get { return _nYRBModel ?? (_nYRBModel = new NYRBViewModel()); }
        }
 
        public RTBookReviewsViewModel RTBookReviewsModel
        {
            get { return _rTBookReviewsModel ?? (_rTBookReviewsModel = new RTBookReviewsViewModel()); }
        }
 
        public TheIndependentViewModel TheIndependentModel
        {
            get { return _theIndependentModel ?? (_theIndependentModel = new TheIndependentViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            NewYorkTimesModel.ViewType = viewType;
            TheGuardianModel.ViewType = viewType;
            NYRBModel.ViewType = viewType;
            RTBookReviewsModel.ViewType = viewType;
            TheIndependentModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                NewYorkTimesModel.LoadItems(isNetworkAvailable),
                TheGuardianModel.LoadItems(isNetworkAvailable),
                NYRBModel.LoadItems(isNetworkAvailable),
                RTBookReviewsModel.LoadItems(isNetworkAvailable),
                TheIndependentModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        /// <summary>
        /// Refresh ViewModel items asynchronous
        /// </summary>
        public async Task RefreshData(bool isNetworkAvailable)
        {
            var refreshTasks = new Task[]
            { 
                NewYorkTimesModel.RefreshItems(isNetworkAvailable),
                TheGuardianModel.RefreshItems(isNetworkAvailable),
                NYRBModel.RefreshItems(isNetworkAvailable),
                RTBookReviewsModel.RefreshItems(isNetworkAvailable),
                TheIndependentModel.RefreshItems(isNetworkAvailable),
            };
            await Task.WhenAll(refreshTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await RefreshData(NetworkInterface.GetIsNetworkAvailable());
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}

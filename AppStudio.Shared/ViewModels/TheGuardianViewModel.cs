using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class TheGuardianViewModel : ViewModelBase<RssSchema>
    {
        override protected string CacheKey
        {
            get { return "TheGuardianDataSource"; }
        }

        private RelayCommandEx<RssSchema> itemClickCommand;
        public RelayCommandEx<RssSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<RssSchema>(
                        (item) =>
                        {
                            NavigationServices.CurrentViewModel = this;
                            this.SelectedItem = item;

                            NavigationServices.NavigateToPage("TheGuardianDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected IDataSource<RssSchema> CreateDataSource()
        {
            return new TheGuardianDataSource(); // RssDataSource
        }

        override public Visibility GoToSourceVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void GoToSource()
        {
            base.GoToSource("{FeedUrl}");
        }

        override public Visibility RefreshVisibility
        {
            get { return ViewType == ViewTypes.List ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public void NavigateToSectionList()
        {
            NavigationServices.NavigateToPage("TheGuardianList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("TheGuardianDetail");
        }
    }
}

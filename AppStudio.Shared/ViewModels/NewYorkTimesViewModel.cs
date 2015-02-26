using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class NewYorkTimesViewModel : ViewModelBase<RssSchema>
    {
        override protected string CacheKey
        {
            get { return "NewYorkTimesDataSource"; }
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

                            NavigationServices.NavigateToPage("NewYorkTimesDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected IDataSource<RssSchema> CreateDataSource()
        {
            return new NewYorkTimesDataSource(); // RssDataSource
        }

        override public Visibility PinToStartVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void PinToStart()
        {
            base.PinToStart("NewYorkTimesDetail", "{Title}", "{Summary}", "{ImageUrl}");
        }

        override public Visibility ShareItemVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public Visibility TextToSpeechVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected async void TextToSpeech()
        {
            await base.SpeakText("Summary");
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
            NavigationServices.NavigateToPage("NewYorkTimesList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("NewYorkTimesDetail");
        }
    }
}

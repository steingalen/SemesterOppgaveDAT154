using Mobile.Common;
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Mobile.DataModel;

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace Mobile
{
    public sealed partial class ListOfRoomTasksPage : Page
    {
        private const string FirstGroupName = "TaskTypes";
        private const string SecondGroupName = "RoomTasks";

        private readonly NavigationHelper _navigationHelper;
        private readonly ResourceLoader _resourceLoader = ResourceLoader.GetForCurrentView("Resources");
        private bool _hasLoaded = false;

        public ListOfRoomTasksPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this._navigationHelper = new NavigationHelper(this);
            this._navigationHelper.LoadState += this.NavigationHelper_LoadRoles;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this._navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel { get; } = new ObservableDictionary();
        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadRoles(object sender, LoadStateEventArgs e) {

            // Checks if the content has already been loaded
            if (_hasLoaded)
                return;

            _hasLoaded = true;

            try {
                // Fetches all the taskTypes
                var taskTypeVm = new TaskTypeVM();
                await taskTypeVm.Populate();
                DefaultViewModel[FirstGroupName] = taskTypeVm;

                // Fetches all the roomtask based on the tasktype
                var roomTasks = new RoomTasksVM(taskTypeVm.Items[0].Id);
                await roomTasks.Populate();
                DefaultViewModel[SecondGroupName] = roomTasks;

                // Creates a piviot element for each taskType
                for (var index = 0; index < taskTypeVm.Items.Count; index++)
                {

                    var pi = new PivotItem { Header = taskTypeVm.Items[index].Type };

                    if (index == 0)
                    {
                        var customControl = new RoomTaskControl(roomTasks);
                        pi.Content = customControl;
                        customControl.TaskClickedEvent += OpenTaskEvent;
                    }

                    PivotControl.Items.Add(pi);
                }
                PivotControl.SelectionChanged += ChooseRole;


            } catch (System.Runtime.Serialization.SerializationException) {

                _hasLoaded = false;
            }
           

            
        }

        /// <summary>
        /// Event is called each time a roomtask is clicked in the listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTaskEvent(object sender, ItemClickEventArgs e)
        {
            var task = e.ClickedItem as RoomTaskViewModel;

            if (task == null)
                return;
            
            if (!Frame.Navigate(typeof(SingleRoomTaskPage), task))
            {
                throw new Exception(this._resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }

        }

        /// <summary>
        /// Event get's called each time the user chooses a role (Maintenence, Cleaning or Room service)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ChooseRole(object sender, RoutedEventArgs e) {
            var piviot = PivotControl.SelectedItem as PivotItem;
            
            if (piviot == null)
                return;

            // PiviotItem has data
            if (piviot.Content != null) {
                return;
            }

            var taskTypeVm = DefaultViewModel[FirstGroupName] as TaskTypeVM;

            if (taskTypeVm == null)
                return;
           
            var taskTypeId = taskTypeVm.GetTaskTypeIdBasedOnTaskType(piviot.Header.ToString());


            // Fetches all the roomtask based on the tasktype
            var roomTasks = new RoomTasksVM(taskTypeId);
            await roomTasks.Populate();
            DefaultViewModel[SecondGroupName] = roomTasks;

            var customControl = new RoomTaskControl(roomTasks);
            piviot.Content = customControl;
            customControl.TaskClickedEvent += OpenTaskEvent;
        }



        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this._navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}

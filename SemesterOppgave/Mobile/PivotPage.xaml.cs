﻿using Mobile.Common;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HttpRequest;
using Mobile.DataModel;
using Models;
// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace Mobile
{
    public sealed partial class PivotPage : Page
    {
        private const string FirstGroupName = "TaskTypes";
        private const string SecondGroupName = "RoomTasks";

        private readonly NavigationHelper _navigationHelper;

        public PivotPage()
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
        public List<TaskType> TaskTypes { get; private set; } = new List<TaskType>();
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
        private async void NavigationHelper_LoadRoles(object sender, LoadStateEventArgs e)
        {
            TaskTypes = JsonSerializer<TaskType>.DeSerializeAsList(await ApiRequests.Get(ApiUrl.TASKTYPES));

            foreach (var taskType in TaskTypes) {
                var tasks =
                    JsonSerializer<RoomTask>.DeSerializeAsList(await ApiRequests.Get(ApiUrl.ROOM_TASKS_BY_TASK, taskType.Id));
                var pi = new PivotItem() {
                    Header = taskType.Type,
                    Content = new RoomTaskControl(tasks)
                };
                PivotControl.Items.Add(pi);
            }

            //var tasks = new TaskTypeVM();
            //await tasks.Populate();

            // this.DefaultViewModel[FirstGroupName] = tasks;
        }
        

        

        /// <summary>
        /// Adds an item to the list when the app bar button is clicked.
        /// </summary>
        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = this.PivotControl.SelectedIndex == 0 ? FirstGroupName : SecondGroupName;
            var group = this.DefaultViewModel[groupName] as TaskTypeVM;
            var nextItemId = group.Items.Count + 1;
            //var newItem = new SampleDataItem(
             //   string.Format(CultureInfo.InvariantCulture, "Group-{0}-Item-{1}", this.pivot.SelectedIndex + 1, nextItemId),
             //   string.Format(CultureInfo.CurrentCulture, this._resourceLoader.GetString("NewItemTitle"), nextItemId),
              //  string.Empty,
              //  string.Empty,
              //  this._resourceLoader.GetString("NewItemDescription"),
              //  string.Empty);

          //  group.Items.Add(newItem);

            // Scroll the new item into view.
            var container = this.PivotControl.ContainerFromIndex(this.PivotControl.SelectedIndex) as ContentControl;
            var listView = container.ContentTemplateRoot as ListView;
           // listView.ScrollIntoView(newItem, ScrollIntoViewAlignment.Leading);
        }


        private async void ChooseRole(object sender, ItemClickEventArgs e) {
            var taskType = e.ClickedItem as TaskType;

            if (taskType == null)
                return;

            var tasks = new RoomTaskVM(taskType.Id);
            await tasks.Populate();

            this.DefaultViewModel[SecondGroupName] = tasks;
        }

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
         //   var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
         //   if (!Frame.Navigate(typeof(ItemPage), itemId))
          //  {
          //      throw new Exception(this._resourceLoader.GetString("NavigationFailedExceptionMessage"));
          //  }
        }

        /// <summary>
        /// Loads the content for the second pivot item when it is scrolled into view.
        /// </summary>
        private async void SecondPivot_Loaded(object sender, RoutedEventArgs e)
        {
          //  var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-2");
          //  this.DefaultViewModel[SecondGroupName] = sampleDataGroup;
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
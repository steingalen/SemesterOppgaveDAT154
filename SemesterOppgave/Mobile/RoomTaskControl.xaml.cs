using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Mobile.Common;
using Models;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Mobile
{
    public sealed partial class RoomTaskControl : UserControl
    {
        public RoomTaskControl(RoomTasksVM roomTaskViewModel)
        {
            this.InitializeComponent();
            
            RoomTasksListView.DataContext = roomTaskViewModel;
        }
        

        public delegate void TaskClickedDelegate(object sender, ItemClickEventArgs e);
        public event TaskClickedDelegate TaskClickedEvent;
        

        private void TaskClicked(object sender, ItemClickEventArgs e) {
            var task = e.ClickedItem as RoomTaskVM;

            if (task == null)
                return;

            TaskClickedEvent?.Invoke(sender, e);
        }
    }
}

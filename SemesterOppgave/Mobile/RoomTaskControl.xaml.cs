using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Models;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Mobile
{
    public sealed partial class RoomTaskControl : UserControl
    {
        public RoomTaskControl(List<RoomTask> romTask)
        {
            this.InitializeComponent();
            RoomTasks = romTask;
            RoomTasksListView.ItemsSource = RoomTasks;

        }

        public List<RoomTask> RoomTasks { get; private set; }

        private void TaskClicked(object sender, ItemClickEventArgs e) {
            var task = e.ClickedItem as RoomTask;

            if (task == null)
                return;


        }
    }
}

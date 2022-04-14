using Avalonia.Controls;
using LabMileshko9.ViewModels;
using LabMileshko9.Models;
using System.Collections.ObjectModel;
using Avalonia.Controls.Primitives;
using System.Linq;
using System.IO;


namespace LabMileshko9.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var carousel = this.FindControl<Carousel>("carousel");
            this.FindControl<Button>("next").Click += delegate
            {
                carousel.Next();
                ObservableCollection<MyImage> items = carousel.Items as ObservableCollection<MyImage>;
                var currentItem = carousel.SelectedItem;
                if (currentItem == items[items.Count - 1])
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.EnableNext = false;
                }
                if (currentItem != items[0])
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.EnableBack = true;
                }

            };
            this.FindControl<Button>("back").Click += delegate
            {
                carousel.Previous();
                ObservableCollection<MyImage> items = carousel.Items as ObservableCollection<MyImage>;
                var currentItem = carousel.SelectedItem;
                if (currentItem == items[0])
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.EnableBack = false;
                }
                if (currentItem != items[items.Count - 1])
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.EnableNext = true;
                }
            };
        }
        private void ClickForLoadNodes(object sender, TemplateAppliedEventArgs e)
        {
            ContentControl treeViewItem = sender as ContentControl;
            MyImage selectedNode = treeViewItem.DataContext as MyImage;
            selectedNode.LoadSubfolders();
        }
        public void OnTreeViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = this.DataContext as MainWindowViewModel;
            context.ChangeSelectedImages(e.AddedItems[0]);
        }
    }
}
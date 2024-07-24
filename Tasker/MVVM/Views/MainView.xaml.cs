using Tasker.MVVM.ViewModels;

using Tasker.MVVM.Models;
namespace Tasker.MVVM.Views;


    public partial class MainView : ContentPage
    {
        private MainViewModel mainViewModel = new MainViewModel();

        public MainView()
        {
            InitializeComponent();
            BindingContext = mainViewModel;
        }

        private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            mainViewModel.UpdateData();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            foreach (var category in mainViewModel.Categories)
            {
                category.IsSelected = false;
            }

            var taskView = new NewTaskView
            {
                BindingContext = new NewTaskViewModel
                {
                    Tasks = mainViewModel.Tasks,
                    Categories = mainViewModel.Categories,
                }
            };
            Navigation.PushAsync(taskView);
        }

        private void DeleteCategoryButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button?.CommandParameter as Category;
            if (category != null)
            {
                var tasksToRemove = mainViewModel.Tasks.Where(t => t.CategoryId == category.Id).ToList();
                foreach (var task in tasksToRemove)
                {
                    mainViewModel.Tasks.Remove(task);
                }
                mainViewModel.Categories.Remove(category);
                mainViewModel.UpdateData();
            }
        }
    }

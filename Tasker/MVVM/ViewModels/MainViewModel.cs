using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.MVVM.Models;

namespace Tasker.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
   public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }         
        public ObservableCollection<MyTask>Tasks { get; set; }

        public MainViewModel()
        {
            FillData();
            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        private void Tasks_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void FillData()
        {
            Categories = new ObservableCollection<Category>
            {
                new Category
                {
                    Id = 1,
                    CategoryName = "Shopping",
                    Color = "#0959db"
                    
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Cleaning",
                    Color = "#bcdb09"
                  
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Studying",
                    Color = "#09db68"
                    
                }
            };

            Tasks = new ObservableCollection<MyTask>
            {
                new MyTask
                {
                    TaskName = "Buy books",
                    Completed = false,
                    CategoryId= 1
                 
                },
                new MyTask
                {
                    TaskName = "Buy clothes",
                    Completed = false,
                    CategoryId= 1
                    
                },
                new MyTask
                {
                    TaskName = "Buy groceries",
                    Completed = true,
                    CategoryId= 1
                    
                },
                new MyTask
                {
                    TaskName = "Clean room",
                    Completed = false,
                    CategoryId= 2
                    
                },
                new MyTask
                {
                    TaskName = "Mow the lawn",
                    Completed = true,
                    CategoryId= 2
                  
                },
                new MyTask
                {
                    TaskName = "Study .net maui",
                    Completed = false,
                    CategoryId= 3
                   
                },
                new MyTask
                {
                    TaskName = "Study C#",
                    Completed = true,
                    CategoryId= 3
                    
                }
            };
           
        }

        public void UpdateData()
        {
            foreach(var c in Categories)
            {
                var tasks = from t in Tasks
                            where t.CategoryId == c.Id
                            select t;

                var completed = from t in tasks
                                where t.Completed == true
                                select t;
                var notCompleted = from t in tasks
                                   where t.Completed == false
                                   select t;

                c.PendingTasks= notCompleted.Count();
                c.Percentage=(float)completed.Count()/(float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var catColor =
                    (from c in Categories
                     where c.Id == t.CategoryId
                     select c.Color).FirstOrDefault();
                t.TaskColor = catColor;   
            }
        }
    }    
}

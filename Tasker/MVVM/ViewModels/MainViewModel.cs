using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.MVVM.Models;

namespace Tasker.MVVM.ViewModels
{
   public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
         
        public ObservableCollection<MyTask>Tasks { get; set; }
        public MainViewModel()
        {
            FillData();
        }

        private void FillData()
        {
            Categories = new ObservableCollection<Category>
            {
                new Category
                {
                    Id = 1,
                    CategoryName = "Test1",
                    Color = "#0c2b5c"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Test2",
                    Color = "#380624"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Test3",
                    Color = "#05520e"
                }
            };

            Tasks = new ObservableCollection<MyTask>
            {
                new MyTask
                {
                    TaskName = "Test1",
                    Completed = false,
                    CategoryId= 1
                 
                },
                new MyTask
                {
                    TaskName = "Test2",
                    Completed = false,
                    CategoryId= 1
                    
                },
                new MyTask
                {
                    TaskName = "Test3",
                    Completed = true,
                    CategoryId= 1
                    
                },
                new MyTask
                {
                    TaskName = "Test4",
                    Completed = false,
                    CategoryId= 2
                    
                },
                new MyTask
                {
                    TaskName = "Test5",
                    Completed = true,
                    CategoryId= 2
                  
                },
                new MyTask
                {
                    TaskName = "Test6",
                    Completed = false,
                    CategoryId= 3
                   
                },
                new MyTask
                {
                    TaskName = "Test7",
                    Completed = true,
                    CategoryId= 3
                    
                }
            };

            UpdateData();

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

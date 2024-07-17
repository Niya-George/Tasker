using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Category
    {
        public string CategoryName {get; set;}
        public int Id { get; set; }

        public string Color { get; set; }

        public float Percentage { get; set; }

        public int PendingTasks { get; set; }

    }
}

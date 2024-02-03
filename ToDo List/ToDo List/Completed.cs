using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_List
{
    internal class Completed : TodoTask
    {
        public Completed(TodoTask task)
        {
            Name = task.Name;
            Description = task.Description;
            SetStatus();
        }
        override public string SetStatus()
        {
            this.Status = "done";
            return this.Status;
        }
    }
}

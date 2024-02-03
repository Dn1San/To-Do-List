using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ToDo_List
{
    internal class InProgress : TodoTask
    {
        public InProgress(TodoTask task)
        {
            Name = task.Name;
            Description = task.Description;
            SetStatus();
        }
        override public string SetStatus()
        {
            this.Status = "in progress";
            return this.Status;
        }
    }
}

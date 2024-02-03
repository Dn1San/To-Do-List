using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_List
{
    internal class Todo : TodoTask
    {
        public Todo(string name, string description)
        {
            Name = name;
            Description = description;
            SetStatus();
        }
        public Todo(TodoTask task)
        {
            SetStatus();
        }
        override public string SetStatus()
        {
            this.Status = "todo";
            return this.Status;
        }
    }
}

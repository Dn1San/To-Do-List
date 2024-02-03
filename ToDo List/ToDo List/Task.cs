using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_List
{
    internal class TodoTask
    {
        private int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public TodoTask() { } 
        public TodoTask(string name, string description)
        {
            Id++;
            Name = name;
            Description = description;
        }
        public TodoTask(TodoTask task) 
        { 

        }

        virtual public string SetStatus()
        {
            return this.Status = "";
        }
    }
}

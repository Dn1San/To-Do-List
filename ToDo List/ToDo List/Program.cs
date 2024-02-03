// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using System.Threading.Tasks;
using ToDo_List;

string command;
List<TodoTask> tasks = new List<TodoTask>();
bool exit = false;
int processors = Environment.ProcessorCount;


//Test
/*Todo test = new Todo("wash clothes", "wash your clothes!");
tasks.Add(test);
Todo test2 = new Todo("eat", "eat dinner!");
tasks.Add(test2);*/
if (args.Length == 0)
{
    Console.Write("ERROR: Please use either the 'new' or 'load' command followed my the name of the file.");
    return; 
}
else if (args.Contains("new"))
{
    WelcomeMessage();
    MainProgram();

    //Test
/*    foreach (TodoTask task in tasks)
    {
        Console.WriteLine(task.Name + ": " + task.Description);
    }*/
}
else if (args.Contains("load"))
{
    Load(args[1] + ".json");

    WelcomeMessage();
    MainProgram();

    //Test
    /*foreach (TodoTask task in tasks)
    {
        Console.WriteLine(task.Name + ": " + task.Description);
    }*/
}
else if (args.Contains("-v"))
{
    Console.WriteLine("ToDo List Version: 1.0");
    //Test
    //Console.WriteLine("This computer has " + processors + " processors.");
}
else
{
    Console.Write("ERROR: Please use either the 'new' or 'load' command followed my the name of the file.");
    return;
}

void MainProgram()
{
    while (!exit)
    {
        Console.WriteLine("please enter a command:");
        command = Console.ReadLine();

        switch (command)
        {
            case null:
                Console.WriteLine("please use one of these commands!");
                DisplayUsage();
                break;
            case "help":
                DisplayUsage();
                break;
            case "all":
                IEnumerable<string> allTasks = tasks.Select(c => c.Name);
                foreach (string task in allTasks) { Console.WriteLine(task); }
                break;
            case "todo":
                foreach (TodoTask task in tasks)
                {
                    if (task.Status == "todo")
                    {
                        Console.WriteLine(task.Name + ": " + task.Description);
                    }
                }
                if (tasks.Count == 0)
                {
                    Console.WriteLine("There are no tasks in your todo list!");
                }
                break;
            case "view in progress":
                bool taskFound = false;
                foreach (TodoTask task in tasks)
                {
                    if (task.Status == "in progress")
                    {
                        Console.WriteLine(task.Name + ": " + task.Description);
                        taskFound = true;
                    }
                }
                if (tasks.Count == 0)
                {
                    Console.WriteLine("There are no tasks in your todo list!");
                }
                else if (!taskFound) { Console.WriteLine("There are no tasks set to in progress!"); }
                break;
            case "view done":
                taskFound = false;
                foreach (TodoTask task in tasks)
                {
                    if (task.Status == "done")
                    {
                        Console.WriteLine(task.Name + ": " + task.Description);
                        taskFound = true;
                    }
                }
                if (tasks.Count == 0)
                {
                    Console.WriteLine("There are no tasks in your todo list!");
                }
                else if (!taskFound) { Console.WriteLine("There are no tasks set to done!"); }
                break;

            case "add":
                Console.WriteLine("What is the name of the task?");
                string taskName = Console.ReadLine();
                Console.WriteLine("What is the task description?");
                string taskDescription = Console.ReadLine();
                tasks.Add(new Todo(taskName, taskDescription));
                Console.WriteLine("Task has been successfully added!");
                break;
            case "in progress":
                Console.WriteLine("What is the name of the task?");
                taskFound = false;
                taskName = Console.ReadLine();
                foreach (TodoTask task in tasks.ToList())
                {
                    if (task.Name == taskName)
                    {
                        taskFound = true;
                        tasks.Add(new InProgress(task));
                        tasks.Remove(task);
                        Console.WriteLine("The task has been successfully set to in progress!");
                    }
                }
                if (!taskFound) { Console.WriteLine("The current task is not on your todo list!"); }
                break;
            case "done":
                Console.WriteLine("What is the name of the task?");
                taskFound = false;
                taskName = Console.ReadLine();
                foreach (TodoTask task in tasks.ToList())
                {
                    if (task.Name == taskName)
                    {
                        taskFound = true;
                        tasks.Add(new Completed(task));
                        tasks.Remove(task);
                        Console.WriteLine("The task has been successfully set to done!");
                    }
                }
                if (!taskFound) { Console.WriteLine("The current task is not on your todo list!"); }
                break;
            case "clear":
                tasks.Clear();
                Console.WriteLine("all tasks have successfully been removed!");
                break;
            case "delete":
                Console.WriteLine("What is the name of the task?");
                taskFound = false;
                taskName = Console.ReadLine();
                foreach (TodoTask task in tasks.ToList())
                {
                    if (task.Name == taskName)
                    {
                        tasks.Remove(task);
                        Console.WriteLine("The task has been successfully removed!");
                        taskFound = true;
                    }
                }
                if (!taskFound) { Console.WriteLine("The current task is not on your todo list!"); }
                break;
            case "recent":
                IEnumerable<TodoTask> recent = tasks.TakeLast(3);
                foreach (TodoTask task in recent)
                {
                    Console.WriteLine(task.Name + " : " + task.Description);
                }
                break;
            case "exit":
                exit = true;
                Save(tasks, args[1] + ".json");
                break;
        }
    }
}
void WelcomeMessage()
{
    Console.WriteLine("Welcome to Dani's todo list!\n " +
        "\n" +
        "To began please provide a command if you need help" +
        " understanding the commands type help.");
}
void DisplayUsage()
{
    Console.WriteLine("How to use:");
    Console.WriteLine("Usage: help                      - displays list of commands ");
    Console.WriteLine("Usage: all                       - displays all task names");
    Console.WriteLine("Usage: todo                      - displays current todo tasks");
    Console.WriteLine("Usage: view in progress          - displays tasks that are in progress");
    Console.WriteLine("Usage: view done                 - displays tasks that are done");
    Console.WriteLine("Usage: add                       - adds item to your todo list");
    Console.WriteLine("Usage: in progress               - sets task to in progress");
    Console.WriteLine("Usage: done                      - sets task to done");
    Console.WriteLine("Usage: clear                     - clears your todo list");
    Console.WriteLine("Usage: delete                    - deletes a task in your todo list");
    Console.WriteLine("Usage: recent                    - View your most recent todo tasks");
    Console.WriteLine("Usage: exit                      - exit todo list app and save");
}
void Save(List<TodoTask> list, string fileName)
{
    string jsonList = JsonSerializer.Serialize(list);
    File.WriteAllText(fileName, jsonList);
}

void Load(string fileName)
{
    string jsonRead = File.ReadAllText(fileName);
    List<TodoTask> listJson = JsonSerializer.Deserialize<List<TodoTask>>(jsonRead);
    foreach (TodoTask task in listJson)
    {
        tasks.Add(task);
    }
}
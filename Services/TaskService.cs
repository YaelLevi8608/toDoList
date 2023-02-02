using System.Text.Json;
using myTasks.Interfaces;
using myTasks.Models;
using myTasks.Service;

namespace myTasks.Services
{
    public class TaskService : ITaskService
    {
        List<MyTask>? tasks { get;}
        private IWebHostEnvironment webHost;
        private string filePath;
        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "Task.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<MyTask>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
        }

        public List<MyTask>  GetAll(string token) {
        string id=TokenService.Decode(token);
        List<MyTask> MyTaskById=new List<MyTask>();
        foreach( MyTask t in tasks)
            if(t.UserId.ToString()==id)
            MyTaskById.Add(t);
        // return ( List<MyTask> ) tasks.Where(t => t.UserId.ToString().Equals(id)).ToList();
        return MyTaskById;
        }

        public MyTask? Get(int id) => tasks?.FirstOrDefault(t => t.Id == id);
        int count=0;
        public MyTask Add(MyTask task)
        {
            
            task.Id = count++;
            tasks.Add(task);
            saveToFile();
            return task;
        }

        

        public void Update(MyTask task)
        {
            int index1 = tasks.FindIndex(t => t.Id == task.Id);
            
            
            if (index1 == -1)
                return;

            tasks[index1] = task;
            saveToFile();
        }

        public void Delete(int id)
        {
            var t = Get(id);
            if (t is null)
                return;

            tasks.Remove(t);
            saveToFile();
        }
         public int Count => tasks.Count();
    }

}

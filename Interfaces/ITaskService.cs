using myTasks.Models;


namespace myTasks.Interfaces
{
    public interface ITaskService
    {
        List<MyTask>? GetAll(string token);
        MyTask? Get(int id);
        MyTask Add(MyTask task);
        void Delete(int id);
        void Update(MyTask task);
        int Count {get;}
    }
}
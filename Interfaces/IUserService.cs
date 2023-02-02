using myTasks.Models;


namespace myTasks.Interfaces
{
    public interface IUserService
    {
        List<MyUser>? GetAll();
        MyUser? Get(int id);
        MyUser Add(MyUser user);
        void Delete(int id);
        // void Update(MyUser user);
        int Count {get;}
        
    }
}
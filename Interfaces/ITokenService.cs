using myTasks.Models;


namespace myTasks.Interfaces{
    public interface ITokenService{
        int Decode(String token);
    }
}
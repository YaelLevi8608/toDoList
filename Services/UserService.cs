
using System.Text.Json;
using myTasks.Interfaces;
using myTasks.Models;


namespace myTasks.Services
{
    public class UserService : IUserService
    {
        List<MyUser>? users { get;}
        private IWebHostEnvironment webHost;
        private string filePath;
        public UserService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "User.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                users = JsonSerializer.Deserialize<List<MyUser>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }

        public List<MyUser> GetAll() { 
        return users;
        }
        public MyUser? Get(int id) => users?.FirstOrDefault(t => t.Id == id);
        int countId=0;
        public MyUser Add(MyUser user)
        {
            
             user.Id=Count;
        //    System.Console.WriteLine(user.Name);
            users?.Add(user);
            saveToFile();
            return user;
        }

        public void Delete(int id)
        {
            var u = Get(id);
            if (u is null)
                return;

            users?.Remove(u);
            saveToFile();
        }
       static int counter=1;
        public int Count => counter++;
     }

}

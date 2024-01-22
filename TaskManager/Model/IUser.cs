using Microsoft.AspNetCore.Identity;

namespace TaskManager.Model
{
    public interface IUser
    {

        public void Register(UserBody body);
        public UserBody Login(Login login);
        public List<UserBody> GetUserBodies();
       
    }
    public class UserDetails : IUser
    {
        public List<UserBody> userbody = new List<UserBody> 
        {
            new UserBody {Id=1,Name="shaan",Password="1234567890",Role=Role.Admin },
            new UserBody {Id=2,Name="shaaan",Password="12345678900",Role=Role.User }
        };

        public List<UserBody> GetUserBodies()
        {
            return userbody;
        }
        public void Register(UserBody body)
        {
            body.Id = userbody.Count + 1;
            userbody.Add(body);
        }
        public UserBody Login(Login login)
        {
            var checkuser = userbody.FirstOrDefault(x=>x.Name == login.Name && x.Password == login.Password);
            return checkuser;
        }
    }
}

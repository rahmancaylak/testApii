using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApii.Entity;
using testApii.Entity.Users;

namespace testApii.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByUsername(string username);
        public void Register(RegisterRequest entity);
        public LoginResponse Login(LoginRequest entity);
    }
}

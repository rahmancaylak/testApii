using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApii.DAL.Interfaces;
using testApii.Entity;
using testApii.Entity.API;
using testApii.Entity.Users;

namespace testApii.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IMapper _mapper;
        public UserRepository(TestDbContext context, IMapper mapper)  : base(context)
        {
            _mapper = mapper;
        }

        public User GetByUsername(string username)
        {
            return _context.Set<User>().FirstOrDefault(x => x.Username == username);
        }
        public void Register(RegisterRequest model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        public LoginResponse Login(LoginRequest entity)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == entity.Username);
            var response = _mapper.Map<LoginResponse>(user);
            return response;
        }
    }
}

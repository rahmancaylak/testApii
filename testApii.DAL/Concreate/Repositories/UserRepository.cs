using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApii.DAL.Interfaces;
using testApii.Entity;
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
            if (_context.Users.Any(x => x.Username == model.Username))
            {
                throw new("Username '" + model.Username + "' is already taken");
            }

            // map model to new user object
            try
            {
                var user = _mapper.Map<User>(model);
                // hash password
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // save user
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoginResponse Login(LoginRequest entity)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == entity.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(entity.Password, user.PasswordHash))
            {
                Console.WriteLine("Username or password is incorrect");
            }

            // authentication successful
            var response = _mapper.Map<LoginResponse>(user);
            return response;
        }
    }
}

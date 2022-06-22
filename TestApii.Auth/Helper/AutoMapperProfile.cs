using AutoMapper;
using testApii.Entity;
using testApii.Entity.Users;

namespace testApii.Auth.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<User, LoginResponse>();

            // RegisterRequest -> User
            CreateMap<RegisterRequest, User>();
        }
    }
}

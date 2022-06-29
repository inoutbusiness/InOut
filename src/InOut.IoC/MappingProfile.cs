namespace InOut.IoC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }

        public static MapperConfiguration CreateMappingProfile()
            => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
    }
}

namespace FilmoSearchPortal.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Actor, ActorDto>().ReverseMap();
        CreateMap<Film, FilmDto>().ReverseMap();
        CreateMap<Review, ReviewDto>().ReverseMap();
    }
}
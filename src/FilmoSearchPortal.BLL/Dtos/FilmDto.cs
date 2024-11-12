namespace FilmoSearchPortal.BLL.Dtos;

public class FilmDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public IEnumerable<ActorDto> Actors { get; set; } = []; 
}
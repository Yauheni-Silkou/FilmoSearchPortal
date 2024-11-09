namespace FilmoSearchPortal.BLL.Dtos;

public class FilmDto : IFilmDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
}
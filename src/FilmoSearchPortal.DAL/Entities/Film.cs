namespace FilmoSearchPortal.DAL.Entities;

public class Film : IFilm
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
}
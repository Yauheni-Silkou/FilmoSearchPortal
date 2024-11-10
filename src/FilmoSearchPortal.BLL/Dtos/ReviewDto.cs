namespace FilmoSearchPortal.BLL.Dtos;

public class ReviewDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Stars { get; set; }

    public FilmDto Film { get; set; } = null!;
}
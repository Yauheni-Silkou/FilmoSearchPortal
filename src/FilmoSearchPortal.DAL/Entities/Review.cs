namespace FilmoSearchPortal.DAL.Entities;

public class Review : IReview
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Stars { get; set; }
}
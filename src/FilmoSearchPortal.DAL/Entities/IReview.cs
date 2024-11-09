namespace FilmoSearchPortal.DAL.Entities;

public interface IReview
{
    int Id { get; set; }

    string Title { get; set; }

    string Description { get; set; }

    int Stars {  get; set; }
}
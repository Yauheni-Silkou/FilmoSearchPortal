namespace FilmoSearchPortal.BLL.Dtos;

public interface IReviewDto
{
    int Id { get; set; }

    string Title { get; set; }

    string Description { get; set; }

    int Stars { get; set; }
}
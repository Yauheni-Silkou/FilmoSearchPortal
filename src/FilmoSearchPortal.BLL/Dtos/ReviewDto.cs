﻿namespace FilmoSearchPortal.BLL.Dtos;

public class ReviewDto : IReviewDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Stars { get; set; }
}
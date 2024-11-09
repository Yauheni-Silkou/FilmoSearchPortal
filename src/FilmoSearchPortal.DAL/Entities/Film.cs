﻿namespace FilmoSearchPortal.DAL.Entities;

public class Film : IFilm
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public ICollection<Actor> Actors { get; set; } = [];

    public ICollection<Review> Reviews { get; set; } = [];
}
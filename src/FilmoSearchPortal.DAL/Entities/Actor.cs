namespace FilmoSearchPortal.DAL.Entities;

public class Actor : IActor
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public ICollection<Film> Films { get; set; } = [];
}
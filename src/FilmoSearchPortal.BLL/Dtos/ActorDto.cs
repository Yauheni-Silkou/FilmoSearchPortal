namespace FilmoSearchPortal.BLL.Dtos;

public class ActorDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public ICollection<Film> Films { get; set; } = [];
}
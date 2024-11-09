namespace FilmoSearchPortal.BLL.Dtos;

public class ActorDto : IActorDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
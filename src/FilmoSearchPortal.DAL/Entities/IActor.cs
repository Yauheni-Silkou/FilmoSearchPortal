namespace FilmoSearchPortal.DAL.Entities;

public interface IActor
{
    int Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }
}
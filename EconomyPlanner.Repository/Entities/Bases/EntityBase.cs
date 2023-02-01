namespace EconomyPlanner.Repository.Entities.Bases;

public class EntityBase
{
    public int Id { get; set; }
    protected string Name { get; set; }
    public bool IsDeleted { get; set; }

    protected EntityBase(string name)
    {
        Name = name;
    }
}
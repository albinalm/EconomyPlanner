namespace EconomyPlanner.Repository.Entities.Bases;

public class EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }

    public EntityBase(string name)
    {
        Name = name;
    }
}
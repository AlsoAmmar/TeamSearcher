using System.ComponentModel.DataAnnotations;

namespace TeamSearcher.Domain.Entities;

public class Person
{
    public int Id { get; set;}
    public string Name { get; set; }
    public string Number { get; set; }
    public ICollection<TeamPerson> TeamPersons { get; set; }
}
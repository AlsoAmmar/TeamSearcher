using System.ComponentModel.DataAnnotations;

namespace TeamSearcherAPI.Models;

public class Person
{
    [Key]
    public int? Id { get; set;}
    public string Name { get; set; }
    public string Number { get; set; }
    public ICollection<TeamPerson> TeamPersons { get; set; }

    public void AddID(int num)
    {
        Id = num;
    }
}
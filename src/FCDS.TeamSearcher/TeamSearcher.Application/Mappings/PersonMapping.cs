using TeamSearcher.Application.DTOs.Person;
using TeamSearcher.Domain.Entities;

namespace TeamSearcher.Application.Mappings;

public static class PersonMapping
{
    public static PersonDto ToDto(this Person person)
    {
        return new PersonDto
        {
            Name = person.Name,
            Number = person.Number
        };
    }
}
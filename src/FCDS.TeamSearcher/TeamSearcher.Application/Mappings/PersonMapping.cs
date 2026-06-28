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

    public static Person ToEntity(this CreatePersonDto dto)
    {
        return new Person
        {
            Name = dto.Name,
            Number = dto.Number
        };
    }
    
    public static IEnumerable<PersonDto> ToDtoList(this IEnumerable<Person> persons)
    {
        if (persons == null) return Enumerable.Empty<PersonDto>();

        return persons.Select(t => t.ToDto()).ToList();
    }

    public static void UpdateEntity(this Person person, UpdatePersonDto updatedPerson)
    {
        person.Name = updatedPerson.Name;
        person.Number = updatedPerson.Number;
    }
}
from pydantic import BaseModel, ConfigDict
from TeamSearcher_Domain import Person

class PersonDto(BaseModel):
    id: int
    name: str
    number: int

    def to_entity(self) -> "Person":
        return Person(
            id = self.id,
            name = self.name,
            number = self.number
        )

class CreatePersonDto(BaseModel):
    name: str
    number: str

    model_config = ConfigDict(from_attributes=True)

    @classmethod
    def to_dto(cls, person: Person) -> "CreatePersonDto":
        return cls.model_validate(person)


class UpdatePersonDto(BaseModel):
    id: int
    name: str
    number: str

    def update_person(self, person: Person):
        person.name = self.name
        person.number = self.number
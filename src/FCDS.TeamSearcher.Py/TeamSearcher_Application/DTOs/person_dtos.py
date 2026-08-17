from pydantic import BaseModel

class PersonDto(BaseModel):
    id: int
    name: str
    number: int

class CreatePersonDto(BaseModel):
    name: str
    number: str

class UpdatePersonDto(BaseModel):
    id: int
    name: str
    number: str
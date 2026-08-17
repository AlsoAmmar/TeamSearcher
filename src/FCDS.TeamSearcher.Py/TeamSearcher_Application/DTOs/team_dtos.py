from pydantic import BaseModel
from TeamSearcher_Domain.enums import Tag

class TeamDto(BaseModel):
    id: int
    name: str
    current_count: int
    max_count: int
    tag: Tag

class CreateTeamDto(BaseModel):
    name: str
    current_count: int
    max_count: int
    tag: Tag

class UpdateTeamDto(BaseModel):
    id: int
    name: str
    current_count: int
    max_count: int
    tag: Tag
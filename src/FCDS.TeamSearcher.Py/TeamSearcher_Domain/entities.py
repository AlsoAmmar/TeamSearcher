from dataclasses import dataclass
from enums import Tag, Status

@dataclass
class Person:
    id: int
    name: str
    number: str

@dataclass
class Team:
    id: int
    name: str
    current_count: int
    max_count: int
    tag: Tag

@dataclass
class TeamPerson:
    status: Status
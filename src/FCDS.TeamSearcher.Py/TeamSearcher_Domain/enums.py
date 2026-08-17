from enum import Enum

class Tag(Enum):
    NOT_ACCEPTED = "Not Accepted"
    ACCEPTED = "Accepted"

class Status(Enum):
    NONE = "None"
    BOYS_ONLY = "Boys Only"
    GIRLS_ONLY = "Girls Only"
-- SQLite
DELETE FROM 'Person';
DELETE FROM 'Team';
DELETE FROM 'TeamPersons';

UPDATE sqlite_sequence SET seq = 1000 WHERE name = 'Person';
UPDATE sqlite_sequence SET seq = 2000 WHERE name = 'Team';
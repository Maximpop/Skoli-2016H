DROP DATABASE IF EXISTS 2410982069_pygame;
CREATE DATABASE 2410982069_pygame;
USE 2410982069_pygame;

DROP TABLE IF EXISTS pygame;
CREATE TABLE pygame(
	ID int(11) PRIMARY KEY AUTO_INCREMENT NOT NULL,
	name varchar(4) NOT NULL,
	score int(11) NOT NULL,
	moment datetime NOT NULL
);

delimiter $$

DROP FUNCTION IF EXISTS topScorer $$
CREATE FUNCTION topScorer()
returns int(11)
BEGIN
RETURN(SELECT min(score) FROM pygame);
END $$

DROP TRIGGER IF EXISTS thereCanBeOnly10 $$
CREATE TRIGGER thereCanBeOnly10
AFTER INSERT ON pygame
for each row
BEGIN
	DECLARE numPeople int(3);
	SELECT count(id) INTO numPeople FROM pygame;
	if((numPeople) > 10) then
		DELETE FROM pygame WHERE MIN(score);
	end if;
END $$

DROP PROCEDURE IF EXISTS newWinner $$
CREATE PROCEDURE newWinner(Name varchar(4), Score int(11))
BEGIN
INSERT INTO pygame (name, score, moment) VALUES (Name, Score, NOW());
END
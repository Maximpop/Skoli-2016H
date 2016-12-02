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

DROP PROCEDURE IF EXISTS newWinner $$
CREATE PROCEDURE newWinner(namee varchar(4), scoree int(11))
BEGIN
	DECLARE numPeople int(3);
	DECLARE lowest int(11);
	DECLARE oldest int(11);
	

	SELECT COUNT(id) INTO numPeople FROM pygame;

	if(numPeople > 9) then
		

		SELECT MIN(score) INTO lowest FROM pygame;
		SELECT MIN(id) INTO oldest FROM pygame;

		DELETE FROM pygame WHERE id = oldest;
	end if;



INSERT INTO pygame (name, score, moment) VALUES (namee, scoree, NOW());
END


/*
call newWinner('Pall', 10);
call newWinner('Pall', 20);
call newWinner('Pall', 30);
call newWinner('Pall', 40);
call newWinner('Pall', 50);
call newWinner('Pall', 60);
call newWinner('Pall', 70);
call newWinner('Pall', 80);
call newWinner('Pall', 90);
call newWinner('Pall', 100);
call newWinner('Alex', 110);
call newWinner('Pall', 120);



*/

delimiter $$

/*1*/
DROP FUNCTION IF EXISTS catIncome $$
CREATE FUNCTION catIncome(num int(11), flight_code int(11))
RETURNS int(11)
BEGIN
RETURN((SELECT COUNT(seatingID) FROM passengers WHERE priceid = num AND bookedFlightID IN (SELECT bookedFlightID FROM bookedflights WHERE flightCode = flight_code))*(SELECT amount FROM prices WHERE priceID = num));
END $$

DROP FUNCTION IF EXISTS flightIncome $$
CREATE FUNCTION flightIncome(flight_code int(11))
RETURNS int(11)
BEGIN
	RETURN(0.93*(93900-(catIncome(1, flight_code) ,catIncome(2, flight_code) ,catIncome(3, flight_code) ,catIncome(4, flight_code) ,catIncome(5, flight_code) ,catIncome(6, flight_code) ,catIncome(7, flight_code) ,catIncome(8, flight_code) ,catIncome(9, flight_code))));
END $$

/* 2*/
DROP FUNCTION IF EXISTS pricePerKm $$
CREATE FUNCTION pricePerKm(flight_number char(5))
RETURNS int(11)
BEGIN
	DECLARE code int(11);
	SELECT flightCode INTO code FROM flights WHERE flightNumber = flight_number;
	RETURN((flightIncome(code))/(SELECT distance FROM flightchedules));
END $$

/*3*/
DROP PROCEDURE IF EXISTS windowSeats $$
CREATE PROCEDURE windowSeats(aircraft_id char(6), flight_code int(11))
begin
SELECT CONCAT(rowNumber, seatNumber) AS Seat FROM aircraftseats 
WHERE seatID 
IN (SELECT seatID FROM passengers WHERE bookedFlightID 
	IN(SELECT bookedFlightID FROM bookedflights WHERE flightCode = flight_code))
AND seatPlacement = "w";
END $$

/*5*/
DROP PROCEDURE IF EXISTS passengerList $$
CREATE PROCEDURE passengerList(flightNum char(5), flightD DATE)
BEGIN
	
	DECLARE passengerID char(35);
	DECLARE passenger_name varchar(75);
	DECLARE passenger_seat char(4);
	DECLARE seat_placement varchar(15);

	DECLARE file_header varchar(125);
	DECLARE file_body text;
	DECLARE file_sub_footer varchar(75);
	DECLARE file_footer varchar(55);

	DECLARE done int default false;

<<<<<<< HEAD
	DECLARE writer CURSOR FOR SELECT passengers.personID, passengers.personName, concat(aircraftseats.rowNumber, aircraftseats.seatNumber), aircraftseats.seatPlacement FROM passengers INNER JOIN aircraftseats ON passengers.seatID = aircraftseats.seatID WHERE bookedFlightID IN(SELECT bookedFlightID FROM bookedflights WHERE flightCode = flightC);

	declare continue handler for not found set done = true;
=======
	DECLARE writer CURSOR 
	FOR SELECT passengers.personID, passengers.personName, concat(aircraftseats.rowNumber, aircraftseats.seatNumber), aircraftseats.seatPlacement FROM passengers INNER JOIN aircraftseats ON passengers.seatID = aircraftseats.seatID WHERE bookedFlightID IN(SELECT bookedFlightID FROM bookedflights WHERE flightCode = flightC);
>>>>>>> origin/master

	set file_body = CONCAT(flightNum , "_" , (SELECT originatingAirport FROM flightschedules WHERE flightNumber = flightNum) , "-" , (SELECT destinationAirport FROM flightschedules WHERE flightNumber = flightNum) , "_" , flightD);
	set file_body = CONCAT("Carrier: ", (SELECT aircraftType FROM aircrafts WHERE aircraftID = (SELECT aircraftID FROM flights WHERE flightNumber = flightNum)));

OPEN writer;
<<<<<<< HEAD
=======

set file_body = CONCAT(flightNum , "_" , (SELECT originatingAirport FROM flightschedules WHERE flightNumber = flightNum) , "-" , (SELECT destinationAirport FROM flightschedules WHERE flightNumber = flightNum) , "_" , flightD);

>>>>>>> origin/master
read_loop: loop

	fetch writer into passengerID, passenger_name, passenger_seat, seat_placement;

	if done then
		leave read_loop;
	end if;

	
	set file_body = concat(passengerID,";",passenger_name,";",passenger_seat,";",seat_placement,";");
end loop;

<<<<<<< HEAD
set file_body = concat("List compiled",NOW());

close writer;

END
=======
set file_body = concat("Carrier: "(SELECT aircraftType FROM aircrafts WHERE aircraftID = (SELECT aircraftID FROM flights WHERE flightNumber = flightNum)));
set file_body = concat("List compiled",NOW())

CLOSE writer;

SELECT file_body;

END $$
>>>>>>> origin/master

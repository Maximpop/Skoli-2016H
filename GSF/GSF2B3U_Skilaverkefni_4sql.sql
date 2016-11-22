delimiter $$

DROP FUNCTION IF EXISTS numPass $$
CREATE FUNCTION numPass(flight_code int(11))
RETURNS int(11)
BEGIN
	DECLARE num int(11);

	SELECT COUNT(bookedFlightID) INTO num FROM bookedflights WHERE flightCode = flight_code;

	RETURN(num);
END $$

DROP VIEW IF EXISTS destinations $$
CREATE VIEW destinations AS 
SELECT cityName FROM cities
$$

DROP VIEW IF EXISTS planes $$
CREATE VIEW planes AS 
SELECT aircraftID AS ID, aircraftType AS Type FROM aircrafts
$$

DROP VIEW IF EXISTS priceCats $$
CREATE VIEW priceCats AS
SELECT classes.className AS Class, 	prices.amount AS Price FROM classes
JOIN pricecategories ON classes.classID = pricecategories.classID
JOIN prices ON pricecategories.categoryID = prices.priceCategoryID
$$

DROP VIEW IF EXISTS airportList $$
CREATE VIEW airportList AS
SELECT airportName AS Name, IATAcode AS CODE FROM airports
$$

DROP VIEW IF EXISTS plans $$
CREATE VIEW plans AS #velja badi flug daginn og flug timann
SELECT flightschedules.originatingAirport AS Orgin, flightschedules.destinationAirport AS Destination, flights.flightDate AS Day, flights.flightTime AS TimeInAir FROM flights
JOIN flightschedules ON flights.flightNumber = flightschedules.flightNumber
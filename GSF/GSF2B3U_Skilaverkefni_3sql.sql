delimiter $$

DROP PROCEDURE IF EXISTS booking $$
CREATE PROCEDURE booking(cardProvider varchar(35), cardOwner varchar(55), 
	payType bit(1), class int(11), returnFl tinyint(1), pID varchar(35), 
	pName varchar(75), dPriceID int(11), dSeatID int(11), rPriceID int(11), 
	rSeatID int(11), dFnum char(5), dFDate DATE, rFNum char(6), rFDate DATE)
BEGIN
	
	DECLARE dFCode int(11);
	DECLARE rFCode int(11);

	SELECT flightCode INTO dFCode FROM flights WHERE flightDate = dFDate AND flightNumber = dFnum;
	SELECT flightCode INTO rFCode FROM flights WHERE flightDate = rFDate AND flightNumber = rFnum;	 

	INSERT INTO bookings (timeOfBooking, paymentType, cardIssuedBy, cardholdersName, classID, returnFlight) VALUES(NOW(), payType, cardProvider, cardOwner, class, returnFl);
	INSERT INTO debug(infoToKeep) VALUES ("Pass 1");
	INSERT INTO bookedflights (bookingNumber, flightCode, flightOrder) VALUES(LAST_INSERT_ID(), dFcode, 1);
	INSERT INTO debug(infoToKeep) VALUES ("Pass 2");
	INSERT INTO passengers (pID, pName, seatID, bookedFlightID)VALUES(pID, pName, dPriceID, dSeatID, LAST_INSERT_ID());
	INSERT INTO debug(infoToKeep) VALUES ("Pass 3");

	INSERT INTO bookings (timeOfBooking, paymentType, cardIssuedBy, cardholdersName, classID, returnFlight) VALUES(NOW(), payType, cardProvider, cardOwner, class, returnFl);
	INSERT INTO debug(infoToKeep) VALUES ("Pass 4");
	INSERT INTO bookedflights (bookingNumber, flightCode, flightOrder) VALUES(LAST_INSERT_ID(), rFcode, 1);
	INSERT INTO debug(infoToKeep) VALUES ("Pass 5");
	INSERT INTO passengers (pID, pName, seatID, bookedFlightID)VALUES(pID, pName, dPriceID, dSeatID, LAST_INSERT_ID());
	INSERT INTO debug(infoToKeep) VALUES ("Pass 6");
END

/*
"visa", "Martin Tallstrom", 1, 3, 1, "SE19647389", "Martin Tallstrom", 6, 1137, 4, 1203, 
"FA407", "01-12-2016", "FA408", "14-12-2016"

call booking("visa", "Martin Tallstrom", 1, 3, 1, "SE19647389", "Martin Tallstrom", 6, 1137, 4, 1203, 
"FA407", "2016-12-01", "FA408", "2016-12-14")*/
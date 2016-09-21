/*
drop procedure if exists FlightEconomicInfo $$

create procedure FlightEconomicInfo(flight_number char(5),flight_date date)
begin
SELECT (NumberOfPassengers(flight_number,flight_date),SeatUsage(flight_number,flight_date),FlightRevenue(flight_number,flight_date))
end
*/

delimiter $$

--1
DROP FUNCTION IF EXISTS catIncome $$
CREATE FUNCTION catIncome(num int(11))
RETURNS int(11)
BEGIN
RETURN((SELECT COUNT(seatingID) FROM passengers WHERE priceid = num)*(SELECT amount FROM prices WHERE priceID = num));
END $$

DROP FUNCTION IF EXISTS flightIncome $$
CREATE FUNCTION flightIncome(flight_code int(11))
RETURNS int(11)
BEGIN
	--select the booked flight id
	declare bookedFlight_id int(11)
	SELECT bookedFlightID INTO bookedFlight_id FROM bookedflights WHERE flightCode = flight_code

	RETURN(0.93*(93900-(catIncome(1) +catIncome(2) +catIncome(3) +catIncome(4) +catIncome(5) +catIncome(6) +catIncome(7) +catIncome(8) +catIncome(9))))
END $$

-- 2
DROP FUNCTION IF EXISTS pricePerKm $$
CREATE FUNCTION pricePerKm(flight_number char(5))
RETURNS int(11)
BEGIN
	RETURN(flightInome(SELECT flightCode FROM flights WHERE flightNumber = flight_number)/(SELECT distance FROM flightchedules))
END $$

--3
DROP PROCEDURE IF EXISTS windowSeats $$
CREATE PROCEDURE windowSeats(aircraft_id char(6), bookedFlight_id int(11))
begin
SELECT CONCAT(rowNumber, seatNumber) AS Seat FROM aircraftseats 
WHERE aircraftID = aircraft_id 
AND seatPlacement = "w";
AND seatID NOT IN (SELECT seatingID FROM passengers WHERE bookedFlightID = bookedFlight_id)
END $$


--4
--find all the unbooked seats. 
--if a mid seat is booked the row is out
--if two in a row are loose but the seat placement is the same. Those 2 are out

DROP PROCEDURE IF EXISTS NextToEachOther $$
CREATE PROCEDURE NextToEachOther(aircraft_id char(6), bookedFlight_id int(11))
BEGIN
declare takenSeats table (id char);
declate looseSeats table (id char);

INSERT INTO takenSeats VALUES(SELECT seatingID FROM passengers WHERE bookedFlightID = bookedFlight_id) 
INSERT INTO looseSeats VALUES(SELECT seatID, CONCAT(row, seatNumber) FROM aircraftseats 
WHERE seatID NOT IN takenSeat
--dis line bad AND seatPlacement IS NOT "m")
)
--while(go through the previous result)
--if seat x and x+1 are avaliable
	--if the seat placement is the same or the row number isn't
		--raise the x value by one and start over
DROP PROCEDURE IF EXISTS sideBySide $$
CREATE PROCEDURE sideBySide(aircraft_id char(6), bookedFlight_id int(11))
BEGIN

declare firstSeat int;
declare secondSeat int;

declare done int default false;

declare vacantSeatsCursor for
	SELECT seatID FROM aircraftseats WHERE seatID NOT IN (
	SELECT seatingID FROM passengers WHERE bookedFlightID = bookedFlight_id) ORDER BY seatID




create procedure TwoSideBySide(flight_number char(5),flight_date date)
begin
	-- variables holding the seats being investigated
	declare first_seat int;
    declare second_seat int;
    
    -- loop control variable
	declare done int default false;
	-- The cursor itselt if declared containing the query code
	declare vacantSeatsCursor cursor for
		select seatID from AircraftSeats 
		where seatID not in(select AircraftSeats.seatid
							from AircraftSeats
							inner join Passengers on AircraftSeats.seatID = Passengers.seatID
							inner join Bookings on Passengers.bookingNumber = Bookings.bookingNumber
							and Bookings.flightCode = FlightCode(flight_number,flight_date))
		and aircraftID = Carrier(flight_number,flight_date)
        order by seatID;
	-- when the cursor reaches the end of it's data, the done variable is set to true
	declare continue handler for not found set done = true;
    
    set first_seat = null;
    set second_seat = null;
    
	-- The query is executed by opening the cursor. After this statement
    -- the data is in the cursor and accessible.
    open vacantSeatsCursor;
	read_loop: loop
		if first_seat is null then
			fetch vacantSeatsCursor into first_seat;
		elseif second_seat is null then
			fetch vacantSeatsCursor into second_seat;
            -- we've loaded two seatID's that are in the cursor rows.
            -- Furthermore we have these two seatID's in the variables.
            -- Now we need to investigate if they are in fact side by side
            -- on board the airplane.
            -- ============================== -- oo0oo -- ================================
            -- This is probably where your code begins
            -- Remember to set done to true only if you have found two seats side by side
            --  Think about how you will tackle the situation if no seats(side by side) are found
            set done = true;
            -- ============================== -- oo0oo -- ================================
		end if;
        
		-- Check to seethe status og the done variable.
		if done then
		  leave read_loop;
		end if;
	end loop;
	close vacantSeatsCursor;
    
    select first_seat,second_seat;
end $$

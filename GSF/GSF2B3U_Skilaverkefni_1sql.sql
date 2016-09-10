delimiter $$
-- 1: Skrifið function sem telur heildarfjölda sæta í ákveðinni vél.
--    Í reynd er hægt að fara í töfluna Aircrafts og select-a úr dálkinum
--	  maxNumberOfPassangers.
--    Það er hins vegar ekki alveg nægjanlega nákvæmt.  Þessi dálkur geymir 
--    mesta magn af farþegum sem einhver ákveðin vélategund getur tekið en
--	  það er ekkert sem segir að það sé endilega raunverulegur sætafjöldi.
--    Þess vegna er best að hreinlega telja sætin í hverri vél.
--    Þau eru geymd í töflunni AircraftSeats :-)

drop function if exists NumberOfSeats $$
create function NumberOfSeats(aircraft_ID char(6))
returns int(11)
begin
RETURN(SELECT COUNT(seatID) FROM aircraftseats WHERE aircraftID = aircraft_ID);
end $$

-- 2: Skrifið function sem skilar fjölda lausra sæta í ákveðnu flugi.
--    Hérna þarf hreinlega að athuga hve mörg sæti eru ekki "frátekin" í einhverju
--    ákveðnu flugi.  Það þarf að gera í gegnum töfluna Passengers og líklega 
--    einhverjar fleiri.

drop function if exists AvailableSeats $$
create function AvailableSeats(flight_id int(11), aircraft_id char(6))
returns int(6)
begin
RETURN(SELECT maxNumberOfPassangers FROM aircrafts WHERE aircraftID = aircraft_id) -(SELECT COUNT(seatingID) FROM passengers WHERE bookedFlightID = flight_id);
end $$

-- 3: Skrifið function sem skilar fjölda "frátekinna" sæta í ákveðnu flugi.
--    Þetta fall er einhvers konar "andhverfa" fallsins í númer 2.

drop function if exists TakenSeats $$
create function TakenSeats(flight_id int(11))
returns int(6)
begin
RETURN(SELECT COUNT(seatingID) FROM passengers WHERE bookedFlightID = flight_id);
end $$

-- 4: Skrifið function sem skilar einkennisnúmeri þeirrar flugvélar sem flýgur ákveðið flug.
--    Í hefðbundnum flugrekstri myndi þessi function vera feikna mikilvæg.

drop function if exists PlaneNum $$
create function PlaneNum(flight_id int(11))
returns int(6)
begin
RETURN(SELECT aircraftID FROM flights WHERE flightNumber=flight_id);
end $$

-- 5:  Stoppa skráningu á flugdögum(flightDates) í töfluna Flights ef 
--     gefin er dagsetning sem er liðin.  ATH: þetta er trigger.
--     Við gætum ímyndað okkur að starfsmaður FreshAir væri að setja upp flug frá
--	   Keflavík(KEF) til Boston(BOS) og slær óvart inn dagsetninu flugsins á degi
--     sem þegar er liðinn.  Triggerinn á að stoppa þetta.

drop trigger if exists flightDateTrigger $$

create trigger flightDateTrigger
	before insert on flights
	for each row
	begin
		declare msg varchar(255);
		if(flightDate < GETDATE()) then
			set msg = concat('This date has passed', cast(new.flightDate as char));
			-- Villu er kastað og villuskilaboðin birt
            signal sqlstate '45000' set message_text = msg;
		end if;
	end $$

-- 6:  Stoppa bókun ef ef reynt er að bóka ferð sem þegar hefur verið farin. 
--     Hér er svipað uppi á teningnum og í 5.  Aðalmálið er að finna hvaða töflu
--     þarf að setja triggerinn á og fyrir hvaða skipun(insert, update, delete)

drop trigger if exists alreadyDeparted $$

create trigger alreadyDeparted
	before insert on bookedflights
	for each row
	begin
		declare msg varchar (255);
		if(flights.flightDate > GETDATE()) then
			set msg = concat("This flight has left", cast(flights.flightCode as char));
			signal sqlstate '45000' set message_text = msg;
     	end if;
end $$

-- 7:  Stoppa bókun ef vélin er full.  Spurning um að nota lausnina úr númer 2 til
--	   að aðstoða í þessum trigger.
/*
drop trigger if exists thisIsHowToSurviveAFloodNoah $$

create trigger thisIsHowToSurviveAFloodNoah
	before insert on passengers
	for each row
	begin
		declare msg varchar(255);
		if(SELECT maxNumberOfPassangers FROM aircrafts WHERE aircraftID = flights.aircraftID) -(SELECT COUNT(seatingID) FROM passengers WHERE bookedFlightID = new.bookedFlightID == 0) then
		INNER JOIN bookedflights ON passengers.bookedFlightID = bookedflights.bookedflightsID,
		INNER JOIN flighs ON bookedflights.flightCode = flights.flightCode,
		set msg = concat("This flight is full", cast(flights.flightNumber))
	end if;
end $$
*/

-- 8: Logga í töfluna FreshAirLogs þegar insert eða update er keyrt á töfluna FlightSchedule. 
--    Hér þarf að smíða töfluna FreshAirLogs. Ef um er að ræða nýtt flightNumber þá er loggað: 
--    "Ný flugáætlun skráð", dagsetning,flugnúmer 
--    Ef flightNumber er til í töflunni fyrir þá er loggað: 
--    "Flugáætlun uppfærð", dagsetning,flugnúmer

drop trigger if exists NSADreamTable_ins $$

create trigger NSADreamTable_ins  
	after insert on flightschedules
		for each row
		begin
		INSERT INTO FreshAirLogs(type, day, flightNumber)VALUES("insert", NOW(), new.flightNumber);
end $$

drop trigger if exists NSADreamTable_up $$

create trigger NSADreamTable_up
	after insert on flightschedules
		for each row
		begin
		INSERT INTO FreshAirLogs(type, day, flightNumber)VALUES("update", NOW(), new.flightNumber);
end $$

-- ATH: 5 til og með 8 eru triggerar!
-- ATH: Þegar verið er að vinna með ákveðið flug þá er ekki verið að nota flightCode,
--      heldur flightNumber og flightDate.
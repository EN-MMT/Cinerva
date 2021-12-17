--EX 1
UPDATE Properties SET Zipcode = CONVERT(varchar(10), NEWID()) WHERE Zipcode is null

--Ex 2
SELECT p.Name,Description,Adress 
FROM Properties as "p" 
INNER JOIN Cities as "c"
on p.CityId = c.Id 
WHERE c.Name = 'Vaslui'

--Ex 3
SELECT c.FirstName, c.LastName, c.Email 
FROM Users as "c" 
INNER JOIN Reservations as "r"
on c.Id = r.UserId
WHERE r.PayedStatus = 1

--Ex 4
SELECT c.FirstName, c.LastName 
FROM Users as "c"
INNER JOIN Roles as "r"
on c.RoleId = r.Id
INNER JOIN Properties as "p"
on p.AdministratorId = c.Id
INNER JOIN Cities
on Cities.Id = p.CityId
WHERE Cities.Name = 'Vaslui'
ORDER BY c.FirstName, c.LastName

--Ex 5
SELECT rc.Name, r.PriceNight FROM Rooms as "r" 
INNER JOIN RoomCategories as "rc" 
on r.RoomCategory = rc.Id
INNER JOIN Properties as "p"
on r.PropertyId = p.Id
WHERE p.Name = 'Fisher Inc'


--Ex 6
SELECT TOP 5 p.Name, 
COUNT(*) as 'Reservations'
FROM Properties as "p"
INNER JOIN Rooms as "r"
on p.Id = r.PropertyId
INNER JOIN RoomReservations as "rr"
on rr.RoomId = r.Id
GROUP BY p.Name
ORDER BY COUNT(*) DESC

--Ex 7
SELECT p.Name 
FROM Properties as "p"
INNER JOIN Rooms as "r"
on r.PropertyId = p.Id
INNER JOIN RoomReservations as "rr"
on rr.Id = r.Id
LEFT OUTER JOIN Reservations as "res"
on res.Id = rr.Id
WHERE GETDATE()> res.CheckInDate
OR GETDATE()<res.CheckOutDate


--Ex 8
update properties set TotalRooms =
(SELECT Count(*) from  rooms as "r" where
r.PropertyId = properties.Id 
) 

SELECT p.name, p.TotalRooms- COUNT(*) as "Rooms"
FROM Properties as "p"
INNER JOIN Rooms as "r" 
on r.PropertyId = p.Id
INNER JOIN RoomCategories as "rc" 
on r.RoomCategory = rc.Id
LEFT JOIN RoomReservations as "rr" 
on r.Id = rr.RoomId
LEFT JOIN Reservations as "res" 
on res.Id = rr.ReservationId
WHERE (res.CheckInDate >= '2021-12-01' and res.CheckInDate < '2022-01-01')
and r.PriceNight between 99 and 751
group by p.name, p.TotalRooms
HAVING p.TotalRooms - COUNT(*) > 0

--Ex 9
SELECT TOP(1) p.name, p.rating 
FROM Properties as "p"
INNER JOIN Rooms as "r" 
on r.PropertyId = p.Id
LEFT JOIN RoomReservations as "rr"
on r.Id = rr.RoomId
LEFT JOIN Reservations as "res"
on res.Id = rr.ReservationId
WHERE NOT ((res.CheckInDate >= '2021-12-04' and res.CheckInDate <='2021-12-06')
or (res.CheckOutDate >= '2021-12-04' and res.CheckOutDate <='2021-12-06')
or (res.CheckInDate <= '2021-12-04' and res.CheckOutDate >='2021-12-06'))
GROUP BY p.name, p.rating

--Ex 11
SELECT p.name, p.rating 
FROM Properties as "p"
INNER JOIN Rooms as "r" 
on r.PropertyId = p.Id
INNER JOIN RoomCategories as "rc"
on rc.Id = r.RoomCategory
LEFT JOIN RoomReservations as "rr"
on r.Id = rr.RoomId
LEFT JOIN Reservations as "res"
on res.Id = rr.ReservationId
WHERE NOT ((res.CheckInDate >= '2021-12-06' and res.CheckInDate <='2021-12-12')
or (res.CheckOutDate >= '2021-12-06' and res.CheckOutDate <='2021-12-12')
or (res.CheckInDate <= '2021-12-06' and res.CheckOutDate >='2021-12-12'))
or res.CheckInDate is null
and rc.BedsCount =2 
and r.PriceNight between 150 and 400

--Ex 12
SELECT p.Name, COUNT(*) 
FROM Users as "u"
INNER JOIN Reservations as "res"
on u.Id = res.UserId
INNER JOIN RoomReservations as "rres"
on rres.ReservationId = res.Id
INNER JOIN Rooms as "r"
on r.Id = rres.RoomId
INNER JOIN Properties as "p"
on p.Id = r.PropertyId
INNER JOIN Cities as "c"
on c.Id = p.CityId
WHERE c.Name='Vaslui'
and (res.CheckInDate between '2021-05-01' and '2021-05-31')
GROUP BY p.Name

--Ex 13
SELECT DISTINCT p.name, p.Description, p.Adress
FROM Properties as "p"
INNER JOIN Rooms as "r" 
on r.PropertyId = p.Id
INNER JOIN RoomCategories as "rc"
on rc.Id = r.RoomCategory
LEFT JOIN RoomReservations as "rr"
on r.Id = rr.RoomId
LEFT JOIN Reservations as "res"
on res.Id = rr.ReservationId
WHERE NOT ((res.CheckInDate >= '2022-05-01' and res.CheckInDate <='2022-08-30')
or (res.CheckOutDate >= '2022-05-01' and res.CheckOutDate <='2022-08-30')
or (res.CheckInDate <= '2022-05-01' and res.CheckOutDate >='2022-08-30'))
or res.CheckInDate is null

--Ex 14
SELECT top 10 p.Name, sum(r.PriceNight) 
FROM Users as "u"
INNER JOIN Reservations as "res"
on u.Id = res.UserId
INNER JOIN RoomReservations as "rres"
on rres.ReservationId = res.Id
INNER JOIN Rooms as "r"
on r.Id = rres.RoomId
INNER JOIN Properties as "p"
on p.Id = r.PropertyId
INNER JOIN Cities as "c"
on c.Id = p.CityId
INNER JOIN Countries as "CO"
on CO.Id = c.CountryId
WHERE CO.Name='Romania'
and (res.CheckInDate between '2020-01-01' and '2021-01-01')
GROUP BY p.Name
ORDER BY sum(r.PriceNight) DESC

--Ex 15
UPDATE Properties set Rating = id%5+Rand()
SELECT RATING from Properties

SELECT DISTINCT p.Name, p.Rating
FROM Users as "u"
LEFT JOIN Reservations as "res"
on u.Id = res.UserId
LEFT JOIN RoomReservations as "rres"
on rres.ReservationId = res.Id
INNER JOIN Rooms as "r"
on r.Id = rres.RoomId
INNER JOIN Properties as "p"
on p.Id = r.PropertyId
INNER JOIN Cities as "c"
on c.Id = p.CityId
INNER JOIN Countries as "CO"
on CO.Id = c.CountryId
WHERE CO.Name='Romania'
and (res.CheckInDate between '2022-08-01' and '2022-08-31')
and p.Rating>=3

--Ex 18
SELECT DISTINCT p.name, sum(r.PriceNight) as 'loss'
FROM Properties as "p"
INNER JOIN Rooms as "r" 
on r.PropertyId = p.Id
INNER JOIN RoomCategories as "rc"
on rc.Id = r.RoomCategory
LEFT JOIN RoomReservations as "rr"
on r.Id = rr.RoomId
LEFT JOIN Reservations as "res"
on res.Id = rr.ReservationId
WHERE (NOT (res.CheckInDate >= GETDATE() 
or (res.CheckOutDate >= GETDATE())
or (res.CheckInDate < GETDATE() and res.CheckOutDate >GETDATE()))
or res.CheckInDate is null)
and p.Name = 'Daniel LLC'
GROUP BY p.Name
CREATE TABLE Countries(
	Id int identity(1,1),
	Name nvarchar(100) not null,
	PRIMARY KEY (Id),
);
CREATE TABLE Cities(
	Id int identity(1,1),
	Name nvarchar(100) not null,
	CountryId int,
	PRIMARY KEY(Id),
	FOREIGN KEY(CountryId) REFERENCES Countries(Id)
);
CREATE TABLE GeneralFeatures(
	Id int identity(1,1),
	Name nvarchar(200) not null,
	IconUrl nvarchar(500) not null,
	PRIMARY KEY (Id)
);
CREATE TABLE PropertyTypes(
	Id int identity(1,1),
	Type nvarchar(50) not null,
	PRIMARY KEY (Id)
);
CREATE TABLE Roles (
    Id int identity(1,1),
	Name nvarchar(200) not null,
	PRIMARY KEY (Id)
);
CREATE TABLE Users (
    Id int identity(1,1),
	FirstName nvarchar(200) not null,
    LastName nvarchar(200) not null,
    RoleId int not null,
    Email nvarchar(100) not null,
	Password char(100) not null,
	isDeleted bit,
	isBanned bit,
	PRIMARY KEY (Id),
	FOREIGN KEY (RoleID) REFERENCES ROLES(Id)
);
CREATE TABLE Properties(
	Id int identity(1,1),
	Name nvarchar(250) not null,
	Rating decimal(2,1) not null,
	Description nvarchar(MAX) not null,
	Adress nvarchar(200) not null,
	CityId int,
	AdministratorId int,
	Phone char(14) not null,
	TotalRooms int,
	NumberOfDayForRefunds int not null,
	PropertyTypeID int,
	PRIMARY KEY (Id),
	FOREIGN KEY (CityId) REFERENCES Cities(Id),
	FOREIGN KEY (AdminisNtratorId) REFERENCES Users(Id),
	FOREIGN KEY (PropertyTypeId) REFERENCES PropertyTypes(Id)
);
CREATE TABLE PropertyImages(
	Id int identity(1,1),
	ImageUrl nvarchar(500) not null,
	PropertyId int,
	FOREIGN KEY (PropertyId) REFERENCES PropertyTypes(Id),
	PRIMARY KEY(Id)
);
CREATE TABLE Reviews (
	UserId int identity(1,1),
	PropertyId int not null,
	Description nvarchar(MAX),
	Rating int,
	ReviewDate date not null,
	FOREIGN KEY (UserId) REFERENCES Users(Id),
	FOREIGN KEY (PropertyId) REFERENCES Properties(Id)
);
CREATE TABLE PropertyFacilities(
	PropertyId int,
	GeneralFeatureId int,
	FOREIGN KEY (PropertyId) REFERENCES Properties(Id),
	FOREIGN KEY (GeneralFeatureId) REFERENCES GeneralFeatures(Id)
);
CREATE TABLE RoomFeatures(
	Id int identity(1,1),
	Name nvarchar(500) not null,
	IconUrl nvarchar(500),
	PRIMARY KEY(Id)
);
CREATE TABLE RoomCategories(
	Id int,
	Name varchar(100) not null,
	BedsCount int,
	Description nvarchar(500),
	PriceNight decimal(20,2) not null,
	PRIMARY KEY(Id)
);
CREATE TABLE Rooms(
	Id int identity(1,1),
	RoomCategory int,
	PropertyId int,
	PRIMARY KEY(Id),
	FOREIGN KEY (RoomCategory) REFERENCES RoomCategories(Id),
	FOREIGN KEY (PropertyId) REFERENCES Properties(Id)
);
CREATE TABLE Reservations (
    Id int identity(1,1),
	UserId int,
	FOREIGN KEY (UserId) REFERENCES Users (Id),
	PRIMARY KEY (Id),
	CheckInDate date not null,
	CheckOutDate date not null,
	PayedStatus bit,
	PaymentMethod varchar(20) not null,
	cancelDate date
);
CREATE TABLE RoomReservations(
	Id int identity(1,1),
	RoomId int,
	ReservationId int,
	PRIMARY KEY (Id),
	FOREIGN KEY (RoomId) REFERENCES Rooms (Id),
	FOREIGN KEY (ReservationId) REFERENCES Reservations(Id)
);
CREATE TABLE RoomFacilities(
	RoomId int,
	RoomFeatureId int,
	FOREIGN KEY (RoomId) REFERENCES Rooms (Id),
	FOREIGN KEY (RoomFeatureId) REFERENCES RoomFeatures(Id)
);
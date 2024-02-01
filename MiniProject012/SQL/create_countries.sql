use MiniProjects
go
create table MiniProject012_Countries(
	Id int identity(1,1) primary key,
	CountryName varchar(100),
	Area decimal(30,0),
	[Population] decimal(30,0)
)
go
insert into MiniProject012_Countries(CountryName, Area, [Population]) values
('Russia', 17098246, 146424729),
('Canada', 9984670, 40528396),
('China', 9596960, 1409670000),
('United States', 9369417, 335893238),
('Brazil', 8510346, 203080756),
('Australia', 7741220, 26461912),
('India', 3287263, 1392329000)

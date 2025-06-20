CREATE DATABASE [OfficeResourceBookingSystemDb]

GO
USE [OfficeResourceBookingSystemDb]
GO

CREATE TABLE [Employees]
(
[EmployeeId] INT PRIMARY KEY IDENTITY,
[FullName] VARCHAR(100) NOT NULL,
[EmailAddress] VARCHAR(100) NOT NULL UNIQUE,
[Password] VARCHAR(256) NOT NULL
)

CREATE TABLE [Resources]
(
[ResourceId] INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100) NOT NULL,
[Type] VARCHAR(50) NOT NULL,
[Description] VARCHAR(200),
[IsAvailable] BIT NOT NULL
)

CREATE TABLE [Reservations]
(
[ReservationId] INT PRIMARY KEY IDENTITY,
[EmployeeId] INT FOREIGN KEY REFERENCES [Employees]([EmployeeId]) NOT NULL,
[ResourceId] INT FOREIGN KEY REFERENCES [Resources]([ResourceId]) NOT NULL,
[Purpose] VARCHAR(150) NOT NULL,
[ParticipantsCount] INT NOT NULL,
[StartTime] DATETIME NOT NULL,
[EndTime] DATETIME NOT NULL,
[IsActive] BIT NOT NULL,
[CreatedAt] DATETIME NOT NULL
)

INSERT INTO [Employees] ([FullName], [EmailAddress], [Password]) VALUES
('Petar Nikolov', 'petar.nikolov@firma.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Ivana Koleva', 'ivana.koleva@firma.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Nikolay Stanchev', 'nikolay.stanchev@firma.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Silvia Marinova', 'silvia.marinova@firma.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Radoslav Iliev', 'radoslav.iliev@firma.bg', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')


INSERT INTO [Resources] ([Name], [Type], [Description], [IsAvailable]) VALUES
('Zasedatelna Staya 1', 'MeetingRoom', 'Malka staya za 6 hora s ekran i whiteboard', 1),
('Zasedatelna Staya 2', 'MeetingRoom', 'Golyama staya za 12 hora s videokonferentno oborudvane', 1),
('Proektor Epson X123', 'Projector', 'HD proektor za prezentatsii', 1),
('Laptop Lenovo ThinkPad', 'Laptop', 'Laptop za sluzhebno polzvane s Windows 11', 0),
('Tablet Samsung Tab S6', 'Tablet', 'Tablet za mobilni prezenatsii i podpisvane', 1),
('Printer HP LaserJet', 'Printer', 'Cherno-byal lazeren printer', 0)

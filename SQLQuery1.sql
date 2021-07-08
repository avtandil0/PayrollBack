CREATE TABLE Department (
	Id uniqueidentifier PRIMARY KEY ,
    Name nvarchar(255) not null,
    DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

CREATE TABLE Coefficient (
	Id uniqueidentifier PRIMARY KEY ,
    Gross float not null,
    Net float not null,
    Paid float not null,
    IncomeTax float not null,
    Pension float not null,
    Tax1 float not null,
    Tax2 float not null,
    DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);



CREATE TABLE CoefficientGroup (
	Id uniqueidentifier PRIMARY KEY ,
	Name nvarchar(255) not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
	PensionCoefficientId uniqueidentifier FOREIGN KEY  REFERENCES Coefficient(ID),
	StandartCoefficientId uniqueidentifier FOREIGN KEY  REFERENCES Coefficient(ID)

);


CREATE TABLE Project (
	Id uniqueidentifier PRIMARY KEY ,
	Description nvarchar(255) not null,
	Code nvarchar(255) not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

CREATE TABLE CostCenter (
	Id uniqueidentifier PRIMARY KEY ,
	Description nvarchar(255) not null,
	Code nvarchar(255) not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

CREATE TABLE AccountsReportChartType (
	Id uniqueidentifier PRIMARY KEY ,
	Description nvarchar(255) not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

CREATE TABLE AccountsReportChart (
	Id uniqueidentifier PRIMARY KEY ,
	Description nvarchar(255) not null,
	AccountsReportChartTypeId uniqueidentifier FOREIGN KEY  REFERENCES AccountsReportChartType(ID),
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

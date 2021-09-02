CREATE TABLE Department (
	Id uniqueidentifier PRIMARY KEY ,
    Name nvarchar(255) not null,
    DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

CREATE TABLE Coefficient (
	Id uniqueidentifier PRIMARY KEY ,
	Name nvarchar(255) not null,
	Description nvarchar(255) not null,
	--Standart
    SGross float not null,
    SNet float not null,
    SPaid float not null,
    SIncomeTax float not null,
    SPension float not null,
    STax1 float not null,
    STax2 float not null,
	--Pension
	PGross float not null,
    PNet float not null,
    PPaid float not null,
    PIncomeTax float not null,
    PPension float not null,
    PTax1 float not null,
    PTax2 float not null,

    DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);



--CREATE TABLE CoefficientGroup (
--	Id uniqueidentifier PRIMARY KEY ,
--	Name nvarchar(255) not null,
--	DateCreated datetime not null,
--    DateChange datetime,
--    DateDeleted datetime,
--	PensionCoefficientId uniqueidentifier FOREIGN KEY  REFERENCES Coefficient(ID),
--	StandartCoefficientId uniqueidentifier FOREIGN KEY  REFERENCES Coefficient(ID)

--);


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
	Name nvarchar(255) not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

CREATE TABLE AccountsReportChart (
	Id uniqueidentifier PRIMARY KEY ,
	Description nvarchar(255) not null,
	Code nvarchar(255) not null,
	AccountsReportChartTypeId uniqueidentifier FOREIGN KEY  REFERENCES AccountsReportChartType(ID),
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

CREATE TABLE Component (
	Id uniqueidentifier PRIMARY KEY ,
	Name nvarchar(255) not null,
	CreditAccountId uniqueidentifier FOREIGN KEY  REFERENCES AccountsReportChart(ID),
	DebitAccountId uniqueidentifier FOREIGN KEY  REFERENCES AccountsReportChart(ID),
	CoefficientId uniqueidentifier FOREIGN KEY  REFERENCES AccountsReportChart(ID),
	StartDate datetime not null,
	EndDate datetime not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);



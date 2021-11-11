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
	CoefficientId uniqueidentifier FOREIGN KEY  REFERENCES Coefficient(ID),
	StartDate datetime not null,
	EndDate datetime not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);



CREATE TABLE SchemeType (
	Id int PRIMARY KEY ,
	Name nvarchar(255) not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);

CREATE TABLE PaymentDaysType (
	Id int PRIMARY KEY ,
	Name nvarchar(255) not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);


CREATE TABLE [dbo].[Employee](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[MobilePhone] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[PersonalNumber] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[BankAccountNumber] [nvarchar](255) NOT NULL,
	[SchemeTypeId] [int] NOT NULL,
	[DepartmentId] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateChange] [datetime] NULL,
	[DateDeleted] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([SchemeTypeId])
REFERENCES [dbo].[SchemeType] ([Id])
GO

CREATE TABLE [dbo].[EmployeeComponents](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NULL,
	[ComponentId] [uniqueidentifier] NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[CostCenterId] [uniqueidentifier] NULL,
	[PaymentDaysTypeId] [int] not NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[SchemeTypeId] [int] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Currency] [nvarchar](255) NOT NULL,
	[PaidByCash] [bit] NOT NULL,
	[CashAmount] [decimal](18, 0) NOT NULL,
	[PaidMultiple] [bit] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateChange] [datetime] NULL,
	[DateDeleted] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EmployeeComponents]  WITH CHECK ADD FOREIGN KEY([ComponentId])
REFERENCES [dbo].[Component] ([Id])
GO

ALTER TABLE [dbo].[EmployeeComponents]  WITH CHECK ADD FOREIGN KEY([CostCenterId])
REFERENCES [dbo].[CostCenter] ([Id])
GO

ALTER TABLE [dbo].[EmployeeComponents]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[EmployeeComponents]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO

ALTER TABLE [dbo].[EmployeeComponents]  WITH CHECK ADD FOREIGN KEY([SchemeTypeId])
REFERENCES [dbo].[SchemeType] ([Id])
GO

ALTER TABLE [dbo].[EmployeeComponents]  WITH CHECK ADD FOREIGN KEY([PaymentDaysTypeId])
REFERENCES [dbo].[PaymentDaysType] ([Id])
GO




ALTER TABLE employee
ADD ResId int;

ALTER TABLE employee
ADD Position nvarchar(100);

ALTER TABLE employee
ADD land_iso nvarchar(10);

CREATE TABLE [dbo].[EmployeeType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateChange] [datetime] NULL,
	[DateDeleted] [datetime] NULL,
 CONSTRAINT [PK_EmployeeType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



ALTER TABLE employee
ADD EmployeeTypeId int FOREIGN KEY  REFERENCES employeeType(ID)


CREATE TABLE EmployeeGraceType (
	Id int PRIMARY KEY ,
	Name nvarchar(255) not null,
	DateCreated datetime not null,
    DateChange datetime,
    DateDeleted datetime,
);


ALTER TABLE employee
ADD EmployeeGraceTypeId int FOREIGN KEY  REFERENCES EmployeeGraceType(ID)

ALTER TABLE EmployeeGraceType
ADD Amount [decimal](18, 0) 

ALTER TABLE employee
ADD GraceAmount [decimal](18, 0) 

ALTER TABLE employee
ADD RemainingGraceAmount [decimal](18, 0) 

ALTER TABLE employee
ADD land_iso nvarchar(10);


CREATE TABLE Calculation(
	Id uniqueidentifier PRIMARY KEY ,
	EmployeeId uniqueidentifier FOREIGN KEY  REFERENCES Employee(ID),
	EmployeeComponentId uniqueidentifier FOREIGN KEY  REFERENCES EmployeeComponents(ID),
	[Res_id] [int] ,
	[PayrollYear] [int] ,
	[PayrollMonth] [int],
	[CalculationDate] [datetime] ,
	SchemeTypeId int FOREIGN KEY  REFERENCES SchemeType(ID),
	Gross [decimal](18, 0) NOT NULL,
	Net [decimal](18, 0) NOT NULL,
	Paid [float] NOT NULL,
	IncomeTax [decimal](18, 0) NOT NULL,
	PensionTax [float] NOT NULL,
	Tax1 [float] NOT NULL,
	Tax2 [float] NOT NULL,
)



select * from SchemeType



select GETDATE()

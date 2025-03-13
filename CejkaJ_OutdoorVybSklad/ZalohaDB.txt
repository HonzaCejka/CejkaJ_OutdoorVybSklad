CREATE TABLE [dbo].[Zbozi] (
    [IdZbo]   INT          IDENTITY (1, 1) NOT NULL,
    [Nazev]   VARCHAR (50) NOT NULL,
	[Popis] VARCHAR(200) NULL,
    [PocetKsSklad] INT          NOT NULL,
    [CenaKs]  INT          NOT NULL,     
    PRIMARY KEY CLUSTERED ([IdZbo] ASC)
);
CREATE TABLE [dbo].[ZboNM] (
    [IdZboNM] INT NOT NULL IDENTITY,
    [IdZbo]   INT NOT NULL,
    [IdProd]  INT NOT NULL,
    [Pocet] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([IdZboNM] ASC), 
    CONSTRAINT [FK_Zbo] FOREIGN KEY (IdZbo) REFERENCES dbo.Zbozi(IdZbo),
	CONSTRAINT [FK_Prod] FOREIGN KEY (IdProd) REFERENCES dbo.Prodejka(IdProd)
);

CREATE TABLE [dbo].[Zak] (
    [IdZak]    INT          NOT NULL,
    [Jmeno]    VARCHAR (50) NOT NULL,
    [Prijmeni] VARCHAR (50) NOT NULL,
    [Adresa]   VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdZak] ASC)
);

CREATE TABLE [dbo].[Prodejka] (
    [IdProd]    INT  IDENTITY (1, 1) NOT NULL,
    [IdZak]     INT  NOT NULL,
    [Cena]      INT  NOT NULL,
    [DatumVytv] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProd] ASC),
    CONSTRAINT [FK_ProdToZak] FOREIGN KEY ([IdZak]) REFERENCES [dbo].[Zak] ([IdZak])
);


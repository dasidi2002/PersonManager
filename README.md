- rode o comando docker compose up
- irá criar ima imagem do sql server 
- conecte no banco com as credenciais do docker 
- na master crie a tabela
- CREATE TABLE Persons (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [DocumentNumber] NVARCHAR(MAX) NOT NULL,
    [PersonType] NVARCHAR(10) NOT NULL, -- Discriminador para o tipo de herança
    [CreatedAt] DATETIME2 NOT NULL,
    [UpdatedAt] DATETIME2 NULL,
    
    -- Address columns (owned entity)
    [Address_Street] NVARCHAR(MAX) NULL,
    [Address_Number] NVARCHAR(MAX) NULL,
    [Address_Complement] NVARCHAR(MAX) NULL,
    [Address_Neighborhood] NVARCHAR(MAX) NULL,
    [Address_City] NVARCHAR(MAX) NULL,
    [Address_State] NVARCHAR(MAX) NULL,
    [Address_ZipCode] NVARCHAR(MAX) NULL,
    
    -- NaturalPerson specific
    [BirthDate] DATETIME2 NULL,
    
    -- LegalPerson specific
    [CompanyName] NVARCHAR(200) NULL
)


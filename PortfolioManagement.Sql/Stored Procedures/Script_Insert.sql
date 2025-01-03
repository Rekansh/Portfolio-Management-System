CREATE PROCEDURE [dbo].[Script_Insert]
    @Name VARCHAR(300), 
    @BseCode NUMERIC(6,0), 
    @NseCode VARCHAR(30), 
    @ISINCode VARCHAR(30), 
    @MoneyControlURL VARCHAR(MAX), 
    @FetchURL VARCHAR(MAX), 
    @IsFetch BIT, 
    @IsActive BIT,
    @IndustryName VARCHAR(MAX) = NULL, 
    @FaceValue INT = NULL, 
    @Group VARCHAR(10) = NULL
AS
/***********************************************************************************************
	 NAME     :  Script_Insert
	 PURPOSE  :  This SP insert record in table Script.
	 REVISIONS:
	 Ver        Date				        Author              Description
	 ---------  ----------					---------------		-------------------------------
	 1.0        03/09/2024					Rekansh Patel             1. Initial Version.	 
************************************************************************************************/
BEGIN
    DECLARE @Id SMALLINT;
    
    IF NOT EXISTS(SELECT [Id] FROM [Script] WHERE [Name] = @Name AND [BseCode] = @BseCode AND [NseCode] = @NseCode AND [ISINCode] = @ISINCode)
    BEGIN
        INSERT INTO [Script] 
            ([Name], [BseCode], [NseCode], [ISINCode], [MoneyControlURL], [FetchURL], [IsFetch], [IsActive], [IndustryName], [FaceValue], [Group]) 
        VALUES 
            (@Name, @BseCode, @NseCode, @ISINCode, @MoneyControlURL, @FetchURL, @IsFetch, @IsActive, @IndustryName, @FaceValue, @Group);
        
        SET @Id = SCOPE_IDENTITY();
    END
    ELSE
        SET @Id = 0;
    
       SELECT @Id;
END

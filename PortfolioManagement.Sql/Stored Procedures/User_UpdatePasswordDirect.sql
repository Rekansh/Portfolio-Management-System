﻿/*
This SP update password in table User
Created By :: Rekansh Patel
Created On :: 08/05/2018
*/	
CREATE PROCEDURE [dbo].[User_UpdatePasswordDirect]
	@Id bigint,
	@Password varchar(max), 
	@PasswordSalt varchar(max),
	@LastUpdateDateTime DATETIME
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.[User] U WHERE U.Id = @Id)
	BEGIN
		UPDATE [User] 
		SET [Password] = @Password, 
			[PasswordSalt] = @PasswordSalt,
			[LastUpdateDateTime] = @LastUpdateDateTime,
			IsActive = 1
		WHERE [Id] = @Id

		SELECT CONVERT(BIT, 1)
	END
	ELSE
		SELECT CONVERT(BIT, 0)
END





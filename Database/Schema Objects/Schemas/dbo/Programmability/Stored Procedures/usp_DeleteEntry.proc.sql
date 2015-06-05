CREATE PROCEDURE [dbo].[usp_DeleteEntry]
	@EntryID INT
AS
	BEGIN TRANSACTION

		DECLARE @EntryNumber INT


		SELECT @EntryNumber = EntryNumber
		FROM Entries
		WHERE EntryID = @EntryID


		DELETE FROM HandPickedMatches 
		WHERE Rooster1EntryNumber = @EntryNumber
		   OR Rooster2EntryNumber = @EntryNumber
		

		DELETE FROM NoFights
		WHERE EntryID1 = @EntryID
		   OR EntryID2 = @EntryID


		DELETE FROM FightSequences
		WHERE Rooster1EntryNumber = @EntryNumber
		   OR Rooster2EntryNumber = @EntryNumber


		DELETE FROM Roosters 
		WHERE EntryID = @EntryID
		

		DELETE FROM Entries 
		WHERE EntryID = @EntryID

	IF @@ROWCOUNT > 0
		COMMIT TRANSACTION
	ELSE
		ROLLBACK TRANSACTION

RETURN
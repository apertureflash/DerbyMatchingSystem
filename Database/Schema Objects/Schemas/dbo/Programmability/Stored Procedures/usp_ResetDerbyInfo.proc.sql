CREATE PROCEDURE [dbo].[usp_ResetDerbyInfo]

AS

	TRUNCATE TABLE FightSequences

	TRUNCATE TABLE HandPickedMatches

	TRUNCATE TABLE NoFights

	DELETE FROM Roosters

	DELETE FROM Entries

	DELETE FROM Handlers

	UPDATE Derbies
		SET DerbyName = '',
			DerbyDate = NULL,
			NumberOfEntries = 0
	WHERE DerbyID = 1
RETURN 0
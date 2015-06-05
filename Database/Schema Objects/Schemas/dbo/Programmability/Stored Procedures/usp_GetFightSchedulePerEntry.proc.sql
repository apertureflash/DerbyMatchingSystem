CREATE PROCEDURE [dbo].[usp_GetFightSchedulePerEntry]
AS
BEGIN
	SELECT FightNumber, Rooster1EntryNumber, Rooster1Entry, Rooster1LB, CAST(Rooster1Weight AS INT) 'Rooster1Weight', 
						Rooster2EntryNumber, Rooster2Entry, Rooster2LB, CAST(Rooster2Weight AS INT) 'Rooster2Weight'
	FROM FightSequences

	UNION

	SELECT FightNumber, Rooster2EntryNumber, Rooster2Entry, Rooster2LB, CAST(Rooster2Weight AS INT),
						Rooster1EntryNumber, Rooster1Entry, Rooster1LB, CAST(Rooster1Weight AS INT)
	FROM FightSequences
	ORDER BY Rooster1EntryNumber, Rooster1Weight
END
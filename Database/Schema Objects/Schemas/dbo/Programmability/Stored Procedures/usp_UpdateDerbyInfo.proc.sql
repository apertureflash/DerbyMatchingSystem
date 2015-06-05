CREATE PROCEDURE [dbo].[usp_UpdateDerbyInfo]
	@DerbyID INT, 
	@LocationID INT = NUll,
	@DerbyName NVARCHAR(200),
	@DerbyDate DATETIME,
	@NumberOfEntries INT,
	@UpdatedBy NVARCHAR(50)
AS
BEGIN

	SET NOCOUNT ON

	UPDATE Derbies
	SET LocationID = @LocationID,
		DerbyName = @DerbyName,
		DerbyDate = @DerbyDate,
		NumberOfEntries = @NumberOfEntries,
		LastUpdated = GETDATE(),
		UpdatedBy = @UpdatedBy
	WHERE DerbyID = @DerbyID

END
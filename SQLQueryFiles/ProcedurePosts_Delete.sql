USE DotNetCourseDatabase
GO

CREATE OR ALTER PROCEDURE TutorialAppSchema.spPosts_Delete
/*EXEC TutorialAppSchema.spPosts_Delete @PostId = 3*/
    @PostId INT,
    @UserId INT
AS
BEGIN
    DELETE FROM TutorialAppSchema.Posts 
        WHERE PostId = @PostId 
            AND UserID = @UserId
END
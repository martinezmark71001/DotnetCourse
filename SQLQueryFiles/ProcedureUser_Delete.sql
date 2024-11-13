USE DotNetCourseDatabase
GO

CREATE OR ALTER PROCEDURE TutorialAppSchema.spUser_Del
    /*EXEC TutorialAppSchema.spUser_Del */
    @UserId INT
AS 
BEGIN
    DELETE FROM TutorialAppSchema.Users 
        WHERE UserId = @UserId
    DELETE FROM TutorialAppSchema.UserJobInfo 
        WHERE UserId = @UserId
    DELETE FROM TutorialAppSchema.UserSalary 
        WHERE UserId = @UserId
END
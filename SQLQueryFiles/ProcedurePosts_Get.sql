USE DotNetCourseDatabase
GO

CREATE OR ALTER PROCEDURE TutorialAppSchema.spPosts_Get
/* EXEC TutorialAppSchema.spPosts_Get @UserId = 1003, @SearchValue = 'Working' */
/* EXEC TutorialAppSchema.spPosts_Get @PostId = 3 */
    @UserId INT = NULL,
    @SearchValue NVARCHAR(MAX) = NULL,
    @PostId INT = NULL
AS
BEGIN
    SELECT [PostId],
        [UserId],
        [PostTitle],
        [PostContent],
        [PostCreated],
        [PostUpdated]
    FROM TutorialAppSchema.Posts AS Posts
        WHERE UserId = ISNULL(@UserId, Posts.UserId)
            AND Posts.PostId = ISNULL(@PostId, Posts.PostId) 
            AND (@SearchValue IS NULL 
                OR Posts.PostTitle LIKE '%' + @SearchValue + '%'
                OR Posts.PostContent LIKE '%' + @SearchValue + '%')
END
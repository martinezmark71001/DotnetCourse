USE DotNetCourseDatabase
GO

CREATE TABLE TutorialAppSchema.Posts (
    PostId INT IDENTITY (1, 1),
    UserId INT,
    PostTitle NVARCHAR(255),
    PostContent NVARCHAR(MAX),
    PostCreated DATETIME,
    PostUpdated DATETIME
)

CREATE CLUSTERED INDEX cix_Posts_UserId_PostId ON TutorialAppSchema.Posts(UserId, PostId)

SELECT [PostId],
[UserId],
[PostTitle],
[PostContent],
[PostCreated],
[PostUpdated] FROM TutorialAppSchema.Posts

INSERT INTO TutorialAppSchema.Posts([PostId],
[UserId],
[PostTitle],
[PostContent],
[PostCreated],
[PostUpdated])VALUES()

SELECT GETDATE()

UPDATE TutorialAppSchema.Posts 
    SET UserId = 1003
    WHERE PostId = 2

SELECT * FROM TutorialAppSchema.Posts
    WHERE PostTitle LIKE '%search%'
        OR PostContent LIKE '%search%'
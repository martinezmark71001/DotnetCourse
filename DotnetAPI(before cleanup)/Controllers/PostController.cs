using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class PostController : Controller
    {
        private readonly DataContextDapper _dapper;
        public PostController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        [HttpGet("Posts/{postId}/{userId}/{searchParam}")]
        public IEnumerable<Post> GetPosts(int postId, int userId, string searchParam = "None")
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get ";
            string parameters = "";
            if(postId != 0)
            {
                parameters += ", @PostId= "+ postId.ToString(); 
            }
            if(userId != 0)
            {
                parameters += ", @UserId= "+ userId.ToString(); 
            }
            if(searchParam.ToLower() != "none")
            {
                parameters += ", @SearchValue='"+ searchParam + "'"; 
            }
            if(parameters.Length > 0)
            {
                sql += parameters.Substring(1);
            }
            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get @UserId = " + 
                this.User.FindFirst("userId")?.Value;

                    // DotnetAPI.Models.User

            return _dapper.LoadData<Post>(sql);
        }
        [HttpGet("PostsBySearch/{searchParam}")]
        public IEnumerable<Post> PostsBySearch(string searchParam)
        {
            string sql = @"
            SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE PostTitle LIKE '%" + searchParam + "%'" +
                        "OR PostContent LIKE '%" + searchParam +"%'";

                    // DotnetAPI.Models.User

            return _dapper.LoadData<Post>(sql);
        }

        [HttpPut("UpsertPost")]
        public IActionResult UpsertPost(Post postToUpsert)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Upsert
                @UserId = "+ this.User.FindFirst("userId")?.Value + 
                ", @PostTitle = '"+ postToUpsert.PostTitle +
                "', @PostContent ='"+ postToUpsert.PostContent + "'";


            if(postToUpsert.PostId > 0)
            {
                sql += ", @PostId = " + postToUpsert.PostId;
            }
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to create new post!");
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Delete @PostId = "+ 
                postId.ToString() + 
                ", @UserId = "+ this.User.FindFirst("userId")?.Value; 

            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete post!");
        }
    }
}
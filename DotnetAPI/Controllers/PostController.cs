using System.Data;
using Dapper;
using DotnetAPI.Data;
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

            DynamicParameters sqlParameters = new DynamicParameters();
           
            sqlParameters.Add("@UserIdParameter", userId.ToString(), DbType.String);

            if(postId != 0)
            {
                parameters += ", @PostId = @PostIdParameter"; 
                sqlParameters.Add("@PostIdParameter", postId.ToString(), DbType.String);
            }
            if(userId != 0)
            {
                parameters += ", @UserId = @UserIdParameter"; 
                sqlParameters.Add("@UserIdParameter", userId.ToString(), DbType.String);
            }
            if(searchParam.ToLower() != "none")
            {
                parameters += ", @SearchValue = @SearchParameter"; 
                sqlParameters.Add("@SearchParameter", searchParam, DbType.String);
            }
            if(parameters.Length > 0)
            {
                sql += parameters.Substring(1);
            }
            return _dapper.LoadDataWithParameters<Post>(sql, sqlParameters);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get 
                @UserId = @UserIdParameter";

            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            // DotnetAPI.Models.User
            return _dapper.LoadDataWithParameters<Post>(sql, sqlParameters);
        }
        [HttpPut("UpsertPost")]
        public IActionResult UpsertPost(Post postToUpsert)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Upsert
                @UserId = @UserIdParameter, 
                @PostTitle = @PostTitleParameter, 
                @PostContent = @PostContentParameter";
                
            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@PostTitleParameter", postToUpsert.PostTitle, DbType.String);
            sqlParameters.Add("@PostContentParameter", postToUpsert.PostContent, DbType.String);

            if(postToUpsert.PostId > 0)
            {
                sql += ", @PostId = @PostIdParameter";
                sqlParameters.Add("@PostIdParameter", postToUpsert.PostId, DbType.Int32);
            }

            if(_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }
            throw new Exception("Failed to create new post!");
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Delete @PostId = @PostIdParameter, @UserId = @UserIdParameter"; 

            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@PostIdParameter", postId, DbType.Int32);

            if(_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }
            throw new Exception("Failed to delete post!");
        }
    }
}
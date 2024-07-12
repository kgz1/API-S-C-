using BobiApi.Data;
using BobiApi.Dtos;
using BobiApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BobiApi.Controllers

{
    // [Authorize]
    [ApiController]
    [Route("Post")]

    public class PostController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        public PostController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        // [HttpGet("Posts")]
        // public IEnumerable<Post> GetPosts()
        // {
        //     string sql = @"SELECT 
        //     [PostId],
        //     [UserId],
        //     [PostTitle],
        //     [PostContent],
        //     [PostCreated],
        //     [PostUpdated] 
        //     FROM TutorialAppSchema.Posts";

        //     return _dapper.LoadData<Post>(sql);
        // }

     [HttpGet("Posts/{postId}/{userId}/{searchParam}")]
        public IEnumerable<Post> GetPosts(int postId = 0, int userId = 0, string searchParam = "None")
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get";
            string parameters = "";
            
            if(postId != 0)
            {
                parameters += ", @PostId=" + postId.ToString();
            }
            if(userId != 0)
            {
                parameters += ", @UserId=" + userId.ToString();
            }
            if(searchParam != "None")
            {
                parameters += ", @SearchValue=" + searchParam.ToString();
            }
            if(parameters.Length > 0)
            {
                sql += parameters.Substring(1);
            }
            return _dapper.LoadData<Post>(sql);
        }

        //  [HttpGet("PostSingle/{postId}")]
        // public Post GetSinglePost(int postId)
        // {
        //     string sql = @"SELECT 
        //     [PostId],
        //     [UserId],
        //     [PostTitle],
        //     [PostContent],
        //     [PostCreated],
        //     [PostUpdated] 
        //     FROM TutorialAppSchema.Posts
        //     WHERE PostId = " +postId.ToString();

        //     return _dapper.LoadDataSingle<Post>(sql);
        // }

        // [HttpGet("PostsByUser/{userId}")]
        // public IEnumerable<Post> GetPostsByUser(int userId)
        // {
        //     string sql = @"SELECT 
        //     [PostId],
        //     [UserId],
        //     [PostTitle],
        //     [PostContent],
        //     [PostCreated],
        //     [PostUpdated] 
        //     FROM TutorialAppSchema.Posts
        //     WHERE UserId = " +userId.ToString();

        //     return _dapper.LoadData<Post>(sql);
        // }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get @UserId = " + this.User.FindFirst("userId")?.Value;

            return _dapper.LoadData<Post>(sql);
        }
        
        [HttpPost("Post")]
        public IActionResult AddPost(PostToAddDto postToAdd)
        {
            string sql = @"INSERT INTO TutorialAppSchema.Posts(
            [UserId],
            [PostTitle],
            [PostContent],
            [PostCreated],
            [PostUpdated]) VALUES (" + this.User.FindFirst("userId")?.Value 
            + ",'" + postToAdd.PostTitle
            + "','" + postToAdd.PostContent
            + "', GETDATE(), GETDATE() )";

            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to create new post!");
        }
        
        [HttpPut("Post")]
        public IActionResult EditPost(PostToEditDto postToEdit)
        {
            string sql = @"
            UPDATE TutorialAppSchema.Posts
            SET PostContent = '" + postToEdit.PostContent + 
            "',  PostTitle = '" + postToEdit.PostTitle + 
            @"', PostUpdated = GETDATE()
            WHERE PostId = "+ postToEdit.PostId.ToString() +
            "AND UserId = " + this.User.FindFirst("userId")?.Value;
          
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to edit new post!");
        }


        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"
            DELETE FROM TutorialAppSchema.Posts WHERE PostId = " + postId.ToString() +
             "AND UserId = " + this.User.FindFirst("userId")?.Value;
            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed To Delete Post!");
        }
    }

}



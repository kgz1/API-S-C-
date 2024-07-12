using BobiApi.Data;
using BobiApi.Dtos;
using BobiApi.Models;
using Microsoft.AspNetCore.Mvc;

//Controller with stored procedures

[ApiController]
[Route("UserComplete")]
public class UserCompleteController : ControllerBase
{
    DataContextDapper _dapper;
    public UserCompleteController(IConfiguration config)
    {
      _dapper = new DataContextDapper(config);
    }
   
  // [HttpGet("TestConnection")]
  // public DateTime TestConnection()
  // {
  //       return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
  // }
  

  [HttpGet("GetUsers/{userId}")]
  public IEnumerable<UserComplete> GetUsers(int userId)
  {
    string sql = @"EXEC TutorialAppSchema.spUsers_Get";

    if(userId != 0)
    {
      sql +=  " @UserId=" + userId.ToString();
    }
        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);
        return users;
  }

  //  [HttpGet("GetUser/{userId}")]
  //   public User GetSingleUser(int userId)
  // {
  //    string sql = @"
  //       SELECT [UserId],
  //             [FirstName],
  //             [LastName],
  //             [Email],
  //             [Gender],
  //             [Active]
  //           FROM TutorialAppSchema.Users
  //           WHERE UserId = " + userId.ToString();

  //           User user = _dapper.LoadDataSingle<User>(sql);
  //           return user;
   
  // }

  [HttpPut("UpsertUser")]

  public IActionResult UpsertUser(UserComplete user)
  {
    //  UPDATE TutorialAppSchema.Users
    // SET [FirstName] = '" + user.FirstName + 
    //                 "', [LastName] = '" + user.LastName +
    //                 "', [Email] = '" + user.Email +
    //                 "', [Gender] = '" + user.Gender +
    //                 "', [Active] = '" + user.Active +
    // "' WHERE UserId = " + user.UserId;
    string sql = @"EXEC TutorialAppSchema.spUser_Upsert
                   @FirstName = '" + user.FirstName + 
                    "', @LastName = '" + user.LastName +
                    "', @Email = '" + user.Email +
                    "', @Gender = '" + user.Gender +
                    "', @Active = '" + user.Active +
                    "', @JobTitle = '" + user.JobTitle +
                    "', @Department = '" + user.Department +
                    "', @Salary = '" + user.Salary +
                    "', @UserId = " + user.UserId;
    if(_dapper.ExecuteSql(sql))
    {
        return Ok();
    }
    throw new Exception("Failed to Update User!");
  }

  // [HttpPost("AddUser")]
  // public IActionResult AddUser(UserDto user)
  // {
  //   string sql = @"
  //   INSERT INTO TutorialAppSchema.Users(
  //         [FirstName],
  //         [LastName],
  //         [Email],
  //         [Gender],
  //         [Active]
  //   ) VALUES (" +
  //       "'" + user.FirstName + 
  //       "', '" + user.LastName +
  //       "', '" + user.Email +
  //       "', '" + user.Gender +
  //       "', '" + user.Active +
  //   "')";

  //   if(_dapper.ExecuteSql(sql)){
  //     return Ok();
  //   }
  //   throw new Exception("Failed to Add User!");
  // }

  [HttpDelete("DeleteUser/{userId}")]

  public IActionResult DeleteUser(int userId)
  {
    string sql = @"
    EXEC TutorialAppSchema.spUser_Delete
    @UserId = " + userId.ToString(); 

  if(_dapper.ExecuteSql(sql))
  {
    return Ok();
  }
    throw new Exception("Failed to Delete User!");
  }

  // [HttpGet("UsersSalary")]
  // public IEnumerable<UserSalary> GetUsersSalaries()
  // {
  //   string sql = @"
  //   SELECT [UserId],
  //          [Salary]
  //       FROM TutorialAppSchema.UserSalary";

  //   IEnumerable<UserSalary> userSalaries = _dapper.LoadData<UserSalary>(sql);
  //   return userSalaries;
  // }
  // [HttpGet("UserSalary/{userId}")]
  // public UserSalary GetSingleUsersSalary(int userId)
  // {
  //   string sql = @"
  //   SELECT [UserId],
  //          [Salary]
  //       FROM TutorialAppSchema.UserSalary
  //       WHERE UserId = " + userId;

  //   UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql);
  //   return userSalary;
  // }

  // [HttpPut("SalaryEdit")]
  // public IActionResult EditSalary(UserSalary userSalary)
  // {
  //   string sql = @"
  //   UPDATE TutorialAppSchema.UserSalary
  //   SET [Salary] = '" + userSalary.Salary +
  //   "' WHERE UserId = " + userSalary.UserId;

  //   if(_dapper.ExecuteSql(sql))
  //   {
  //     return Ok();
  //   }
  //   throw new Exception("Salary Can't be Updated");
  // }

  // [HttpPost("AddSalary")]
  // public IActionResult AddSalary(UserSalary userSalary)
  // {
  //   string sql = @"
  //   INSERT INTO TutorialAppSchema.UserSalary(
  //         [UserId],
  //         [Salary]
  //   ) VALUES ( " +userSalary.UserId
  //     +", " + userSalary.Salary + 
  //     ")";
  //       if(_dapper.ExecuteSql(sql))
  //       {
  //         return Ok();
  //       }
  //       throw new Exception("Salary can't be added");
  // }

  // [HttpDelete("SalaryDelete/{userId}")]
  // public IActionResult DeleteSalary(int userId)
  // {
  //   string sql = @"
  //   DELETE FROM TutorialAppSchema.UserSalary 
  //   WHERE UserId = " + userId.ToString(); 
  //   if(_dapper.ExecuteSql(sql))
  //   {
  //     return Ok();
  //   }
  //   throw new Exception("Salary Can't be Deleted");
  // }

  // [HttpGet("GetUserJobInfo/{userId}")]

  // public UserJobInfo GetUserJobInfo(int userId)
  // {
  //   string sql = @"
  //   SELECT [UserId],
  //          [JobTitle],
  //          [Department]
  //       FROM TutorialAppSchema.UserJobInfo
  //       WHERE UserId = " + userId;
          
  //       UserJobInfo user = _dapper.LoadDataSingle<UserJobInfo>(sql);
  //       return user;
  // }

  // [HttpPut("UpdateUserJobInfo")]
  // public IActionResult EditUserInfo(UserJobInfo userJobInfo)
  // {
  //     string sql = @"
  //     UPDATE TutorialAppSchema.UserJobInfo
  //           SET [JobTitle] = '" + userJobInfo.JobTitle + 
  //           "', [Department] = '" + userJobInfo.Department +
  //           "' WHERE UserId = " + userJobInfo.UserId;
  //           if(_dapper.ExecuteSql(sql))
  //           {
  //             return Ok();
  //           }
  //           throw new Exception("Job Info can't be updated");
  // }

  // [HttpPost("AddUserJobInfo")]
  // public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
  // {
  //   string sql = @"
  //    INSERT INTO TutorialAppSchema.UserJobInfo(
  //           [UserId],
  //           [JobTitle],
  //           [Department]
  //     ) VALUES (" +
  //       "'" + userJobInfo.UserId + 
  //       "', '" + userJobInfo.JobTitle +
  //       "', '" + userJobInfo.Department +
  //       "')";

  //     if(_dapper.ExecuteSql(sql))
  //     {
  //       return Ok();
  //     }
  //     throw new Exception("User can't be added");
  // }

//   [HttpDelete("DeleteJobInfo/{userId}")]
//   public IActionResult DeleteUserJobInfo(int userId)
//   {
//     string sql = @"
//     DELETE FROM TutorialAppSchema.UserJobInfo 
//     WHERE UserId = " + userId.ToString(); 
//     if(_dapper.ExecuteSql(sql))
//     {
//       return Ok();
//     }
//     throw new Exception("Cant Delete User Job Info");
//   }
}
using AutoMapper;
using BobiApi.Data;
using BobiApi.Dtos;
using BobiApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace UserController.Controllers;

[ApiController]
[Route("UserEF")]
public class UserEFController : ControllerBase
{
    IUserRepository _userRepository;
    IMapper _mapper;
    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {
      // _entityFramework = new DataContextEF(config);
      _mapper = new Mapper(new MapperConfiguration(cfg => {
          cfg.CreateMap<UserDto, User>();
      }));
      _userRepository = userRepository;
    }

  [HttpGet("GetUsers")]
//   public IActionResult Test()
  public IEnumerable<User> GetUsers()
  {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;
    //  string[] responseArray = new string[] { "test1", "test2" };
    //  return responseArray;
  }

   [HttpGet("GetUser/{userId}")]
//   public IActionResult Test()
    public User GetSingleUser(int userId)
  {
        // User? user = _userRepository.GetSingleUser(userId);
        // if(user != null)
        // {
        //   return user;
        // }
        // throw new Exception("Failed to Get User!");
    //  string[] responseArray = new string[] { "test1", "test2", testValue };
    //  return responseArray;
    return _userRepository.GetSingleUser(userId);
  }

  [HttpPut("EditUser")]

  public IActionResult EditUser(User user)
  {
    User? userDb = _userRepository.GetSingleUser(user.UserId);

    if(userDb != null)
    {
      userDb.Active = user.Active;
      userDb.FirstName = user.FirstName;
      userDb.LastName = user.LastName;
      userDb.Email = user.Email;
      userDb.Gender = user.Gender;
      if(_userRepository.SaveChanges())
      {
        return Ok();
      }
        throw new Exception("Failed to Update User!");
    }
    throw new Exception("Failed to Get User!");
  }

  [HttpPost("AddUser")]
  public IActionResult AddUser(UserDto user)
  {
    User userDb = _mapper.Map<User>(user);

    _userRepository.AddEntity<User>(userDb);
    if(_userRepository.SaveChanges())
    {
      return Ok();
    }
    throw new Exception("Failed to Add User!");
  }

  [HttpDelete("DeleteUser/{userId}")]
  public IActionResult DeleteUser(int userId)
  {
    User? userDb = _userRepository.GetSingleUser(userId);

    if(userDb != null)
    {
      _userRepository.RemoveEntity<User>(userDb);
      if(_userRepository.SaveChanges())
        return Ok();
      }
        throw new Exception("Failed to Delete User!");
    }

  [HttpGet("GetSalaries")]
  public IEnumerable<UserSalary> GetSalaries()
  {
    IEnumerable<UserSalary> userSalaries = _userRepository.GetSalaries();
    return userSalaries;
  }

   [HttpGet("GetSingleSalary/{userId}")]
  public UserSalary GetSingleSalary(int userId)
  {
    // UserSalary? singleuserSalary = _entityFramework.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();
    // if(singleuserSalary != null)
    // {
    //   return singleuserSalary;
    // }
    // throw new Exception("Salary can't be returned!");
    return _userRepository.GetSingleSalary(userId);
  }

  [HttpPut("EditSalary")]
  public IActionResult EditSalaryEF(UserSalary userSalary)
  {
    UserSalary? userDbSalary = _userRepository.GetSingleSalary(userSalary.UserId);

    if(userDbSalary != null)
    {
      userDbSalary.Salary = userSalary.Salary;
      if(_userRepository.SaveChanges())
      {
        return Ok();
      }
        throw new Exception("Failed to Update User!");
    }
    throw new Exception("Failed to Get User!");
  }

[HttpDelete("DeleteSalaryEF/{userId}")]
public IActionResult DeleteSalaryEF(int userId)
{
  UserSalary? userSalaryef = _userRepository.GetSingleSalary(userId);
  if(userSalaryef != null)
  {
    _userRepository.RemoveEntity<UserSalary>(userSalaryef);
     if(_userRepository.SaveChanges())
      {
        return Ok();
      }
  }
  throw new Exception("Salary can't be deleted!");
}

[HttpPost("AddSalary")]
public IActionResult AddSalary(UserSalary userSalary)
{
  _userRepository.AddEntity<UserSalary>(userSalary);
  if(_userRepository.SaveChanges())
    {
      return Ok();
    }
    throw new Exception("Failed to Add Salary!");
}
  //EndofUsersTableMethods

[HttpGet("GetJobInfo")]
  public IEnumerable<UserJobInfo> GetJobInfos()
  {
    IEnumerable<UserJobInfo> userJobInfo = _userRepository.GetJobInfos();
    return userJobInfo;
  }

   [HttpGet("GetSingleJobInfo/{userId}")]
  public UserJobInfo GetSingleJobInfo(int userId)
  {
    // UserJobInfo? singleuserJobInfo = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();
    // if(singleuserJobInfo != null)
    // {
    //   return singleuserJobInfo;
    // }
    // throw new Exception("Job Info can't be returned!");
    return _userRepository.GetSingleJobInfo(userId);
  }

  [HttpPut("EditJobInfo")]
  public IActionResult EditJobInfo(UserJobInfo userJobInfo)
  {
    UserJobInfo? userJobInfoo = _userRepository.GetSingleJobInfo(userJobInfo.UserId);

    if(userJobInfoo != null)
    {
      userJobInfoo.JobTitle = userJobInfoo.JobTitle;
      userJobInfoo.Department = userJobInfoo.Department;
      if(_userRepository.SaveChanges())
      {
        return Ok();
      }
        throw new Exception("Failed to Update User!");
    }
    throw new Exception("Failed to Get User!");
  }

[HttpDelete("DeleteJobInfoEF/{userId}")]
public IActionResult DeleteJobInfoEF(int userId)
{
  UserJobInfo? userjobinfoef = _userRepository.GetSingleJobInfo(userId);
  if(userjobinfoef != null)
  {
    _userRepository.RemoveEntity<UserJobInfo>(userjobinfoef);
     if(_userRepository.SaveChanges())
      {
        return Ok();
      }
  }
  throw new Exception("Salary can't be deleted!");
}

[HttpPost("AddJobInfo")]
public IActionResult AddJobInfoEf(UserJobInfo userJobInfo)
{
  _userRepository.AddEntity<UserJobInfo>(userJobInfo);
  if(_userRepository.SaveChanges())
    {
      return Ok();
    }
    throw new Exception("Failed to Add Salary!");
}
}
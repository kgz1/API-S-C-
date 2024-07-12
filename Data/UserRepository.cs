using BobiApi.Models;

namespace BobiApi.Data

{
    public  class UserRepository : IUserRepository
    {
        DataContextEF _entityFramework;
        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }
        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }
        public void AddEntity<T>(T entitytoAdd)
        {
            if(entitytoAdd != null)
            {
              _entityFramework.Add(entitytoAdd);
            }
        }
        public void RemoveEntity<T>(T entitytoRemove)
        {
            if(entitytoRemove != null)
            {
                _entityFramework.Remove(entitytoRemove);
            }
        }
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList();
            return users;
        }
        public User GetSingleUser(int userId)
        {
        User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault();
        if(user != null)
        {
          return user;
        }
        throw new Exception("Failed to Get User!");
        }
        public IEnumerable<UserSalary> GetSalaries()
        {
            IEnumerable<UserSalary> userSalaries = _entityFramework.UserSalary.ToList();
            return userSalaries;
        }
        public UserSalary GetSingleSalary(int userId)
        {
            UserSalary? singleuserSalary = _entityFramework.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();
            if(singleuserSalary != null)
        {
        return singleuserSalary;
        }
            throw new Exception("Salary can't be returned!");
        }
        public IEnumerable<UserJobInfo> GetJobInfos()
        {
        IEnumerable<UserJobInfo> userJobInfo = _entityFramework.UserJobInfo.ToList();
        return userJobInfo;
        }
        public UserJobInfo GetSingleJobInfo(int userId)
        {
        UserJobInfo? singleuserJobInfo = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();
            if(singleuserJobInfo != null)
        {
            return singleuserJobInfo;
        }
            throw new Exception("Job Info can't be returned!");
        }
        }
}
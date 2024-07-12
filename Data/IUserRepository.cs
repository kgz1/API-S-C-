using BobiApi.Models;

namespace BobiApi.Data

{
    public interface IUserRepository 
    {
        public bool SaveChanges();
        public void AddEntity<T>(T entitytoAdd);
        public void RemoveEntity<T>(T entitytoRemove);
        public IEnumerable<User> GetUsers();
        public User GetSingleUser(int userId);
        public IEnumerable<UserSalary> GetSalaries();
        public UserSalary GetSingleSalary(int userId);
        public IEnumerable<UserJobInfo> GetJobInfos();
        public UserJobInfo GetSingleJobInfo(int userId);

    }

}
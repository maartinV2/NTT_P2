using NTT.Domain;
using System.Collections.Generic;
using System.Linq;

namespace NTT.Repository
{
    public class UserRepository : RepositoryBase
    {
        private readonly string _sqlConnString;

        public UserRepository(string connectionString) : base(connectionString)
        {
            _sqlConnString = connectionString;
        }

        public User GetById(string userId)
        {
            var sqlQuery = $"SELECT Id,FirstName,LastName FROM [AspNetUsers] WHERE id = '{userId}'";
            var payload = _db.Query<UserData>(sqlQuery).FirstOrDefault()?.ToDomain();
          
            _db.Dispose();
            return payload;
        }
        //public IEnumerable<User> GetAll()
        //{
        //    var sqlQuery = "SELECT * FROM [user]";
        //    var user = _db.Query<UserData>(sqlQuery);
        //    var payload = user.Select(data => data.ToDomain()).ToList();
        //    _db.Dispose();
        //    return payload;
        //}

    }

    internal class UserData 
    {
        public string id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }

        public User ToDomain()
        {

            var domain = new User 
            {
                Id = id,
                Name = firstname,
            };
            return domain;
        }
    }

}

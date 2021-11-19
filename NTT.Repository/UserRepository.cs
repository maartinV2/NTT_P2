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

        public User GetById(int userId)
        {
            var sqlQuery = $"SELECT * FROM [user] WHERE id = {userId}";
            var payload = _db.Query<UserData>(sqlQuery).FirstOrDefault()?.ToDomain();
          
            _db.Dispose();
            return payload;
        }
        public IEnumerable<User> GetAll()
        {
            var sqlQuery = "SELECT * FROM [user]";
            var user = _db.Query<UserData>(sqlQuery);
            var payload = user.Select(data => data.ToDomain()).ToList();
            _db.Dispose();
            return payload;
        }

    }

    internal class UserData 
    {
        public int id { get; set; }
        public string name { get; set; }

        public User ToDomain()
        {

            var domain = new User 
            {
                Id = id,
                Name = name,
            };
            return domain;
        }
    }

}

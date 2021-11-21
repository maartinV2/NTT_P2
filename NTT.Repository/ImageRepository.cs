
using ClearAnimal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using NTT.Domain;

namespace NTT.Repository
{
    public class ImageRepository : RepositoryBase
    {
      private readonly string _sqlConnString;

        public ImageRepository(string connectionString) : base(connectionString)
        {
            _sqlConnString = connectionString;
        }

        public IEnumerable<Image> GetAll()
        {
            var sqlQuery = "SELECT * FROM images";
            var imageData = _db.Query<ImageData>(sqlQuery);
              var payload = imageData.Select(data => data.ToDomain(_sqlConnString)).ToList();
              _db.Dispose();
              return payload;
        }

        public IEnumerable<Image> GetImageIdsByUserId(string userId)
        {
                var sqlQuery = $"SELECT id FROM images WHERE user_id = {userId}";
                var imageData = _db.Query<ImageData>(sqlQuery).ToList();
                var payload = imageData.Select(data => data.ToDomain(_sqlConnString)).ToList();
                _db.Dispose();
                return payload;
        }

        public IEnumerable<Image> GetAllImageIds()
        {
                var sqlQuery = "SELECT id FROM images";
                var imageData = _db.Query<ImageData>(sqlQuery).ToList();
                var payload = imageData.Select(data => data.ToDomain(_sqlConnString)).ToList();
                _db.Dispose();
                return payload;
        }

        public int Create(Image image)
        {
            var sqlExecute = @"INSERT INTO images (name,upload_date,user_id,type, location)
                            VALUES
                            (@name,@upload_date,@user_id,@type,@location)";
            var param = new ImageData()
            {
                name = image.Name,
                user_id = image.User.Id,
                upload_date = image.UploadDate,
                type = image.Type,
                location = image.Location
            };
            var insertedId = _db.Query<int>(sqlExecute, param).FirstOrDefault();
            return insertedId;
        }
        public int UpdateImage(string imageId, string name , bool type)
        {
            var sql = $"UPDATE images SET name = '{name}',type = '{type}', WHERE id = '{imageId}'";
            return _db.Execute(sql);
        }

        public int Delete(int id)
        {
            var sqlExecute = $"DELETE FROM images where id = {id}";
            return _db.Execute(sqlExecute);
        }

 


        public Image GetById(string imageId)
        {
            var sql = $"SELECT * from images where id = '{imageId}'";
            var payload = _db.Query<ImageData>(sql).FirstOrDefault()?.ToDomain(_sqlConnString);
            _db.Dispose();
            return payload;
        }

      
    }

    internal class ImageData
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime upload_date { get; set; }
        public string user_id { get; set; }
        public bool type { get; set; }
        public string location { get; set; }

        public Image ToDomain(string connectionString)
        {
            var image = new Image
            {
                Id = id,
                Name = name,
                UploadDate = upload_date,
                Type = type,
                Location = location
            };

            if (user_id !="")
            {
                using var userRepo = new UserRepository(connectionString);
                image.User = userRepo.GetById(user_id);
            }
          
           
            return image;
        }
    }
}

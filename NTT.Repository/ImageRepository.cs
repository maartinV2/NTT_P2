
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

        public IEnumerable<Image> GetImageIdsByUserId(int userId)
        {
                var sqlQuery = $"SELECT id FROM images WHERE user_id = {userId}";
                var animalData = _db.Query<ImageData>(sqlQuery).ToList();
                var payload = animalData.Select(data => data.ToDomain(_sqlConnString)).ToList();
                _db.Dispose();
                return payload;
        }

        public IEnumerable<Image> GetAllImageIds()
        {
                var sqlQuery = "SELECT id FROM images";
                var animalData = _db.Query<ImageData>(sqlQuery).ToList();
                var payload = animalData.Select(data => data.ToDomain(_sqlConnString)).ToList();
                _db.Dispose();
                return payload;
        }

        public int Create(Image image)
        {
            var sqlExecute = @"INSERT INTO images (id,name,upload_date,user_id,type, location)
                            VALUES
                            (@id,@name,@upload_date,@user_id,@type,@location)";
            var param = new ImageData()
            {
                id = image.Id,
                user_id = image.User.Id,
                upload_date = image.UploadDate,
                type = image.Type,
                location = image.Location
            }; 
            return _db.Execute(sqlExecute, param);
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

        public int SetUserId(int imageId, int userId)
        {   
            if (userId > 0)
            {
                var sqlExecute = $"UPDATE images SET user_Id = {userId} WHERE id = {imageId}";
                return _db.Execute(sqlExecute);
            }
            else { var sqlExecute = $"UPDATE images SET user_Id = NULL WHERE id = {imageId}";
                return _db.Execute(sqlExecute);
            }
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
        public int user_id { get; set; }
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

            if (user_id > 0)
            {
                using var userRepo = new UserRepository(connectionString);
                image.User = userRepo.GetById(user_id);
            }
          
           
            return image;
        }
    }
}

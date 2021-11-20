using System;
using ClearAnimal.Domain;
using Microsoft.CodeAnalysis;
using NTT.Domain;

namespace NTT.API.Models.Dto
{
    public class ImageDto 
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public Age Age { get; set; }
        public string UploadDateString { get; set; }
        public bool Type { get; set; }
        public string Location { get; set; }
        public UserDto User { get; set; }
        public string Url { get; set; }
        public ImageDto FromDomain(Image image)
        {
            return new ImageDto
            {
                Id = image.Id,
                Name = image.Name,
                UploadDateString = image.UploadDate.ToString("yyyyMMdd"),
                Type =image.Type,
                Location=image.Location,
                User = new UserDto().FromDomain(image.User),
                Age = image.Age,
                Url = image.Url
            };
        }

        public Image ToDomain()
        {
            return new Image
            {
                Id = Id,
                Name = Name,
                Type = Type,
                UploadDate = UploadDateString == null ? new DateTime(1970, 1, 1) : DateTime.ParseExact(UploadDateString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                Location = Location,
                User = User.ToDomain(),
            };
        }
    }
}

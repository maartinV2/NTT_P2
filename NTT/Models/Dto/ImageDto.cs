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
        public DateTime UploadDate { get; set; }
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
                UploadDate = image.UploadDate,
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
            };
        }
    }
}

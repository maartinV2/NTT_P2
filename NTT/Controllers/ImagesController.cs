using NTT.Azure.Repository;
using NTT.Domain;
using NTT.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NTT.API.Models.Dto;

namespace NTT.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly string _connectionStringAzure;
        private readonly string _azureBlobContainer;


        public ImagesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
#if DEBUG
            _connectionStringAzure = configuration.GetValue<string>("ConnectionStrings:AzureStoreConnection");
            _azureBlobContainer = configuration.GetValue<string>("ConnectionStrings:AzureMasterBlobContainer");

#else
            _connectionStringAzure = configuration.GetValue<string>("ConnectionStrings:AzureStoreConnection");
            _azureBlobContainer = configuration.GetValue<string>("ConnectionStrings:AzureMasterBlobContainer");
#endif


        }

        [HttpGet]
        public IEnumerable<ImageDto> Get()
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var images = imageRepo.GetAll().ToList();
            return images.Select(image => new ImageDto().FromDomain(image));
        }

        [HttpGet("~/api/GetImageIdsByUserId/{userId}")]
        public IEnumerable<AllImagesDto> GetImageIdsByUserId(int userId)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var images = imageRepo.GetImageIdsByUserId(userId).ToList();
            return images.Select(image => new AllImagesDto().FromDomain(image));
        }

        [HttpGet("~/api/image/GetAllImageIds/")]
        public IEnumerable<AllImagesDto> GetAllImageIds(int userId)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var images = imageRepo.GetAllImageIds().ToList();
            return images.Select(image => new AllImagesDto().FromDomain(image));
        }

        [HttpGet("~/api/image/{imageId}")]
        public ActionResult<ImageDto> GetById(string imageId)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var image = imageRepo.GetById(imageId);
            var fileRepo = new FileRepository(_connectionStringAzure);
            image.FileExists = fileRepo.FileExist(_azureBlobContainer, $"images/{image.FileName}");
            var animalDto = new ImageDto().FromDomain(image);
            return animalDto;
        }


        [HttpPost]
        public ActionResult<int> Create([FromBody] ImageDto imageDto)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            imageRepo.Create(imageDto.ToDomain());
            return 0;
        }
        [HttpDelete]
        public ActionResult<int> Delete([FromBody] int id)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            imageRepo.Delete(id);
            return 1;
        }
        [HttpPut("~/api/image/test")]
        public ActionResult<int> UpdateAnimal([FromBody]  ImageDto updatedImage)
        {
            var image = new Image
            {
                Id = updatedImage.Id,
                Name = updatedImage.Name,
                Type = updatedImage.Type
            };
            using var imageRepo = new ImageRepository(_connectionString);
              imageRepo.UpdateImage(image.Id, image.Name,image.Type);
            return 7;
        }

        [HttpPut]
        public ActionResult<int> Put([FromBody] JObject body)
        {
            var animalID = int.Parse(body["body"]["image_id"].ToString());
            var farmerID = int.Parse(body["body"]["user_id"].ToString());
            using var imageRepo = new ImageRepository(_connectionString);
            imageRepo.SetUserId(animalID, farmerID);
            return 2;
        }


        [HttpPost("~/api/image/{image_id}")]
        public IActionResult Upload(string image_id)
        {
            try
            {

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    string folder = "Images/";

                    string fileName = folder + image_id + ".jpg";

                    string fileType = file.ContentType;

                    var stream = new MemoryStream((int)file.Length);

                    file.CopyTo(stream);

                    var fileByteArray = stream.ToArray();

                    stream.Close();

                    var fileRepo = new FileRepository(_connectionStringAzure);

                    var fileUrl = fileRepo.UploadFileToBlob(fileName, fileByteArray, fileType, _azureBlobContainer);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}

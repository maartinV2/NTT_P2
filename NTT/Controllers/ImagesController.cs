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
            var fileRepo = new FileRepository(_connectionStringAzure);
            foreach (var image in images)
            {
                image.FileExists = fileRepo.FileExist(_azureBlobContainer, $"{image.User.Id}/post/{image.FileName}");
            }
            return images.Select(image => new ImageDto().FromDomain(image));
        }
     


        [HttpGet("GetImageIdsByUserId/{userId}")]
        public IEnumerable<AllImagesDto> GetImageIdsByUserId(string userId)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var images = imageRepo.GetImageIdsByUserId(userId).ToList();
            return images.Select(image => new AllImagesDto().FromDomain(image));
        }

        [HttpGet("image/GetAllImageIds/")]
        public IEnumerable<AllImagesDto> GetAllImageIds(string userId)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var images = imageRepo.GetAllImageIds().ToList();
            return images.Select(image => new AllImagesDto().FromDomain(image));
        }

        [HttpGet("{imageId}")]
        public ActionResult<ImageDto> GetById(string imageId)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var image = imageRepo.GetById(imageId);
            var fileRepo = new FileRepository(_connectionStringAzure);
            image.FileExists = fileRepo.FileExist(_azureBlobContainer, $"{image.User.Id}/post/{image.FileName}");
            var imageDto = new ImageDto().FromDomain(image);
            return imageDto;
        }

        [HttpPut]
        public ActionResult<int> Update([FromBody] ImageDto imageDto)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var id = imageRepo.UpdateImage(imageDto.ToDomain());
            return id;
        }

        [HttpPost]
        public ActionResult<int> Create([FromBody] ImageDto imageDto)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            var id= imageRepo.Create(imageDto.ToDomain());
            return id;
        }


        [HttpDelete]
        public ActionResult<int> Delete([FromBody] int id)
        {
            using var imageRepo = new ImageRepository(_connectionString);
            imageRepo.Delete(id);
            return 1;
        }
       



        [HttpPost("~/api/post/image/{imageId}:{userId}")]
        public IActionResult Upload(string imageId,string userId )
        {
            try
            {

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    string folder = userId+"/post/";

                    string fileName = folder + imageId + ".jpg";

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

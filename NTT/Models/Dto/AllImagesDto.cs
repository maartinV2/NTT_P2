using ClearAnimal.Domain;
using NTT.Domain;

namespace NTT.API.Models.Dto
{
    public class AllImagesDto
    {
            
        public string Id { get; set; }
        public AllImagesDto FromDomain(Image animal)
        {
            return new AllImagesDto
            {
                Id = animal.Id
            };
        }

    }
}

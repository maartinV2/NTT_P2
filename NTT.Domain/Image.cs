using ClearAnimal.Domain;
using System;

namespace NTT.Domain
{
    public class Image
    {

        const string AzureUrl = "https://minstagram.blob.core.windows.net/users";


        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public bool Type { get; set; }
        public string Location { get; set; }

        public User User { get; set; }

        public bool FileExists { get; set; }

        public string FileName => $"{Id}.jpg";


        public Age Age { get
            {
                

                DateTime zeroTime = new DateTime(1, 1, 1);
                TimeSpan span = DateTime.Now - UploadDate;
                if(span.Ticks < 0)
                {
                    return new Age { Years = 0, Months = 0 };
                }
                var age = new Age();
                age.Years = (zeroTime + span).Year - 1;
                age.Months = (zeroTime + span).Month - 1;
                
                return age;
            } }
    


        public string Url => FileExists ? $"{AzureUrl}/{User.Id}/post/{FileName}" : "https://minstagram.blob.core.windows.net/server/no-image-available.jpg";
    }
}

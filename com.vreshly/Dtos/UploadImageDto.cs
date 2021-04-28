using System;
using Microsoft.AspNetCore.Http;

namespace com.vreshly.Dtos
{
    public class UploadImageDto
    {
        public UploadImageDto()
        {
        }

        public IFormFile UploadImage { get; set; }
    }
}

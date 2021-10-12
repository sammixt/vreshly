using System;
namespace com.vreshly.Dtos
{
    public class BannerDto : UploadImageDto
    {
        public BannerDto()
        {
        }
        public long Id { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        public string ImageOne { get; set; }
        public string TitleOne { get; set; }
        public string SubTitleOne { get; set; }

        public string ImageTwo { get; set; }
        public string TitleTwo { get; set; }
        public string SubTitleTwo { get; set; }

        public string ImageThree { get; set; }
        public string TitleThree { get; set; }
        public string SubTitleThree { get; set; }

        public string ImageFour { get; set; }
        public string TitleFour { get; set; }
        public string SubTitleFour { get; set; }

        public string SubPageImage { get; set; }

       
    }

    
}

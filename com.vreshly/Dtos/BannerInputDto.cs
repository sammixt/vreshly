using System;
namespace com.vreshly.Dtos
{
    public class BannerInputDto : UploadImageDto
    {
        public BannerInputDto()
        {
        }

        public string Image { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public SliderTypes SliderTypes { get; set; }
    }

    public enum SliderTypes
    {
        BannerOne = 1,
        BannerTwo = 2,
        BannerThree = 3,
        BannerFour = 4,
        SubPageBanner = 5
    }
}

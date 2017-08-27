using ProductListMVCDemo.Objects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace ProductListMVCDemo.Objects.Products
{
    public class GameProduct : ProductBase
    {
        private const string ProductTypeString = "Car";

        [Required]
        public GameProductType GameType { get; set; }

        [Required]
        public int YearOfRelease { get; set; }

        [Required]
        public int RecommendedAge { get; set; }

        public override string GetAdditionalInformation()
        {
            StringBuilder infoSb = new StringBuilder();

            switch (GameType)
            {
                case GameProductType.BoardGame:
                    infoSb.Append("Board Game");
                    break;
                case GameProductType.VideoGame:
                    infoSb.Append("Video Game");
                    break;
            }

            infoSb.Append(", ");
            infoSb.Append(YearOfRelease);
            infoSb.Append(", Ages ");
            infoSb.Append(RecommendedAge);
            infoSb.Append("+");

            return infoSb.ToString();
        }

        public override string GetProductTypeString()
        {
            return ProductTypeString;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class Color
    {
        public string ColorName { get; set; }

    }

    public class Size
    {
        public string SizeName { get; set; }

    }


    public enum statusEnum
    {
        Ordered = 1,
        Preparation = 2,
        Ready = 3,
        Delivered = 4,
    }

    public enum ProductTypeEnum
    {
        Bra = 10,
        Briefs = 50,
        Activewear = 10,
        Knitwear = 10,
    }

}
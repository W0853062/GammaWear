﻿using GammaWear.Models;

namespace GammaWear.ViewModels
{

    public class SockDetailsViewModel
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public string SockStyleName { get; set; }
        public string OutdoorSportName { get; set; }
        public SockSize SockSize { get; set; }
        public ConsumerGroup ConsumerGroup { get; set; }
        public string SeasonName { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public string ImageFile { get; set; }
        public string Description { get; set; }
    }

}

﻿using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public SliderContent SliderContent { get; set; }
    }
}

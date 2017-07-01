using DND.Core.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Core.ViewModels
{
    public class CarouselViewModel
    {
        public int ItemCount { get; set; }
        public IList<BlogPostDTO> Posts { get; set; }

        public IList<DirectoryInfo> Albums { get; set; }
        public IList<CarouselItemDTO> AlbumCarouselItems { get; set; }

        public IList<CarouselItemDTO> CarouselItems { get; set; }
    }
}

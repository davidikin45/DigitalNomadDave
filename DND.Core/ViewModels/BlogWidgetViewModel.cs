using DND.Core.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Core.ViewModels
{
    public class BlogWidgetViewModel
    {
        public IList<CategoryDTO> Categories { get; set; }
        public IList<TagDTO> Tags { get; set; }
        public IList<BlogPostDTO> LatestPosts { get; set; }
        public IList<FileInfo> LatestPhotos { get; set; }
    }
}

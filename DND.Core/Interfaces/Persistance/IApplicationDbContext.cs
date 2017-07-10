using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Solution.Base.Interfaces.Persistance;
using DND.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DND.Core.Interfaces.Persistance
{
    public interface IApplicationDbContext : IBaseDbContext
    {
        IDbSet<BlogPost> BlogPosts { get; set; }
        IDbSet<BlogPostTag> BlogPostTags { get; set; }
        IDbSet<BlogPostLocation> BlogPostLocations { get; set; }

        IDbSet<Author> Authors { get; set; }
        IDbSet<Tag> Tags { get; set; }
        IDbSet<Category> Categories { get; set; }

        IDbSet<Location> Locations { get; set; }

        IDbSet<CarouselItem> CarouselItems { get; set; }

        IDbSet<Project> Projects { get; set; }
        IDbSet<Testimonial> Testimonials { get; set; }

        //IDbSet<Place> Places { get; set; }

        //IDbSet<Customer> Customers { get; set; }
        //IDbSet<Opportunity> Opportunities { get; set; }
        //IDbSet<Risk> Risks { get; set; }     

    }
}

using DND.Core.Interfaces.Persistance;
using DND.Core.Model;
using DND.Core.DTOs;
using Solution.Base.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;

namespace DND.Core.Interfaces.Services
{
    public interface IBlogPostService : IBaseEntityService<BlogPostDTO>
    {
        Task<BlogPostDTO> GetPostAsync(int year, int month, string titleSlug, CancellationToken cancellationToken);

        Task<IEnumerable<BlogPostDTO>> GetPostsAsync(int pageNo, int pageSize, CancellationToken cancellationToken);
        Task<IEnumerable<BlogPostDTO>> GetPostsForCarouselAsync(int pageNo, int pageSize, CancellationToken cancellationToken);
        Task<int> GetTotalPostsAsync(bool checkIsPublished, CancellationToken cancellationToken);

        Task<IEnumerable<BlogPostDTO>> GetPostsForAuthorAsync(string authorSlug, int pageNo, int pageSize, CancellationToken cancellationToken);
        Task<int> GetTotalPostsForAuthorAsync(string authorSlug, CancellationToken cancellationToken);

        Task<IEnumerable<BlogPostDTO>> GetPostsForCategoryAsync(string categorySlug, int pageNo, int pageSize, CancellationToken cancellationToken);
        Task<int> GetTotalPostsForCategoryAsync(string categorySlug, CancellationToken cancellationToken);


        Task<IEnumerable<BlogPostDTO>> GetPostsForTagAsync(string tagSlug, int pageNo, int pageSize, CancellationToken cancellationToken);
        Task<int> GetTotalPostsForTagAsync(string tagSlug, CancellationToken cancellationToken);


        Task<IEnumerable<BlogPostDTO>> GetPostsForSearchAsync(string search, int pageNo, int pageSize, CancellationToken cancellationToken);
        Task<int> GetTotalPostsForSearchAsync(string search, CancellationToken cancellationToken);

        //Admin
        Task<IEnumerable<BlogPostDTO>> GetPostsAsync(int pageNo, int pageSize, Expression<Func<IQueryable<BlogPostDTO>, IOrderedQueryable<BlogPostDTO>>> orderBy, CancellationToken cancellationToken);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Solution.Base.Interfaces.Model;
using System.Data.Entity.Core.Objects;
using System.IO;

namespace Solution.Base.Interfaces.Repository
{
    public interface IBaseFileReadOnlyRepository : IDisposable
    {

        IEnumerable<FileInfo> GetAll(
            Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<FileInfo>> GetAllAsync(
          Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
          int? skip = null,
          int? take = null);

        IEnumerable<FileInfo> Get(
            Expression<Func<FileInfo, bool>> filter = null,
            Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
            int? skip = null,
            int? take = null)
            ;

        Task<IEnumerable<FileInfo>> GetAsync(
           Expression<Func<FileInfo, bool>> filter = null,
           Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
           int? skip = null,
           int? take = null)
           ;

        IEnumerable<FileInfo> Search(
          string search = "",
          Expression<Func<FileInfo, bool>> filter = null,
          Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
          int? skip = null,
          int? take = null)
          ;

        Task<IEnumerable<FileInfo>> SearchAsync(
           string search = "",
           Expression<Func<FileInfo, bool>> filter = null,
           Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
           int? skip = null,
           int? take = null)
           ;

        FileInfo GetOne(
            Expression<Func<FileInfo, bool>> filter = null)
            ;

        Task<FileInfo> GetOneAsync(
         Expression<Func<FileInfo, bool>> filter = null)
         ;

        FileInfo GetFirst(
            Expression<Func<FileInfo, bool>> filter = null,
            Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null)
            ;

        Task<FileInfo> GetFirstAsync(
          Expression<Func<FileInfo, bool>> filter = null,
          Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null)
          ;

        FileInfo GetByPath(string path)
            ;

        Task<FileInfo> GetByPathAsync(string path)
           ;

        int GetCount(Expression<Func<FileInfo, bool>> filter = null)
            ;

        Task<int> GetCountAsync(Expression<Func<FileInfo, bool>> filter = null)
           ;

        int GetSearchCount(string search = "", Expression<Func<FileInfo, bool>> filter = null)
          ;

        Task<int> GetSearchCountAsync(string search= "", Expression<Func<FileInfo, bool>> filter = null)
         ;

        bool GetExists(Expression<Func<FileInfo, bool>> filter = null)
            ;

        Task<bool> GetExistsAsync(Expression<Func<FileInfo, bool>> filter = null)
           ;

        FileInfo GetMain()
         ;

        Task<FileInfo> GetMainAsync();

    }

}

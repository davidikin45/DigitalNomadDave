using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Solution.Base.Interfaces.Model;
using Solution.Base.Interfaces.Repository;
using Solution.Base.Interfaces.Persistance;

using System.Threading.Tasks;
using System.Data.Entity;
using System.Threading;
using System.IO;

namespace Solution.Base.Implementation.Repository.File
{
    public class BaseFileReadOnlyRepository : IBaseFileReadOnlyRepository
    {
        protected readonly string _physicalPath;
        protected readonly string _searchPattern;
        protected readonly SearchOption _searchOption;
        protected readonly CancellationToken _cancellationToken;
        protected readonly string[] _extensions;

        public BaseFileReadOnlyRepository(string physicalPath, Boolean includeSubDirectories, string searchPattern = "*.*",  CancellationToken cancellationToken = default(CancellationToken), params string[] extensions)
        {
            if (!physicalPath.EndsWith("\\"))
            {
                physicalPath = physicalPath + "\\";
            }

            if (!System.IO.Directory.Exists(physicalPath))
            {
                throw new Exception("Path: " + physicalPath + " does not exist");
            }

            this._physicalPath = physicalPath;

            _searchOption = SearchOption.TopDirectoryOnly;
            if (includeSubDirectories)
            {
                _searchOption = SearchOption.AllDirectories;
            }
            _searchPattern = searchPattern;
            _cancellationToken = cancellationToken;
            _extensions = extensions;
        }

        protected virtual IQueryable<FileInfo> GetQueryable(
            Expression<Func<FileInfo, bool>> filter = null,
            Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<FileInfo> query = new DirectoryInfo(_physicalPath).EnumerateFiles(_searchPattern, _searchOption).AsQueryable();

            if (_extensions != null && _extensions.Count() > 0)
            {
                query = query.Where(f => _extensions.Contains(f.Extension.ToLower()));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        protected virtual IEnumerable<FileInfo> GetIEnumerable(
          Expression<Func<FileInfo, bool>> filter = null,
          Func<IEnumerable<FileInfo>, IOrderedEnumerable<FileInfo>> orderBy = null,
          int? skip = null,
          int? take = null)
        {
            IEnumerable<FileInfo> query = new DirectoryInfo(_physicalPath).GetFiles(_searchPattern, _searchOption).ToList();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (_extensions != null && _extensions.Count() > 0)
            {
                query = query.Where(f => _extensions.Contains(f.Extension.ToLower()));
            }

            if (filter != null)
            {
                query = query.Where(filter.Compile());
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual IEnumerable<FileInfo> GetAll(
          Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
          int? skip = null,
          int? take = null)
        {
            return GetQueryable(null, orderBy, skip, take).ToList();
        }

        public async virtual Task<IEnumerable<FileInfo>> GetAllAsync(
         Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
         int? skip = null,
         int? take = null)
        {
            return GetQueryable(null, orderBy, skip, take).ToList();
        }

        public virtual IEnumerable<FileInfo> Get(
            Expression<Func<FileInfo, bool>> filter = null,
            Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(filter, orderBy, skip, take).ToList();
        }

        public async virtual Task<IEnumerable<FileInfo>> GetAsync(
           Expression<Func<FileInfo, bool>> filter = null,
           Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null,
           int? skip = null,
           int? take = null)
        {
            return GetQueryable(filter, orderBy, skip, take).ToList();
        }

        public virtual FileInfo GetOne(
            Expression<Func<FileInfo, bool>> filter = null)
        {
            return GetQueryable(filter, null, null, null).SingleOrDefault();
        }

        public async virtual Task<FileInfo> GetOneAsync(
           Expression<Func<FileInfo, bool>> filter = null)
        {
            return GetQueryable(filter, null, null, null).SingleOrDefault();
        }

        public virtual FileInfo GetMain()
        {
            FileInfo main = null;

            var ordered = GetQueryable(null, o => o.OrderBy(f => f.LastWriteTime), null, null);
            main = ordered.FirstOrDefault();

            if (main != null)
            {
                var mainFile = ordered.Where(f => f.Name.ToLower().Contains("main")).FirstOrDefault();
                if (mainFile != null)
                {
                    main = mainFile;
                }
            }

            return main;
        }

        public async virtual Task<FileInfo> GetMainAsync()
        {
            FileInfo main = null;

            var ordered =  await GetQueryable(null, o => o.OrderByDescending(f => f.LastWriteTime), null, null).ToListAsync(_cancellationToken);

            main = ordered.FirstOrDefault();

            if (ordered.Count > 0)
            {
                var mainFile = ordered.Where(f => f.Name.ToLower().Contains("main")).FirstOrDefault();
                if (mainFile != null)
                {
                    main = mainFile;
                }
            }

            return main;
        }

        public virtual FileInfo GetFirst(
           Expression<Func<FileInfo, bool>> filter = null,
           Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null)
        {
            return GetQueryable(filter, orderBy, null, null).FirstOrDefault();
        }

        public async virtual Task<FileInfo> GetFirstAsync(
          Expression<Func<FileInfo, bool>> filter = null,
          Func<IQueryable<FileInfo>, IOrderedQueryable<FileInfo>> orderBy = null)
        {
            return GetQueryable(filter, orderBy, null, null).FirstOrDefault();
        }

        public virtual FileInfo GetByPath(string path)
        {
            return GetQueryable(f => f.FullName.ToLower().EndsWith(path.ToLower())  , null, null, null).FirstOrDefault();
        }

        public async virtual Task<FileInfo> GetByPathAsync(string path)
        {
            return GetQueryable(f => f.FullName.ToLower().EndsWith(path.ToLower()), null, null, null).FirstOrDefault();
        }

        public virtual int GetCount(Expression<Func<FileInfo, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public async virtual Task<int> GetCountAsync(Expression<Func<FileInfo, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public virtual bool GetExists(Expression<Func<FileInfo, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public async virtual Task<bool> GetExistsAsync(Expression<Func<FileInfo, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public void Dispose()
        {
            
        }
    }
}

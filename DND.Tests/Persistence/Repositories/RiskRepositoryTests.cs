using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solution.Base.Implementation.Repository.EntityFramework;
using DND.Core.Models;
using Solution.Base.Interfaces.Persistance;
using DND.Core.Interfaces.Persistance;
using Moq;
using Solution.Base.Testing;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;

namespace DND.Tests.Persistence.Repositories
{
    [TestClass]
    public class RiskRepositoryTests
    {
        private BaseEFRepository<IApplicationDbContext, BlogPost> _repository;
        private DbSet<BlogPost> _dbSet;
        private List<BlogPost> _list;

        [TestInitialize]
        public void TestInitialize()
        {
            _list = new List<BlogPost>();
            _dbSet = TestHelpers.MockDbSet<BlogPost>(_list);
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.BlogPosts).Returns(_dbSet);
            mockContext.Setup(c => c.Set<BlogPost>()).Returns(_dbSet);
            _repository = new BaseEFRepository<IApplicationDbContext, BlogPost>(mockContext.Object, true);
        }

        [TestMethod]
        public void GetAllRisks_All_ShouldReturnAll()
        {
            var post = new BlogPost();
            _list.Add(post);
            _list.Add(post);

            var result = _repository.GetCount();

            result.Should().Be(2);
        }
    }
}

using NUnit.Framework;
using DND.Controllers;
using DND.Services;
using Solution.Base.Implementation.UnitOfWork;
using DND.EFPersistance;
using Solution.Base.Implementation.Repository;
using AutoMapper;
using Solution.Base.Implementation.Logging;
using Solution.Base.Testing;
using Solution.Base.Interfaces.Persistance;
using Moq;
using System.Linq;
using DND.Core.Model;
using DND.Core.Interfaces.Persistance;

namespace DND.IntegrationTests.Controllers
{
    [TestFixture]
    public class FlightSearchControllerTests
    {
        private FlightSearchController _controller;
        private ApplicationDbContext _context;
        private IMapper _mapper;


        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => {
              
            });
             _mapper = config.CreateMapper();

            _context = new ApplicationDbContext();
            var mockDbContextFactory = new Mock<IDbContextFactory>();
            mockDbContextFactory.Setup(c => c.CreateDefault()).Returns(_context);


            _controller = new FlightSearchController(new FlightSearchService(new BaseUnitOfWorkScopeFactory(mockDbContextFactory.Object, new BaseAmbientDbContextLocator(), new BaseRepositoryFactory()), _mapper), _mapper, new LogFactory());      
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Search_WhenCalled_ShouldReturn()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);


            var unitOfWorkFactory = new BaseUnitOfWorkScopeFactory(new DbContextFactory(), new BaseAmbientDbContextLocator(), new BaseRepositoryFactory());

            var post = new BlogPost();
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
               unitOfWork.Repository<IApplicationDbContext, BlogPost>().Create(post);
               
                using (var unitOfWork2 = unitOfWorkFactory.Create())
                {
                    unitOfWork2.Repository<IApplicationDbContext, BlogPost>().Create(post);
                    unitOfWork2.Complete();
                }
                unitOfWork.Complete();
            }

            //Act


            //Assert
        }
    }
}

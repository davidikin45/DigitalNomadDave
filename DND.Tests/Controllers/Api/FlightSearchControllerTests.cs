using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Security.Claims;
using DND.Controllers;
using DND.Services;
using Solution.Base.Implementation.UnitOfWork;
using Moq;
using DND.Core.Interfaces.Services;
using AutoMapper;
using DND.Core.ViewModels;
using DND.Core.DTOs;
using System.Web.Mvc;
using Solution.Base.Interfaces.UnitOfWork;
using Solution.Base.Interfaces.Logging;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Solution.Base.Testing;
using Solution.Base.ActionResults;
using System.Collections.Specialized;
using System.Globalization;

namespace DND.Tests.Controllers.Api
{
    /// <summary>
    /// Summary description for FlightSearchControllerTests
    /// </summary>
    [TestClass]
    public class FlightSearchControllerTests
    {
        private FlightSearchController _controller;

        [TestInitialize()]
        public void TestInitialize() {
            var expected = new FlightSearchRequestDTO();
            var mockService = new Mock<IFlightSearchService>();
            mockService.Setup(s => s.SearchAsync(It.IsAny<FlightSearchRequestDTO>(), It.IsAny<CancellationToken>())).ReturnsAsync(new FlightSearchResponseDTO(new List<ItineraryDTO>(), 0, 10, 1));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<FlightSearchClientRequestForm, FlightSearchRequestDTO>(It.IsAny<FlightSearchClientRequestForm>()))
                .Returns(expected);

            var mockLogger = new Mock<ILogFactory>();

            _controller = new FlightSearchController(mockService.Object, mockMapper.Object, mockLogger.Object);
            //_controller.MockHttpContext("1", "d.ikin@test.com");
        }

        [TestMethod]
        public void Search_Request_ShouldReturnBetterJsonResultResponse()
        {
            var model = new FlightSearchClientRequestForm();
            //var task = _controller.Search(model);
            //task.Wait();
            // result = task.Result;
           // result.Should().BeOfType<BetterJsonResult<FlightSearchResponseDTO>>();
        }

        [TestMethod]
        public void Search_InvalidRequest_ShouldReturnError()
        {
            var model = new FlightSearchClientRequestForm();
           // _controller.BindModelToController(model);

            //var task = _controller.Search(new FlightSearchClientRequestForm());
          //  task.Wait();
            //var result = (BetterJsonResult)task.Result;
        }
    }
}

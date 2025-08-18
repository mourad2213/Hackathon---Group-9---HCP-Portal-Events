using HCP_Portal_Events.Controllers;
using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models;
using HCP_Portal_Events.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HCP_Portal_Events.Tests.Controllers
{
    public class UserRegistrationControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserRepositiories> _mockUserRepository;
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly Mock<IUserRegistrationRepository> _mockUserRegistrationRepository;
        private readonly UserRegistrationController _controller;

        public UserRegistrationControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IUserRepositiories>();
            _mockEventRepository = new Mock<IEventRepository>();
            _mockUserRegistrationRepository = new Mock<IUserRegistrationRepository>();

            _mockUnitOfWork.Setup(u => u.UserRepositiory).Returns(_mockUserRepository.Object);
            _mockUnitOfWork.Setup(u => u.EventRepository).Returns(_mockEventRepository.Object);
            _mockUnitOfWork.Setup(u => u.UserRegistrationRepository).Returns(_mockUserRegistrationRepository.Object);

            _controller = new UserRegistrationController(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task BookEvent_ReturnsOk_WhenSuccessful()
        {
            
            int userId = 1, eventId = 10;
            var user = new UserReadDTO { Id = userId, UserName = "John" };
            var ev = new EventReadDTO { Id = eventId, Title = "Tech Event" };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mockEventRepository.Setup(r => r.GetEventByIdAsync(eventId)).ReturnsAsync(ev);
            _mockUserRegistrationRepository.Setup(r => r.BookEventAsync(userId, eventId)).ReturnsAsync(true);

            
            var result = await _controller.BookEvent(userId, eventId);

           
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("successfully", okResult.Value.ToString());
        }

        [Fact]
        public async Task BookEvent_ReturnsConflict_WhenAlreadyRegistered()
        {
            int userId = 1, eventId = 10;
            var user = new UserReadDTO { Id = userId };
            var ev = new EventReadDTO { Id = eventId };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mockEventRepository.Setup(r => r.GetEventByIdAsync(eventId)).ReturnsAsync(ev);
            _mockUserRegistrationRepository.Setup(r => r.BookEventAsync(userId, eventId)).ReturnsAsync(false);

            var result = await _controller.BookEvent(userId, eventId);

            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task BookEvent_ReturnsNotFound_WhenUserDoesNotExist()
        {
            int userId = 1, eventId = 10;

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((UserReadDTO)null);

            var result = await _controller.BookEvent(userId, eventId);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UnbookEvent_ReturnsOk_WhenSuccessful()
        {
            int userId = 2, eventId = 20;
            var user = new UserReadDTO { Id = userId };
            var ev = new EventReadDTO { Id = eventId };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mockEventRepository.Setup(r => r.GetEventByIdAsync(eventId)).ReturnsAsync(ev);
            _mockUserRegistrationRepository.Setup(r => r.UnbookEventAsync(userId, eventId)).ReturnsAsync(true);

            var result = await _controller.UnbookEvent(userId, eventId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("unbooked", okResult.Value.ToString());
        }

        [Fact]
        public async Task UnbookEvent_ReturnsNotFound_WhenNotRegistered()
        {
            int userId = 2, eventId = 20;
            var user = new UserReadDTO { Id = userId };
            var ev = new EventReadDTO { Id = eventId };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mockEventRepository.Setup(r => r.GetEventByIdAsync(eventId)).ReturnsAsync(ev);
            _mockUserRegistrationRepository.Setup(r => r.UnbookEventAsync(userId, eventId)).ReturnsAsync(false);

            var result = await _controller.UnbookEvent(userId, eventId);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task IsUserRegistered_ReturnsOk_WithTrue()
        {
            int userId = 3, eventId = 30;
            var user = new UserReadDTO { Id = userId };
            var ev = new EventReadDTO { Id = eventId };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mockEventRepository.Setup(r => r.GetEventByIdAsync(eventId)).ReturnsAsync(ev);
            _mockUserRegistrationRepository.Setup(r => r.IsUserRegisteredAsync(userId, eventId)).ReturnsAsync(true);

            var result = await _controller.IsUserRegistered(userId, eventId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("True", okResult.Value.ToString());
        }
    }
}

using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HCP_Portal_Events.Controllers;
using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models;
using HCP_Portal_Events.Models.DTOs;

namespace HCP_Portal_Events.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserRepositiories> _mockUserRepository;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IUserRepositiories>();
            _mockUnitOfWork.Setup(u => u.UserRepositiory).Returns(_mockUserRepository.Object);
            _controller = new UserController(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetUser_ReturnsOk_WhenUserExists()
        {
            var user = new UserReadDTO { Id = 1, UserName = "John Doe", Email = "john@example.com" };
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _controller.GetUser(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserReadDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync((UserReadDTO)null);

            var result = await _controller.GetUser(1);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsNoContent_WhenUserUpdated()
        {
            var existingUser = new UserReadDTO { Id = 1, UserName = "John" };
            var updateDto = new UserUpdateDto
            {
                UserName = "NewName",
                Email = "new@example.com",
                PhoneNumber = 123,
                ProfilePicturePath = "pic.jpg"
            };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(1))
                .ReturnsAsync(existingUser);

            _mockUserRepository.Setup(r => r.UpdateUserAsync(1, updateDto))
                .ReturnsAsync(true); 

            _mockUnitOfWork.Setup(u => u.CompleteAsync())
                .ReturnsAsync(1);

            var result = await _controller.UpdateUser(1, updateDto);

            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task UpdateUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var updateDto = new UserUpdateDto { UserName = "NewName" };
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync((UserReadDTO)null);

            var result = await _controller.UpdateUser(1, updateDto);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetUserPreviousEvents_ReturnsOk_WithEvents()
        {
            var events = new List<EventReadDTO> { new EventReadDTO { Id = 1, Title = "Past Event", Status = "Previous" } };
            _mockUserRepository.Setup(r => r.GetUserPerviousEvents(1)).ReturnsAsync(events);

            var result = await _controller.GetUserPreviousEvents(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<EventReadDTO>>(okResult.Value);
            var singleEvent = Assert.Single(returnValue);
            Assert.Equal("Previous", singleEvent.Status);
        }

        [Fact]
        public async Task GetUserUpcomingEvents_ReturnsOk_WithEvents()
        {
            var events = new List<EventReadDTO> { new EventReadDTO { Id = 2, Title = "Upcoming Event", Status = "Upcoming" } };
            _mockUserRepository.Setup(r => r.GetUserUpcomingEvents(1)).ReturnsAsync(events);

            var result = await _controller.GetUserUpcomingEvents(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<EventReadDTO>>(okResult.Value);
            var singleEvent = Assert.Single(returnValue);
            Assert.Equal("Upcoming", singleEvent.Status);
        }

        [Fact]
        public async Task GetUserSpecialityEvents_ReturnsNotFound_WhenUserMissing()
        {
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync((UserReadDTO)null);

            var result = await _controller.GetUserSpecialityEvents(1);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetUserSpecialityEvents_ReturnsOk_WhenUserExists()
        {
            var speciality = new Speciality { Id = 10, Field = "Cardiology" };
            var user = new UserReadDTO { Id = 1, UserName = "John", Speciality = speciality.Field };
            var events = new List<EventReadDTO> { new EventReadDTO { Id = 3, Title = "Speciality Event", EventField = speciality.Field } };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync(user);
            _mockUserRepository.Setup(r => r.GetUserSpecialityEvents(user)).ReturnsAsync(events);

            var result = await _controller.GetUserSpecialityEvents(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<EventReadDTO>>(okResult.Value);
            var singleEvent = Assert.Single(returnValue);
            Assert.Equal("Cardiology", singleEvent.EventField);
        }
    }
}

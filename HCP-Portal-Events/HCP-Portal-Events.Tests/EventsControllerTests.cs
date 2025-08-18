using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HCP_Portal_Events.Controllers;
using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models.DTOs;

namespace HCP_Portal_Events.Tests
{
    public class EventsControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly EventsController _controller;

        public EventsControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEventRepository = new Mock<IEventRepository>();

            _mockUnitOfWork.Setup(u => u.EventRepository).Returns(_mockEventRepository.Object);
            _controller = new EventsController(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetPreviousEvents_ReturnsOk_WhenEventsExist()
        {
            var events = new List<EventReadDTO>
            {
                new EventReadDTO
                {
                    Id = 1,
                    Title = "Past Conference",
                    Description = "Medical past event",
                    Date = DateTime.Now.AddDays(-30),
                    Status = "Previous",
                    imageUrl = "past.jpg",
                    EventType = "CME",
                    EventField = "Cardiology"
                }
            };

            _mockEventRepository.Setup(r => r.GetAllPreviousEventsAsync())
                .ReturnsAsync(events);

            var result = await _controller.GetPreviousEvents();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEvents = Assert.IsAssignableFrom<IEnumerable<EventReadDTO>>(okResult.Value);
            Assert.Single(returnedEvents);
        }

        [Fact]
        public async Task GetPreviousEvents_ReturnsNotFound_WhenNoEvents()
        {
            _mockEventRepository.Setup(r => r.GetAllPreviousEventsAsync())
                .ReturnsAsync(new List<EventReadDTO>());

            var result = await _controller.GetPreviousEvents();

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetUpcomingEvents_ReturnsOk_WhenEventsExist()
        {
            var events = new List<EventReadDTO>
            {
                new EventReadDTO
                {
                    Id = 2,
                    Title = "Future Workshop",
                    Description = "Upcoming training event",
                    Date = DateTime.Now.AddDays(10),
                    Status = "Upcoming",
                    imageUrl = "future.jpg",
                    EventType = "Webinar",
                    EventField = "Neurology"
                }
            };

            _mockEventRepository.Setup(r => r.GetAllUpcomingEventsAsync())
                .ReturnsAsync(events);

            var result = await _controller.GetUpcomingEvents();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEvents = Assert.IsAssignableFrom<IEnumerable<EventReadDTO>>(okResult.Value);
            Assert.Single(returnedEvents);
        }

        [Fact]
        public async Task GetUpcomingEvents_ReturnsNotFound_WhenNoEvents()
        {
            _mockEventRepository.Setup(r => r.GetAllUpcomingEventsAsync())
                .ReturnsAsync(new List<EventReadDTO>());

            var result = await _controller.GetUpcomingEvents();

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetEventsByType_ReturnsBadRequest_WhenTypeIsEmpty()
        {
            var result = await _controller.GetEventsByType("");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetEventsByType_ReturnsOk_WhenEventsExist()
        {
            var events = new List<EventReadDTO>
            {
                new EventReadDTO
                {
                    Id = 3,
                    Title = "Conference on AI",
                    EventType = "CME",
                    EventField = "Technology",
                    Date = DateTime.Now,
                    Description = "AI Conference",
                    Status = "Previous",
                    imageUrl = "ai.jpg"
                }
            };

            _mockEventRepository.Setup(r => r.GetAllEventsByTypeAsync("CME"))
                .ReturnsAsync(events);

            var result = await _controller.GetEventsByType("CME");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetEventsByStatus_ReturnsBadRequest_WhenStatusIsEmpty()
        {
            var result = await _controller.GetEventsByStatus("");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetEventsByStatus_ReturnsOk_WhenEventsExist()
        {
            var events = new List<EventReadDTO>
            {
                new EventReadDTO
                {
                    Id = 4,
                    Title = "Active Event",
                    Status = "Upcoming",
                    Date = DateTime.Now,
                    EventType = "CME",
                    EventField = "Education",
                    Description = "Ongoing seminar",
                    imageUrl = "edu.jpg"
                }
            };

            _mockEventRepository.Setup(r => r.GetAllEventsByStatusAsync("Upcoming"))
                .ReturnsAsync(events);

            var result = await _controller.GetEventsByStatus("Upcoming");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }
        [Fact]
        public async Task GetEventById_ReturnsBadRequest_WhenIdInvalid()
        {
            var result = await _controller.GetEventById(0);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetEventById_ReturnsNotFound_WhenNoEvent()
        {
            _mockEventRepository.Setup(r => r.GetEventByIdAsync(10))
                .ReturnsAsync((EventReadDTO)null);

            var result = await _controller.GetEventById(10);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetEventById_ReturnsOk_WhenEventExists()
        {
            var ev = new EventReadDTO
            {
                Id = 5,
                Title = "Sample Event",
                Description = "Sample",
                Date = DateTime.Now,
                Status = "Planned",
                imageUrl = "sample.jpg",
                EventType = "Workshop",
                EventField = "Medical"
            };

            _mockEventRepository.Setup(r => r.GetEventByIdAsync(5)).ReturnsAsync(ev);

            var result = await _controller.GetEventById(5);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(ev, okResult.Value);
        }
        [Fact]
        public async Task GetEventWithActivities_ReturnsBadRequest_WhenIdInvalid()
        {
            var result = await _controller.GetEventWithActivities(0);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetEventWithActivities_ReturnsNotFound_WhenNoEvent()
        {
            _mockEventRepository.Setup(r => r.GetEventWithActivitiesAndAttachments(99))
                .ReturnsAsync((EventReadActivitesandAttachmentsDTO)null);

            var result = await _controller.GetEventWithActivities(99);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetEventWithActivities_ReturnsOk_WhenEventExists()
        {
            var ev = new EventReadActivitesandAttachmentsDTO
            {
                Id = 6,
                Title = "Medical Event with Activities",
                Description = "Event with sessions and files",
                Date = DateTime.Now,
                Status = "Active",
                imageUrl = "med.jpg",
                EventType = "Conference",
                EventField = "Medicine",
                EventActivities = new List<ActivityWithAttachmentsDTO>
                {
                    new ActivityWithAttachmentsDTO
                    {
                        Id = 1,
                        Title = "Opening Session",
                        Date = DateTime.Now,
                        Description = "Kickoff",
                        Attachments = new List<AttachmentDTO>
                        {
                            new AttachmentDTO { Id = 1, FileName = "agenda.pdf", FilePath = "/files/agenda.pdf" }
                        },
                        Speakers = new List<SpeakerDTO>
                        {
                            new SpeakerDTO { Id = 1, UserName = "Dr. Smith", Speciality = "Cardiology", ProfilePicture = "smith.jpg" }
                        }
                    }
                }
            };

            _mockEventRepository.Setup(r => r.GetEventWithActivitiesAndAttachments(6))
                .ReturnsAsync(ev);

            var result = await _controller.GetEventWithActivities(6);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(ev, okResult.Value);
        }

        [Fact]
        public async Task SearchEvents_ReturnsBadRequest_WhenNameEmpty()
        {
            var result = await _controller.SearchEvents("");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task SearchEvents_ReturnsOk_WhenFound()
        {
            var events = new List<EventReadDTO>
            {
                new EventReadDTO
                {
                    Id = 7,
                    Title = "Searchable Event",
                    Description = "Found via search",
                    Date = DateTime.Now,
                    Status = "Active",
                    imageUrl = "search.jpg",
                    EventType = "Seminar",
                    EventField = "Research"
                }
            };

            _mockEventRepository.Setup(r => r.SearchEventsByNameAsync("search"))
                .ReturnsAsync(events);

            var result = await _controller.SearchEvents("search");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task SearchEvents_ReturnsNotFound_WhenNoMatch()
        {
            _mockEventRepository.Setup(r => r.SearchEventsByNameAsync("missing"))
                .ReturnsAsync(new List<EventReadDTO>());

            var result = await _controller.SearchEvents("missing");

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}

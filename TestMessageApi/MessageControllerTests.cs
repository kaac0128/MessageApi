using DataAccess;
using Entities;
using MessageApi.Controllers;
using MessageApi.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Services;
using System;

namespace TestMessageApi
{
    public class MessageControllerTests
    {
        private readonly Mock<IMessageService> _mockMessageService;
        private readonly Mock<ILogger<MessageController>> _mockLogger;
        private readonly MessageController _controller;

        public MessageControllerTests()
        {
            // Crear el mock del servicio
            _mockMessageService = new Mock<IMessageService>();
            _mockLogger = new Mock<ILogger<MessageController>>();

            // Crear el controlador con los mocks
            _controller = new MessageController(_mockMessageService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task SendMessageUser_ValidUser_ReturnsOk()
        {
            // Arrange
            var message = new MessageEntity { Name = "Test User", PhoneNumber = "12345" };
            _mockMessageService.Setup(service => service.AddUserAsync(message))
                .ReturnsAsync((true, "Usuario creado exitosamente."));

            // Act
            var result = await _controller.SendMessageUser(message);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ResponseDataMessage>(actionResult.Value);
            Assert.True(returnValue.Success);
            Assert.Equal("Usuario creado exitosamente.", returnValue.Message);
        }

        [Fact]
        public async Task SendMessageUser_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            MessageEntity message = null;

            // Act
            var result = await _controller.SendMessageUser(message);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<ResponseDataMessage>(actionResult.Value);
            Assert.False(returnValue.Success);
            Assert.Equal("Datos del usuario inválidos.", returnValue.Message);
        }

        [Fact]
        public async Task SendMessageUser_ServiceFailure_ReturnsInternalServerError()
        {
            // Arrange
            var message = new MessageEntity { Name = "Test User", PhoneNumber = "12345" };
            _mockMessageService.Setup(service => service.AddUserAsync(message))
                .ReturnsAsync((false, "Error al crear el usuario."));

            // Act
            var result = await _controller.SendMessageUser(message);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);  // StatusCode(500)
            Assert.Equal(500, actionResult.StatusCode);
            var returnValue = Assert.IsType<ResponseDataMessage>(actionResult.Value);
            Assert.False(returnValue.Success);
            Assert.Equal("Error al crear el usuario.", returnValue.Message);
        }
    }
}
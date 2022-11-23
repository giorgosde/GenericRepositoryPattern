using AutoMapper;
using FluentAssertions;
using GenericRepository.Api.Controllers;
using GenericRepository.Api.Models;
using GenericRepository.Dal;
using GenericRepository.Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GenericRepository.Api.Tests
{
    public class VehicleControllerTests
    {
        private readonly VehicleController _controller;
        private readonly Mock<IVehicleRepository> _repositoryMock;
        private readonly IMapper _mapper;

        public VehicleControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new VehicleProfile()));
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _repositoryMock = new Mock<IVehicleRepository>(MockBehavior.Strict);
            _controller = new VehicleController(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkObject()
        {
            _repositoryMock
                .Setup(r => r.AllAsync())
                .ReturnsAsync(new List<Vehicle>());

            var response = await _controller.Get();
            
            response.Should().BeOfType<OkObjectResult>();
            response.As<OkObjectResult>().Value.Should().BeOfType<List<VehicleDto>>();
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsOkObject()
        {
            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new Vehicle());

            var response = await _controller.GetById(It.IsAny<string>());
            
            response.Should().BeOfType<OkObjectResult>();
            response.As<OkObjectResult>().Value.Should().BeOfType<VehicleDto>();
        }

        [Fact]
        public async Task GetById_WhenCalledAndNoResults_ReturnsNotFoundObject()
        {
            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(null as Vehicle);

            var response = await _controller.GetById(It.IsAny<string>());
            
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Add_WhenCalled_ReturnsOkObject()
        {
            _repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<Vehicle>()))
                .ReturnsAsync(new Vehicle());

            var response = await _controller.Add(new VehicleDto());
            
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Delete_WhenSuccessful_ReturnsOkObject()
        {
            _repositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<string>());

            var response = await _controller.Delete(It.IsAny<string>());
            
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Delete_WhenRepositoryThowsException_ReturnsBadRequestObject()
        {
            _repositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<string>()))
                .Throws<ArgumentNullException>();

            var response = await _controller.Delete(It.IsAny<string>());
            
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Update_WhenSuccessful_ReturnsOkObject()
        {
            var vehicleId = Guid.NewGuid().ToString();

            var mockVehicle = new VehicleDto
            {
                Id = vehicleId
            };

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Vehicle>()))
                .ReturnsAsync(new Vehicle());

            var response = await _controller.Update(vehicleId, mockVehicle);
            
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Update_WhenCalledWithNoMaychingIds_ReturnsBadRequest()
        {
            var mockVehicle = new VehicleDto
            {
                Id = Guid.NewGuid().ToString()
            };

            _repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Vehicle>()))
                .ReturnsAsync(new Vehicle());

            var response = await _controller.Update(Guid.NewGuid().ToString(), mockVehicle);
            
            response.Should().BeOfType<BadRequestObjectResult>();
        }

    }
}

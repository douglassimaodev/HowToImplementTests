using FluentAssertions;
using HowToImplementTests.Api.Controllers;
using HowToImplementTests.Api.DAL;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace HowToImplementTests.UnitTesting.Calculators
{
    public class CalculatorControllerTest
    {
        [Fact]
        public void Add_ShouldReturnOkResult()
        {
            // Arrange
            var calculator = Substitute.For<ICalculatorModel>();
            calculator.Add(5, 3).Returns(8);

            var controller = new CalculatorController(calculator);

            // Act
            var sut = controller.Add(5, 3);

            // Assert
            sut.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<double>()
            .Which.Should().Be(8);
        }

        [Fact]
        public void Subtract_ShouldReturnOkResult()
        {
            // Arrange
            var calculator = Substitute.For<ICalculatorModel>();
            calculator.Subtract(5, 3).Returns(2);

            var controller = new CalculatorController(calculator);

            // Act
            var sut = controller.Subtract(5, 3);

            // Assert
            sut.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<double>()
            .Which.Should().Be(2);
        }

        [Fact]
        public void Multiply_ShouldReturnOkResult()
        {
            // Arrange
            var calculator = Substitute.For<ICalculatorModel>();
            calculator.Multiply(5, 3).Returns(15);

            var controller = new CalculatorController(calculator);

            // Act
            var sut = controller.Multiply(5, 3);

            // Assert
            sut.Result.Should().BeOfType<OkObjectResult>()
             .Which.Value.Should().BeOfType<double>()
             .Which.Should().Be(15);
        }

        [Fact]
        public void Divide_ShouldReturnOkResult()
        {
            // Arrange
            var calculator = Substitute.For<ICalculatorModel>();
            calculator.Divide(10, 2).Returns(5);

            var controller = new CalculatorController(calculator);

            // Act
            var sut = controller.Divide(10, 2);

            // Assert
            sut.Result.Should().BeOfType<OkObjectResult>()
           .Which.Value.Should().BeOfType<double>()
           .Which.Should().Be(5);
        }

        [Fact]
        public void Divide_ShouldReturnBadResult_WhenDividingByZero()
        {
            // Arrange
            var calculator = Substitute.For<ICalculatorModel>();
            calculator.Divide(10, 0).Returns(_ => throw new DivideByZeroException("Cannot divide by zero"));

            var controller = new CalculatorController(calculator);

            // Act
            var sut = controller.Divide(10, 0);

            // Assert
            sut.Result.Should().BeOfType<BadRequestObjectResult>()
           .Which.Value.Should().BeOfType<string>()
           .Which.Should().Be("Cannot divide by zero");
        }
    }
}

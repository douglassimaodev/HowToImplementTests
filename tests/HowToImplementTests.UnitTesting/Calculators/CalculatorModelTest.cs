using FluentAssertions;
using HowToImplementTests.Api.Models;

namespace HowToImplementTests.UnitTesting.Calculators
{
    public class CalculatorModelTest
    {
        [Fact]
        public void Add_ShouldReturnCorrectResult()
        {
            // Arrange
            var calculator = new CalculatorModel();

            // Act
            double result = calculator.Add(5, 3);

            // Assert
            result.Should().Be(8); // Ensure the result is as expected
        }

        [Fact]
        public void Subtract_ShouldReturnCorrectResult()
        {
            // Arrange
            var calculator = new CalculatorModel();

            // Act
            double result = calculator.Subtract(5, 3);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void Multiply_ShouldReturnCorrectResult()
        {
            // Arrange
            var calculator = new CalculatorModel();

            // Act
            double result = calculator.Multiply(5, 3);

            // Assert
            result.Should().Be(15);
        }

        [Fact]
        public void Divide_ShouldReturnCorrectResult()
        {
            // Arrange
            var calculator = new CalculatorModel();

            // Act
            double result = calculator.Divide(6, 3);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void Divide_ShouldThrowExceptionForZeroDenominator()
        {
            // Arrange
            var calculator = new CalculatorModel();

            // Act & Assert
            calculator.Invoking(c => c.Divide(5, 0))
                .Should().Throw<DivideByZeroException>();
        }
    }
}

using FluentAssertions;
using HowToImplementTests.Api.DAL;

namespace HowToImplementTests.UnitTesting.Calculators
{
    public class CalculatorModelTest
    {
        private readonly CalculatorModel _sut = new();

        [Theory]
        [InlineData(15, 5, 20)]
        [InlineData(0, 0, 0)]
        [InlineData(-30, -30, -60)]
        [InlineData(-5, 15, 10)]
        [InlineData(-1, -1, -1, Skip = "This is not implemented yet")]
        public void Add_ShouldReturnCorrectResult(double a, double b, double expected)
        {
            // Arrange
            //var calculator = new CalculatorModel();

            // Act
            double result = _sut.Add(a, b);

            // Assert
            result.Should().Be(expected);
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
            // Act
            double result = _sut.Multiply(5, 3);

            // Assert
            result.Should().Be(15);
        }

        [Fact]
        public void Divide_ShouldReturnCorrectResult()
        {
            // Act
            double result = _sut.Divide(6, 3);

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
                .Should().Throw<DivideByZeroException>()
                .WithMessage("*Cannot divide by zero*");
        }

        [Fact]
        public void Demo_ShouldAccessAnInternalProperty()
        {
            // Arrange
            var calculator = new CalculatorModel();

            // Act & Assert
            calculator.InternalProperty.Should().Be("Demo");

            //Check HowToImplementTests.Api.csproj file
        }
    }
}

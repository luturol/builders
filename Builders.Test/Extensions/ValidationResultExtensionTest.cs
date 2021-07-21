using System.Net;
using Builders.Extensions;
using Builders.Validations;
using FluentValidation.Results;
using Xunit;

namespace Builders.Test.Extensions
{
    public class ValidationResultExtensionTest
    {
        [Fact]
        public void ShoudlBeAbleToParseValidationResultToProblemDetails()
        {
            #region Arrange
            var invalidId = "asdsadas";
            var validations = new ObjectIdValidation().Validate(invalidId);
            #endregion Arrange

            #region Act
            var actualProblemDetails = validations.ToProblemDetails(HttpStatusCode.BadRequest);
            #endregion Act

            #region Assert
            Assert.NotNull(actualProblemDetails);
            Assert.Equal("Invalid parameters", actualProblemDetails.Title);
            Assert.Equal(validations.Errors.Count, actualProblemDetails.Extensions.Count);
            #endregion Assert
        }

        [Fact]
        public void ShouldNotHaveErrorsGivingEmptyValidationResults()
        {
            #region Arrange
            var validations = new ValidationResult();
            var expectedErrorsCount = 0;
            #endregion Arrange

            #region Act
            var actualProblemDetails = validations.ToProblemDetails(HttpStatusCode.BadRequest);
            #endregion Act

            #region Assert
            Assert.NotNull(actualProblemDetails);
            Assert.Equal("Invalid parameters", actualProblemDetails.Title);
            Assert.Equal(validations.Errors.Count, actualProblemDetails.Extensions.Count);
            Assert.Equal(expectedErrorsCount, actualProblemDetails.Extensions.Count);
            #endregion Assert
        }
    }
}
using Builders.Validations;
using MongoDB.Bson;
using Xunit;

namespace Builders.Test.Validatios
{
    public class ObjectIdValidationTest
    {
        [Fact]
        public void ShouldBeAbleToReturnTrueGivingValidObjectId()
        {
            #region Arrange
            var validId = new ObjectId().ToString();
            var validation = new ObjectIdValidation();
            var expectedErrorsCount = 0;
            #endregion Arrange

            #region Act
            var actualResults = validation.Validate(validId);
            #endregion Act

            #region Assert
            Assert.True(actualResults.IsValid);
            Assert.Equal(expectedErrorsCount, actualResults.Errors.Count);
            #endregion Assert
        }

        [Fact]
        public void ShouldBeAbleToReturnFalseGivingInvalidObjectId()
        {
            #region Arrange
            var validId = "new ObjectId().ToString()";
            var validation = new ObjectIdValidation();
            var expectedErrorsCount = 1;
            #endregion Arrange

            #region Act
            var actualResults = validation.Validate(validId);
            #endregion Act

            #region Assert
            Assert.False(actualResults.IsValid);
            Assert.Equal(expectedErrorsCount, actualResults.Errors.Count);
            #endregion Assert
        }
    }
}
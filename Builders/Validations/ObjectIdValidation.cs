using FluentValidation;
using MongoDB.Bson;

namespace Builders.Validations
{
    public class ObjectIdValidation : AbstractValidator<string>
    {
        public ObjectIdValidation()
        {
            RuleFor(id => id)
                .Must(IsIdAnObjectId)
                .OverridePropertyName("id")
                .WithMessage("Invalid value for id");
        }

        private bool IsIdAnObjectId(string id)
        {
            bool isValid = ObjectId.TryParse(id, out var objectId);            
            return isValid;
        }
    }
}
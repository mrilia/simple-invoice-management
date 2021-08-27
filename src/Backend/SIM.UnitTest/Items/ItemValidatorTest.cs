using FluentValidation.TestHelper;
using SIM.Application.Handlers.Items.Command.AddNew;
using SIM.Application.Interfaces;
using SIM.Application.Common.Validator.Items;
using SIM.Persistence.Context;
using Xunit;

namespace SepidStar.UnitTest.Door
{
    public class ItemValidatorTest
    {
        private readonly AddNewItemCommandValidator _validator;

        public ItemValidatorTest()
        {
            ISIMContext context = new SIMContext();
            _validator = new AddNewItemCommandValidator(context);
        }

        [Fact]
        public void When_Name_Is_Empty_ValidationFailed()
        {
            var model = new AddNewItemCommand();

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JumpApp.Models;
namespace JumpApp.Services
{
    public class UserInfoValidator : AbstractValidator<CoreUserInfo>
    {
        public UserInfoValidator()
        {
            RuleFor(x => x.Dob).NotNull().WithMessage("Please select your date of birth");
            RuleFor(x => x.Height).NotNull().WithMessage("Please select your height");
            RuleFor(x => x.Weight).NotNull().WithMessage("Please select your weight");
        }
    }
}

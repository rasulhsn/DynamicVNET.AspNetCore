using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicVNET.Lib.AspNetCore
{
    public class ProfileValidatorFilter : IActionFilter
    {
        private DynamicVNET.Lib.ProfileValidator _profileValidator;
        public ProfileValidatorFilter(DynamicVNET.Lib.ProfileValidator validator)
        {
            this._profileValidator = validator;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ActionArguments != null)
                foreach (var item in context.ActionArguments)
                {
                    try
                    {
                        string profileName = GetBindedProfile(context.ActionDescriptor, item.Key);
                        if (string.IsNullOrEmpty(profileName))
                            continue;
                        IEnumerable<ValidationMarkerResult> results = this._profileValidator.Validate(profileName, item.Value);
                        AddModelError(results, context);
                    }
                    catch (InvalidProfileNameException)
                    {
                        context.Result = new StatusCodeResult(404);
                        break;
                    }
                    catch (ValidatorException)
                    { }
                    catch (Exception)
                    { }
                }


        }

        private string GetBindedProfile(ActionDescriptor descriptor,string argumentName)
        {
            var parameter = descriptor.Parameters.SingleOrDefault(x => x.Name == argumentName) as ControllerParameterDescriptor;
            if (parameter != null)
                return parameter
                        .ParameterInfo
                        .CustomAttributes
                        .SingleOrDefault(x => x.AttributeType == typeof(ProfileBind))?
                        .ConstructorArguments[0].Value.ToString();
            return null;
        }

        private void AddModelError(IEnumerable<ValidationMarkerResult> results, ActionExecutingContext context)
        {
            if (results != null)
            {
                foreach (var item in results)
                {
                    context.ModelState.AddModelError(item.MemberName ?? "", item.ErrorMessage);
                }
            }
        }
    }
}

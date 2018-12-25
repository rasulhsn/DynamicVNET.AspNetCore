using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DynamicVNET.Lib.AspNetCore
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class ProfileVNET : Attribute , IFilterFactory
    {
        public bool IsReusable => false;
        
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            DynamicVNET.Lib.ProfileValidator instance = serviceProvider.GetService(typeof(DynamicVNET.Lib.ProfileValidator)) as DynamicVNET.Lib.ProfileValidator;
            return new ProfileValidatorFilter(instance);
        }
    }
}

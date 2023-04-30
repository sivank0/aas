#region

using System.Net;
using AAS.BackOffice.Infrastructure;
using AAS.Domain.AccessPolicies;
using AAS.Domain.Services;
using AAS.Domain.Users.SystemUsers;
using AAS.Tools.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace AAS.BackOffice.Filters;

[AttributeUsage(AttributeTargets.Method)]
internal class IsAuthorizedAttribute : ActionFilterAttribute
{
    public AccessPolicy[] AccessPolicies;

    internal IsAuthorizedAttribute(params AccessPolicy[] policies)
    {
        AccessPolicies = policies;
    }

    public void OnActionExecuting(ActionExecutingContext context, IUsersService usersService)
    {
        try
        {
            IRequestCookieCollection cookies = context.HttpContext.Request.Cookies;

            if (!cookies.ContainsKey(CookieNames.Token))
                throw new UnauthenticatedException();

            string? token = cookies[CookieNames.Token];
            if (token is null) throw new UnauthenticatedException();

            SystemUser? systemUser = usersService.GetSystemUser(token);
            if (systemUser is null) throw new UnauthenticatedException();
            if (!IsAuthorized(systemUser)) throw new UnauthorizedException();

            context.HttpContext.Items[CookieNames.SystemUser] = systemUser;
        }
        catch (Exception exception)
        {
            if (exception is UnauthenticatedException)
                SetUnauthenticated(context);

            else if (exception is UnauthorizedException)
                SetUnauthorized(context);

            else throw;
        }
    }

    private bool IsAuthorized(SystemUser systemUser)
    {
        if (AccessPolicies.Length == 0) return true;

        return AccessPolicies.Any(policy => systemUser.HasAccess(policy));
    }

    private void SetUnauthenticated(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.IsAjaxRequest())
        {
            ClearAjaxRequest(context);
            return;
        }

        context.HttpContext.Response.Cookies.Delete(CookieNames.Token);
        context.Result = new RedirectResult("/authentication");
    }

    private void SetUnauthorized(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.IsAjaxRequest()) ClearAjaxRequest(context);
        else context.Result = new RedirectResult("/error/403");
    }

    private void ClearAjaxRequest(ActionExecutingContext context)
    {
        context.Result = new EmptyResult();
        context.HttpContext.Response.Clear();
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
    }

    private class UnauthenticatedException : Exception
    {
    }

    private class UnauthorizedException : Exception
    {
    }
}

public class IsAuthorizedFilter : IActionFilter
{
    private readonly IUsersService _usersService;

    public IsAuthorizedFilter(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        IsAuthorizedAttribute? authAttribute = context.ActionDescriptor.FilterDescriptors
            .OrderByDescending(d => d.Scope)
            .Select(d => d.Filter)
            .OfType<IsAuthorizedAttribute>()
            .FirstOrDefault();

        authAttribute?.OnActionExecuting(context, _usersService);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // no implementation
    }
}
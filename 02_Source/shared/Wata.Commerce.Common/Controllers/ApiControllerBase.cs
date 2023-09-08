using Microsoft.AspNetCore.Mvc;

namespace Wata.Commerce.Common.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected async Task<string?> GetUserID()
        {
            return await Task.Run<string?>(() =>
            {
                if (Request != null && Request.Headers["UserID"].Any())
                {
                    return Request.Headers["UserID"];
                }

                return null;
            });
        }
    }
}
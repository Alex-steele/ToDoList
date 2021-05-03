using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data.Repositories.Interfaces;

namespace ToDoList.WebAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                var userRepo = context.RequestServices.GetService<IUserRepository>();
                var sha256Generator = context.RequestServices.GetService<ISha256Generator>();

                var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();

                var encoding = Encoding.GetEncoding("iso-8859-1");
                var usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var separatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, separatorIndex);
                var rawPassword = usernamePassword.Substring(separatorIndex + 1);
                var hashedPassword = sha256Generator.ComputeSha256Hash(rawPassword);

                var userExists = await userRepo.UserExists(username, hashedPassword);

                if (userExists)
                {
                    await next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}

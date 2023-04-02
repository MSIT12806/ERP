namespace App_NET6.Controllers
{
    public class SpaMiddleware
    {
        private readonly RequestDelegate _next;

        public SpaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
            {
                context.Request.Path = "/index.html";
                await _next(context);
            }
        }
    }
}

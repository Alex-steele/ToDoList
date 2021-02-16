using Microsoft.AspNetCore.Mvc;

namespace ToDoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        protected IActionResult Error() => Problem();
    }
}

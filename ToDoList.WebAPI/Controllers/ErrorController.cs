using Microsoft.AspNetCore.Mvc;

namespace ToDoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}

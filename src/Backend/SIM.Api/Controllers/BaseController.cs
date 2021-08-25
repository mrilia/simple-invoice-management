using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SIM.Api.Controllers
{
    [Route("[Controller]")]
    [EnableCors]
    [ApiController]
    public class BaseController : Controller
    {
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<string> TestNoRoleEndpoint()
        {
            return "Accediste al endpoint protegido general(nivel de cliente)";
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public ActionResult<string> TestEmployeeEndpoint()
        {
            return "Accediste al endpoint de nivel de empleado";
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> TestAdminEndpoint()
        {
            return "Accediste al endpoint de nivel de administrador";
        }
    }
}

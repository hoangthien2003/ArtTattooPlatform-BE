using back_end.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controller
{
    [Route("api/[controller]")]
    [Authorize(Roles = "MN")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private TattooPlatformEndContext _context;
        public ManagerController(TattooPlatformEndContext context)
        {
            _context = context;
        }
    }
}

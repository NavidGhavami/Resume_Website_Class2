using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Interface;

namespace ServiceHost.Controllers
{
    public class ProjectController : SiteBaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("projects")]
        public async Task<IActionResult> Projects()
        {
            var project = await _projectService.GetAllProjects();
            return View(project);
        }
    }
}

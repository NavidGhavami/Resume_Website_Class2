using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.Project;
using Resume.Application.Dtos.User;
using Resume.Application.Services.Implementation;
using Resume.Application.Services.Interface;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class ProjectController : AdminBaseController
    {
        #region Fields

        private readonly IProjectService _projectService;



        #endregion

        #region Constructor


        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        #endregion


        #region Actions

        [HttpGet("project-list")]
        public async Task<IActionResult> FilterProject()
        {
            var project = await _projectService.GetAllProjects();
            return View(project);
        }

        [HttpGet("create-project")]
        public IActionResult CreateProject()
        {
            return View();
        }


        [HttpPost("create-project"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(CreateProjectDto project, IFormFile ProjectImage)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(user);
            //}

            var result = await _projectService.CreateProject(project, ProjectImage);

            switch (result)
            {
                case CreateProjectResult.Success:
                    return RedirectToAction("FilterProject", "Project", new { area = "Administration" });

               
                case CreateProjectResult.Error:
                    TempData["ErrorMessage"] = ErrorMessage;
                    break;
            }

            return View(project);
        }

        #endregion
    }
}

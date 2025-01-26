using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.Education;
using Resume.Application.Services.Interface;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class ResumeController : AdminBaseController
    {
        #region Fields

        private readonly IEducationService _educationService;



        #endregion

        #region Constructor

        public ResumeController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        #endregion

        #region Actions

        #region Education

        #region Get All Educations Item

        [HttpGet("education-list")]
        public async Task<IActionResult> FilterEducations()
        {
            var education = await _educationService.GetAllEducations();
            return View(education);
        }

        #endregion

        #region Create Education

        [HttpGet("create-education")]
        public IActionResult CreateEducation()
        {
            return View();
        }


        [HttpPost("create-education"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEducation(CreateEducationDto education)
        {
            if (!ModelState.IsValid)
            {
                return View(education);
            }

            var result = await _educationService.CreateEducation(education);

            switch (result)
            {
                case CreateEducationResult.Success:
                    return RedirectToAction("FilterEducations", "Resume", new { area = "Administration" });
                case CreateEducationResult.Error:
                    TempData["ErrorMessage"] = ErrorMessage;
                    break;
            }

            return View(education);
        }

        #endregion

        #endregion

        #endregion
    }
}

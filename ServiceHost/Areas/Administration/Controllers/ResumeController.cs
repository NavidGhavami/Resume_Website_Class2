using Microsoft.AspNetCore.Mvc;
using Resume.Application.Dtos.Education;
using Resume.Application.Dtos.Experience;
using Resume.Application.Services.Interface;
using Resume.Domain.Entities.Skills;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class ResumeController : AdminBaseController
    {
        #region Fields

        private readonly IEducationService _educationService;
        private readonly IExperienceService _experienceService;
        private readonly ISkillService _skillService;



        #endregion

        #region Constructor

        public ResumeController(IEducationService educationService, IExperienceService experienceService, ISkillService skillService)
        {
            _educationService = educationService;
            _experienceService = experienceService;
            _skillService = skillService;
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

        #region Experience

        #region Get All Experience Item

        [HttpGet("experience-list")]
        public async Task<IActionResult> FilterExperience()
        {
            var experience = await _experienceService.GetAllExperience();
            return View(experience);
        }

        #endregion

        #region Create Experienece

        [HttpGet("create-experience")]
        public IActionResult CreateExperience()
        {
            return View();
        }


        [HttpPost("create-experience"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExperience(CreateExperienceDto experience)
        {
            if (!ModelState.IsValid)
            {
                return View(experience);
            }

            var result = await _experienceService.CreateExperience(experience);

            switch (result)
            {
                case CreateExperienceResult.Success:
                    return RedirectToAction("FilterExperience", "Resume", new { area = "Administration" });
                case CreateExperienceResult.Error:
                    TempData["ErrorMessage"] = ErrorMessage;
                    break;
            }

            return View(experience);
        }

        #endregion

        #endregion

        #region Skills

        #region Get All Skills Item

        [HttpGet("skills-list")]
        public async Task<IActionResult> FilterSkills()
        {
            var skills = await _skillService.GetAllSkills();
            return View(skills);
        }

        #endregion

        #region Create Experienece

        [HttpGet("create-skill")]
        public IActionResult CreateSkill()
        {
            return View();
        }


        [HttpPost("create-skill"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSkill(CreateSkillDto skill)
        {
            if (!ModelState.IsValid)
            {
                return View(skill);
            }

            var result = await _skillService.CreateSkill(skill);

            switch (result)
            {
                case CreateSkillDto.CreateSkillResult.Success:
                    return RedirectToAction("FilterSkills", "Resume", new { area = "Administration" });
                case CreateSkillDto.CreateSkillResult.Error:
                    TempData["ErrorMessage"] = ErrorMessage;
                    break;
            }

            return View(skill);
        }

        #endregion

        #endregion

        #endregion
    }
}

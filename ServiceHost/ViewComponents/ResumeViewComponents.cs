using Microsoft.AspNetCore.Mvc;
using Resume.Application.Services.Implementation;
using Resume.Application.Services.Interface;

namespace ServiceHost.ViewComponents
{
    #region Education

    public class EducationViewComponent : ViewComponent
    {

        private readonly IEducationService _educationService;

        public EducationViewComponent(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var education = await _educationService.GetAllEducations();
            return View("Education", education);
        }

    }

    #endregion

    #region Experience

    public class ExperienceViewComponent : ViewComponent
    {

        private readonly IExperienceService _experienceService;

        public ExperienceViewComponent(IExperienceService experienceService)
        {
            _experienceService = experienceService;
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var experience = await _experienceService.GetAllExperience();
            return View("Experience", experience);
        }

    }

    #endregion

    #region Skills

    #region Experience

    public class SkillViewComponent : ViewComponent
    {

        private readonly ISkillService _skillService;

        public SkillViewComponent(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var skill = await _skillService.GetAllSkills();
            return View("Skill", skill);
        }

    }


    

    #endregion

    #endregion
}

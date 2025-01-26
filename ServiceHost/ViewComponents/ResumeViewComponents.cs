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
}

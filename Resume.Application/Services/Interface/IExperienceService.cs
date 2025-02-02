using Resume.Application.Dtos.Experience;

namespace Resume.Application.Services.Interface;

public interface IExperienceService : IAsyncDisposable
{
    Task<List<FilterExperienceDto>> GetAllExperience();
    Task<CreateExperienceResult> CreateExperience(CreateExperienceDto command);
}
using Resume.Application.Dtos.Education;

namespace Resume.Application.Services.Interface;

public interface IEducationService : IAsyncDisposable
{
    Task<List<FilterEducationDto>> GetAllEducations();
    Task<CreateEducationResult> CreateEducation(CreateEducationDto command);
}
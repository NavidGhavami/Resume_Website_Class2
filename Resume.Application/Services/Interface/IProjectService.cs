using Microsoft.AspNetCore.Http;
using Resume.Application.Dtos.Project;

namespace Resume.Application.Services.Interface;

public interface IProjectService : IAsyncDisposable
{
    Task<List<FilterProjectDto>> GetAllProjects();
    Task<CreateProjectResult> CreateProject(CreateProjectDto command, IFormFile projectImage);
}
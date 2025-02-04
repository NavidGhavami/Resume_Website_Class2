using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Project;
using Resume.Application.Services.Interface;
using Resume.Application.Utilities;
using Resume.Domain.Entities.Project;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation
{
    public class ProjectService : IProjectService
    {

        private readonly IGenericRepository<Project> _projectRepository;

        public ProjectService(IGenericRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }


        public async Task<List<FilterProjectDto>> GetAllProjects()
        {
            return await _projectRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterProjectDto
                {
                    Id = x.Id,
                    ProjectTitle = x.ProjectTitle,
                    Desription = x.Description,
                    ProjectImage = x.ProjectImage,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                }).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<CreateProjectResult> CreateProject(CreateProjectDto command, IFormFile projectImage)
        {
            var uploadFolder = Path.Combine("wwwroot/uploads");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(projectImage.FileName);
            var filePath = Path.Combine("wwwroot/uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await projectImage.CopyToAsync(stream);

                var newProject = new Project(command.ProjectTitle, command.Description, fileName);

                await _projectRepository.AddEntity(newProject);
                await _projectRepository.SaveChanges();

                return CreateProjectResult.Success;
            }


        }

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            await _projectRepository.DisposeAsync();
        }

        #endregion
    }
}

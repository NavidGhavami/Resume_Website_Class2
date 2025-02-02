using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Experience;
using Resume.Application.Services.Interface;
using Resume.Application.Utilities;
using Resume.Domain.Entities.Experience;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation;

public class ExperienceService : IExperienceService
{

    #region Fields

    private readonly IGenericRepository<Experience> _experienceRepository;



    #endregion


    #region Constructor

    public ExperienceService(IGenericRepository<Experience> experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }

    #endregion

    #region Filter Experience

    public async Task<List<FilterExperienceDto>> GetAllExperience()
    {
        var exp = await _experienceRepository
            .GetQuery()
            .AsQueryable()
            .Where(x => !x.IsDelete)
            .Select(x => new FilterExperienceDto
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                JobStartDate = x.JobStartDate,
                JobEndDate = x.JobEndDate,
                Description = x.Description,
                CreateDate = x.CreateDate.ToStringShamsiDate()

            }).OrderByDescending(x => x.Id).ToListAsync();

        return exp;
    }



    #endregion

    #region Create Experience

    public async Task<CreateExperienceResult> CreateExperience(CreateExperienceDto command)
    {
        var exp = new Experience
        {
            CompanyName = command.CompanyName,
            JobStartDate = command.JobStartDate,
            JobEndDate = command.JobEndDate,
            Description = command.Description,

        };

        await _experienceRepository.AddEntity(exp);
        await _experienceRepository.SaveChanges();

        return CreateExperienceResult.Success;
    }

    #endregion




    #region Dispose

    public async ValueTask DisposeAsync()
    {
        if (_experienceRepository != null)
        {
            await _experienceRepository.DisposeAsync();
        }
    }

    #endregion


}
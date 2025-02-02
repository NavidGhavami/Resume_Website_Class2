using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Skills;
using Resume.Application.Services.Interface;
using Resume.Application.Utilities;
using Resume.Domain.Entities.Skills;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation
{
    public class SkillService : ISkillService
    {

        #region Fields

        private readonly IGenericRepository<Skills> _skillsRepository;



        #endregion


        #region Constructor

        public SkillService(IGenericRepository<Skills> skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        #endregion



        public async Task<List<FilterSkillDto>> GetAllSkills()
        {
            return await _skillsRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterSkillDto
                {
                    Id = x.Id,
                    SkillTitle = x.SkillTitle,
                    SkillPercent = x.SkillPercent,
                    CreateDate = x.CreateDate.ToStringShamsiDate()

                }).OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<CreateSkillDto.CreateSkillResult> CreateSkill(CreateSkillDto command)
        {
            var newSkill = new Skills
            {
                SkillTitle = command.SkillTitle,
                SkillPercent = command.SkillPercent,
            };

            await _skillsRepository.AddEntity(newSkill);
            await _skillsRepository.SaveChanges();

            return CreateSkillDto.CreateSkillResult.Success;
        }



        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_skillsRepository != null)
            {
                await _skillsRepository.DisposeAsync();
            }
        }

        #endregion


    }
}

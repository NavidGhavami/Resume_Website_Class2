using Resume.Application.Dtos.Skills;
using Resume.Domain.Entities.Skills;

namespace Resume.Application.Services.Interface;

public interface ISkillService : IAsyncDisposable
{
    Task<List<FilterSkillDto>> GetAllSkills();
    Task<CreateSkillDto.CreateSkillResult> CreateSkill(CreateSkillDto command);
}
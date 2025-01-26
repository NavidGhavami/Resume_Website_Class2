using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Education;
using Resume.Application.Dtos.User;
using Resume.Application.Services.Interface;
using Resume.Application.Utilities;
using Resume.Domain.Entities.Education;
using Resume.Domain.Entities.User;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation
{
    public class EducationService : IEducationService
    {
        #region Fields

        private readonly IGenericRepository<Education> _educationRepository;



        #endregion


        #region Constructor

        public EducationService(IGenericRepository<Education> educationRepository)
        {
            _educationRepository = educationRepository;
        }

        #endregion

        #region Filter Education

        public async Task<List<FilterEducationDto>> GetAllEducations()
        {
            return await _educationRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterEducationDto
                {
                    Id = x.Id,
                    UniversityName = x.UniversityName,
                    EducationYear = x.EducationYear,
                    Description = x.Description,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                })
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }


        #endregion

        #region Create Education

        public async Task<CreateEducationResult> CreateEducation(CreateEducationDto command)
        {
            
                var newEducation = new Education
                {
                    UniversityName = command.UniversityName,
                    EducationYear = command.EducationYear,
                    Description = command.Description,
                };

                await _educationRepository.AddEntity(newEducation);
                await _educationRepository.SaveChanges();

                return CreateEducationResult.Success;

        }


        #endregion

        #region Edit Education

        /// <summary>
        /// Implement Edit Method For Education 
        /// </summary>
        /// <returns></returns>

        #endregion




        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_educationRepository != null)
            {
                _educationRepository.DisposeAsync();
            }
        }

        

        #endregion
    }
}

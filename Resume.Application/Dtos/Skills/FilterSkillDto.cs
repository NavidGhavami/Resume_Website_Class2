using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.Dtos.Skills
{
    public class FilterSkillDto
    {
        #region Properties

        public long Id { get; set; }
        public string SkillTitle { get; set; }
        public string SkillPercent { get; set; }
        public string CreateDate { get; set; }
        #endregion
    }
}

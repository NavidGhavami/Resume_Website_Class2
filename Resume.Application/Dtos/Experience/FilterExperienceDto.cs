namespace Resume.Application.Dtos.Experience
{
    public class FilterExperienceDto
    {
        #region Properties

        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string JobStartDate { get; set; }
        public string JobEndDate { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }


        #endregion
    }
}

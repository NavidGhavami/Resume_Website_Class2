namespace Resume.Application.Dtos.User
{
    public class EditUserDto : CreateUserDto
    {
        public long Id { get; set; }
    }


    public enum EditUserResult
    {
        Success,
        Error,
        NotFound
    }

}

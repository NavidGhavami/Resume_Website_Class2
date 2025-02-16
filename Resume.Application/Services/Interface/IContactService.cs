using Resume.Application.Dtos.Contact;

namespace Resume.Application.Services.Interface;

public interface IContactService : IAsyncDisposable
{
	Task<List<FilterContactDto>> GetAllMessages();
	Task<CreateContactResult> SendNewMessage(CreateContactDto command);
}
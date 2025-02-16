

using Microsoft.EntityFrameworkCore;
using Resume.Application.Dtos.Contact;
using Resume.Application.Services.Interface;
using Resume.Application.Utilities;
using Resume.Domain.Entities.Contact;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation
{
	public class ContactService : IContactService
	{
		private readonly IGenericRepository<Contact> _contactRepository;

		public ContactService(IGenericRepository<Contact> contactRepository)
		{
			_contactRepository = contactRepository;
		}


		public async Task<List<FilterContactDto>> GetAllMessages()
		{
			return await _contactRepository
				.GetQuery()
				.AsQueryable()
				.Where(x => !x.IsDelete)
				.Select(x => new FilterContactDto
				{
					Id = x.Id,
					Fullname = x.Fullname,
					Email = x.Email,
					Message = x.Message,
					CreateDate = x.CreateDate.ToStringShamsiDate()
				}).OrderByDescending(x => x.Id).ToListAsync();
		}
		public async Task<CreateContactResult> SendNewMessage(CreateContactDto command)
		{
			var newMessage = new Contact
			{
				MapAddress = command.MapAddress,
				Fullname = command.Fullname,
				Email = command.Email,
				Message = command.Message,

			};

			await _contactRepository.AddEntity(newMessage);
			await _contactRepository.SaveChanges();

			return CreateContactResult.Success;
		}


		#region Dispose

		public async ValueTask DisposeAsync()
		{
			if (_contactRepository != null)
			{
				await _contactRepository.DisposeAsync();
			}
			
		}

		#endregion

		
	}
}

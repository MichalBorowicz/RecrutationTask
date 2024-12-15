using CardOperationSerice.Models;

namespace CardOperationSerice.Services.Interfaces
{
	public interface ICardService
	{
		Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
	}
}

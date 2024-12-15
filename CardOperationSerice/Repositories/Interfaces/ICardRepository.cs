using CardOperationSerice.Models;

namespace CardOperationSerice.Repositories.Interfaces
{
	public interface ICardRepository
	{
		Task<Dictionary<string, CardDetails>> GetCardsForUser(string userId);
	}
}

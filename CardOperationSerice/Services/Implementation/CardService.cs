using CardOperationSerice.Models;
using CardOperationSerice.Repositories.Interfaces;
using CardOperationSerice.Services.Interfaces;
using Microsoft.VisualBasic;

namespace CardOperationSerice.Services.Implementation
{
	public class CardService : ICardService
	{
		private readonly ICardRepository _cardRepository;

		public CardService(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
		{
			var cards = await _cardRepository.GetCardsForUser(userId);
			cards.TryGetValue(cardNumber, out var cardDetails);
			return cardDetails;
		}
	}


}

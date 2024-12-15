using CardOperationSerice.Models;
using CardOperationSerice.Repositories.Interfaces;

namespace CardOperationSerice.Repositories.Implementation
{
	public class CardRepository : ICardRepository
	{
		private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards = CreateSampleUserCards();

		public async Task<Dictionary<string, CardDetails>> GetCardsForUser(string userId)
		{
			await Task.Delay(1000); // Symulacja opóźnienia
			_userCards.TryGetValue(userId, out var cards);
			return cards ?? new Dictionary<string, CardDetails>();
		}

		private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
		{
			var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();

			for (var i = 1; i <= 3; i++)
			{
				var cards = new Dictionary<string, CardDetails>();
				var cardIndex = 1;
				foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
				{
					foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
					{
						var cardNumber = $"Card{i}{cardIndex}";
						cards.Add(cardNumber,
							new CardDetails(
								CardNumber: cardNumber,
								CardType: cardType,
								CardStatus: cardStatus,
								IsPinSet: cardIndex % 2 == 0));
						cardIndex++;
					}
				}
				var userId = $"User{i}";
				userCards.Add(userId, cards);
			}

			return userCards;
		}
	}

}

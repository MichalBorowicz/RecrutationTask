using CardOperationSerice.Common;
using CardOperationSerice.Models;
using CardOperationSerice.Services.Abstract;

namespace CardOperationSerice.Services.Implementation
{
	public class RuleService : IRuleService
	{
		public IEnumerable<string> GetAllowedActions(CardDetails cardDetails)
		{
			var allowedActionsByCardType = AllowedActionsByCardType(cardDetails.CardType);
			var allowedActionsByCardStatus = AllowedActionsByCardStatus(cardDetails.CardStatus);
			var allowedActionsByCardStatusIncludingPin = UpdateAllowedActionsWithPin(cardDetails, allowedActionsByCardStatus);


			return GetCommonActionsForCardTypeAndCardStatus(allowedActionsByCardType, allowedActionsByCardStatusIncludingPin);
		}

		private ICollection<string> UpdateAllowedActionsWithPin(CardDetails cardDetails, ICollection<string> allowedActionsByCardStatus)
		{
			switch (cardDetails.CardStatus)
			{
				case CardStatus.Ordered:
					if (cardDetails.IsPinSet)
					{
						allowedActionsByCardStatus.Add(ActionNames.Action6);
					}
					else if (!cardDetails.IsPinSet)
					{
						allowedActionsByCardStatus.Add(ActionNames.Action7);
					}
					break;
				case CardStatus.Inactive:
					if (cardDetails.IsPinSet)
					{
						allowedActionsByCardStatus.Add(ActionNames.Action6);
					}
					else if (!cardDetails.IsPinSet)
					{
						allowedActionsByCardStatus.Add(ActionNames.Action7);
					}
					break;
				case CardStatus.Active:
					if (cardDetails.IsPinSet)
					{
						allowedActionsByCardStatus.Add(ActionNames.Action6);
					}
					else if (!cardDetails.IsPinSet)
					{
						allowedActionsByCardStatus.Add(ActionNames.Action7);
					}
					break;
				case CardStatus.Restricted:
					break;
				case CardStatus.Blocked:
					if (cardDetails.IsPinSet)
					{
						allowedActionsByCardStatus.Add(ActionNames.Action6);
						allowedActionsByCardStatus.Add(ActionNames.Action7);
					}
					break;
				case CardStatus.Expired:
					break;
				case CardStatus.Closed:
					break;
				default:
					break;
			}
			return allowedActionsByCardStatus;
		}


		private ICollection<string> AllowedActionsByCardType(CardType cardType)
		{
			return cardType switch
			{
				CardType.Prepaid => SafeGetCardActions(CardType.Prepaid),
				CardType.Debit => SafeGetCardActions(CardType.Debit),
				CardType.Credit => SafeGetCardActions(CardType.Credit),
				_ => new List<string>() //TODO: To consider if error should be thrown. Need more information.
			};
		}

		private ICollection<string> SafeGetActions(CardStatus status)
		{
			return CardRules.CardDefaultActionsStatus.GetValueOrDefault(status) ?? new List<string>();
		}
		private ICollection<string> SafeGetCardActions(CardType type)
		{
			return CardRules.CardDefaultActionsType.GetValueOrDefault(type) ?? new List<string>();
		}
		private ICollection<string> AllowedActionsByCardStatus(CardStatus cardStatus)
		{//TODO: To consider if error should be thrown when list will be empty. Need more information.
			return cardStatus switch
			{
				CardStatus.Ordered => SafeGetActions(CardStatus.Ordered),
				CardStatus.Inactive => SafeGetActions(CardStatus.Inactive),
				CardStatus.Active => SafeGetActions(CardStatus.Active),
				CardStatus.Restricted => SafeGetActions(CardStatus.Restricted),
				CardStatus.Blocked => SafeGetActions(CardStatus.Blocked),
				CardStatus.Expired => SafeGetActions(CardStatus.Expired),
				CardStatus.Closed => SafeGetActions(CardStatus.Closed),
				_ => new List<string>()
			};
		}
		public static IEnumerable<string> GetCommonActionsForCardTypeAndCardStatus(ICollection<string> allowedActionsByCardType, ICollection<string> allowedActionsByCardStatusIncludingPin)
		{
			return allowedActionsByCardType.Intersect(allowedActionsByCardStatusIncludingPin);
		}
	}

}

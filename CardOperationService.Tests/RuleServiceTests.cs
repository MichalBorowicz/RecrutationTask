using CardOperationSerice.Common;
using CardOperationSerice.Models;
using CardOperationSerice.Services.Implementation;
using Microsoft.VisualBasic;

namespace CardOperationService.Tests
{
	public class RuleServiceTests
	{
		private readonly RuleService _ruleService;

		public RuleServiceTests()
		{
			_ruleService = new RuleService();
		}

		[Fact]
		public void GetAllowedActions_ReturnsCorrectActions_ForPrepaidClosedCard()
		{
			// Arrange
			var cardDetails = new CardDetails(
			CardNumber: "Card1",
				CardType: CardType.Prepaid,
				CardStatus: CardStatus.Closed,
				IsPinSet: false
			);

			// Act
			var allowedActions = _ruleService.GetAllowedActions(cardDetails);

			// Assert
			Assert.Equal(new List<string> { ActionNames.Action3, ActionNames.Action4, ActionNames.Action9 }, allowedActions);
		}

		[Fact]
		public void GetAllowedActions_ReturnsCorrectActions_ForCreditBlockedCardWithPin()
		{
			// Arrange
			var cardDetails = new CardDetails(
			CardNumber: "Card2",
				CardType: CardType.Credit,
				CardStatus: CardStatus.Blocked,
				IsPinSet: true
			);

			// Act
			var allowedActions = _ruleService.GetAllowedActions(cardDetails);

			// Assert
			Assert.Contains(ActionNames.Action6, allowedActions);
			Assert.Contains(ActionNames.Action7, allowedActions);
		}

		[Fact]
		public void GetAllowedActions_ExcludesAction6_WhenPinIsNotSet()
		{
			// Arrange
			var cardDetails = new CardDetails(
			CardNumber: "Card3",
				CardType: CardType.Debit,
				CardStatus: CardStatus.Active,
				IsPinSet: false
			);

			// Act
			var allowedActions = _ruleService.GetAllowedActions(cardDetails);

			// Assert
			Assert.DoesNotContain(ActionNames.Action6, allowedActions);
		}

		[Fact]
		public void GetAllowedActions_ReturnsCorrectActions_ForExpiredPrepaidCard()
		{
			// Arrange
			var cardDetails = new CardDetails("Card5", CardType.Prepaid, CardStatus.Expired, false);

			// Act
			var allowedActions = _ruleService.GetAllowedActions(cardDetails); ;

			// Assert
			Assert.Equal(new List<string> { ActionNames.Action3, ActionNames.Action4, ActionNames.Action9 }, allowedActions);
		}
	}
}
using CardOperationSerice.Models.DTO;
using CardOperationSerice.Models;
using CardOperationSerice.Services.Abstract;
using CardOperationSerice.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using CardOperationSerice.Common;

namespace CardOperationService.Tests
{
	public class CardControllerTests
	{
		private readonly Mock<ICardService> _cardServiceMock;
		private readonly Mock<IRuleService> _ruleServiceMock;
		private readonly CardController _controller;

		public CardControllerTests()
		{
			_cardServiceMock = new Mock<ICardService>();
			_ruleServiceMock = new Mock<IRuleService>();
			_controller = new CardController(_cardServiceMock.Object, _ruleServiceMock.Object);
		}

		[Fact]
		public async Task GetAllowedActions_ReturnsAllowedActions_WhenCardExists()
		{
			// Arrange
			var request = new GetAllowedActionsRequest
			{
				UserId = "User1",
				CardNumber = "Card1"
			};

			var cardDetails = new CardDetails(
				CardNumber: "Card1",
				CardType: CardType.Prepaid,
				CardStatus: CardStatus.Closed,
				IsPinSet: false
			);

			_cardServiceMock.Setup(cs => cs.GetCardDetails("User1", "Card1"))
				.ReturnsAsync(cardDetails);

			_ruleServiceMock.Setup(rs => rs.GetAllowedActions(cardDetails))
				.Returns(["ACTION3", "ACTION4", "ACTION9"]);

			// Act
			var result = await _controller.GetAllowedActions(request);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var response = Assert.IsType<GetAllowedActionsResponse>(okResult.Value);

			Assert.Equal("Card1", response.CardNumber);
			Assert.Equal(["ACTION3", "ACTION4", "ACTION9"], response.AllowedActions);
		}

		[Fact]
		public async Task GetAllowedActions_ReturnsNotFound_WhenCardDoesNotExist()
		{
			// Arrange
			var request = new GetAllowedActionsRequest
			{
				UserId = "User1",
				CardNumber = "Card99"
			};

			_cardServiceMock.Setup(cs => cs.GetCardDetails("User1", "Card99"))
				.ReturnsAsync(value: null);

			// Act
			var result = await _controller.GetAllowedActions(request);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			var response = Assert.IsType<ErrorResponse>(notFoundResult.Value);

			Assert.Equal(Constants.CardNotFoundMessage, response.Message);
		}

		[Fact]
		public async Task GetAllowedActions_ReturnsBadRequest_WhenRequestIsInvalid()
		{
			// Arrange
			var request = new GetAllowedActionsRequest
			{
				UserId = "",
				CardNumber = ""
			};

			_controller.ModelState.AddModelError("UserId", "The UserId field is required.");

			// Act
			var result = await _controller.GetAllowedActions(request);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var response = Assert.IsType<ErrorResponse>(badRequestResult.Value);

			Assert.Equal(Constants.InvalidRequestMessage, response.Message);
		}

		[Fact]
		public async Task GetAllowedActions_ReturnsBadRequest_WhenCardNumberIsMissing()
		{
			// Arrange
			var request = new GetAllowedActionsRequest
			{
				UserId = "User1",
				CardNumber = ""
			};

			_controller.ModelState.AddModelError("CardNumber", "The CardNumber field is required.");

			// Act
			var result = await _controller.GetAllowedActions(request);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var response = Assert.IsType<ErrorResponse>(badRequestResult.Value);

			Assert.Equal(Constants.InvalidRequestMessage, response.Message);
		}

		[Fact]
		public async Task GetAllowedActions_ReturnsBadRequest_WhenUserIdIsMissing()
		{
			// Arrange
			var request = new GetAllowedActionsRequest
			{
				UserId = "",
				CardNumber = "Card1"
			};

			_controller.ModelState.AddModelError("UserId", Constants.InvalidRequestMessage);

			// Act
			var result = await _controller.GetAllowedActions(request);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var response = Assert.IsType<ErrorResponse>(badRequestResult.Value);

			Assert.Equal(Constants.InvalidRequestMessage, response.Message);
		}

		[Fact]
		public async Task GetAllowedActions_ReturnsBadRequest_WhenBothUserIdAndCardNumberAreMissing()
		{
			// Arrange
			var request = new GetAllowedActionsRequest
			{
				UserId = "",
				CardNumber = ""
			};

			_controller.ModelState.AddModelError("UserId", "The UserId field is required.");
			_controller.ModelState.AddModelError("CardNumber", "The CardNumber field is required.");

			// Act
			var result = await _controller.GetAllowedActions(request);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			var response = Assert.IsType<ErrorResponse>(badRequestResult.Value);

			Assert.Equal(Constants.InvalidRequestMessage, response.Message);
		}

		[Fact]
		public async Task GetAllowedActions_ReturnsEmptyActions_WhenRuleServiceReturnsEmptyList()
		{
			// Arrange
			var request = new GetAllowedActionsRequest
			{
				UserId = "User1",
				CardNumber = "Card1"
			};

			var cardDetails = new CardDetails(
				CardNumber: "Card1",
				CardType: CardType.Prepaid,
				CardStatus: CardStatus.Closed,
				IsPinSet: false
			);

			_cardServiceMock.Setup(cs => cs.GetCardDetails("User1", "Card1"))
				.ReturnsAsync(cardDetails);

			_ruleServiceMock.Setup(rs => rs.GetAllowedActions(cardDetails))
				.Returns(new List<string>());

			// Act
			var result = await _controller.GetAllowedActions(request);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var response = Assert.IsType<GetAllowedActionsResponse>(okResult.Value);

			Assert.Equal("Card1", response.CardNumber);
			Assert.Empty(response.AllowedActions);
		}
	}
}

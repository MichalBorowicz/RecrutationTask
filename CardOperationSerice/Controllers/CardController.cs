using CardOperationSerice.Common;
using CardOperationSerice.Models;
using CardOperationSerice.Models.DTO;
using CardOperationSerice.Services.Abstract;
using CardOperationSerice.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
	private readonly ICardService _cardService;
	private readonly IRuleService _ruleService;

	public CardController(ICardService cardService, IRuleService ruleService)
	{
		_cardService = cardService;
		_ruleService = ruleService;
	}

	[HttpPost("GetAllowedActions")]
	public async Task<IActionResult> GetAllowedActions([FromBody] GetAllowedActionsRequest request)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(new ErrorResponse(Constants.InvalidRequestMessage));
		}

		var cardDetails = await _cardService.GetCardDetails(request.UserId, request.CardNumber);

		if (cardDetails == null)
		{
			return NotFound(new ErrorResponse(Constants.CardNotFoundMessage));
		}

		var allowedActions = _ruleService.GetAllowedActions(cardDetails);

		return Ok(new GetAllowedActionsResponse
		{
			CardNumber = cardDetails.CardNumber,
			AllowedActions = allowedActions
		});
	}
}


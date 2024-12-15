using CardOperationSerice.Models;

namespace CardOperationSerice.Services.Abstract
{
	public interface IRuleService
	{
		IEnumerable<string> GetAllowedActions(CardDetails cardDetails);
	}
}

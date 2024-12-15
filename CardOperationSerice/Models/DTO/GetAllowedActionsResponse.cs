namespace CardOperationSerice.Models.DTO
{
	public class GetAllowedActionsResponse
	{
		public string CardNumber { get; set; }
		public IEnumerable<string> AllowedActions { get; set; }
	}
}

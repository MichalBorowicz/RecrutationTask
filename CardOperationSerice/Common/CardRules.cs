using CardOperationSerice.Models;

namespace CardOperationSerice.Common
{
	public static class CardRules
	{
		public static readonly Dictionary<CardStatus, List<string>> CardDefaultActionsStatus = new()
	{
		{ CardStatus.Ordered, new List<string> { ActionNames.Action3, ActionNames.Action4, ActionNames.Action5, ActionNames.Action8, ActionNames.Action9, ActionNames.Action10, ActionNames.Action12, ActionNames.Action13 } },
		{ CardStatus.Inactive, new List<string> { ActionNames.Action2, ActionNames.Action3, ActionNames.Action4, ActionNames.Action5, ActionNames.Action8, ActionNames.Action9, ActionNames.Action10, ActionNames.Action11, ActionNames.Action12, ActionNames.Action13 } },
		{ CardStatus.Active, new List<string> { ActionNames.Action1, ActionNames.Action3, ActionNames.Action4, ActionNames.Action5, ActionNames.Action8, ActionNames.Action9, ActionNames.Action10, ActionNames.Action11, ActionNames.Action12, ActionNames.Action13 } },
		{ CardStatus.Restricted, new List<string> { ActionNames.Action3, ActionNames.Action4, ActionNames.Action5, ActionNames.Action9 } },
		{ CardStatus.Blocked, new List<string> { ActionNames.Action3, ActionNames.Action4, ActionNames.Action5, ActionNames.Action8, ActionNames.Action9 } },
		{ CardStatus.Expired, new List<string> { ActionNames.Action3, ActionNames.Action4, ActionNames.Action5, ActionNames.Action9 } },
		{ CardStatus.Closed, new List<string> { ActionNames.Action3, ActionNames.Action4, ActionNames.Action5, ActionNames.Action9 } }
	};

		public static readonly Dictionary<CardType, List<string>> CardDefaultActionsType = new()
	{
		{ CardType.Prepaid, new List<string> { ActionNames.Action1, ActionNames.Action2, ActionNames.Action3, ActionNames.Action4, ActionNames.Action6, ActionNames.Action7, ActionNames.Action8, ActionNames.Action9, ActionNames.Action10, ActionNames.Action11, ActionNames.Action12, ActionNames.Action13 } },
		{ CardType.Debit, new List<string> { ActionNames.Action1, ActionNames.Action2, ActionNames.Action3, ActionNames.Action4, ActionNames.Action6, ActionNames.Action7, ActionNames.Action8, ActionNames.Action9, ActionNames.Action10, ActionNames.Action11, ActionNames.Action12, ActionNames.Action13 }  },
		{ CardType.Credit, new List<string> { ActionNames.Action1, ActionNames.Action2, ActionNames.Action3, ActionNames.Action4, ActionNames.Action5, ActionNames.Action6, ActionNames.Action7, ActionNames.Action8, ActionNames.Action9, ActionNames.Action10, ActionNames.Action11, ActionNames.Action12, ActionNames.Action13 }  }
	};
	}

}

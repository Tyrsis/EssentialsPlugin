﻿using System.Linq;
using EssentialsPlugin.Utility;
using SEModAPIInternal.API.Common;


namespace EssentialsPlugin.ChatHandlers
{
	public class HandleFaction : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "Sends a private message to all faction members that are online.  Usage: /faction <msg>";
		}

		public override string GetCommandText()
		{
			return "/faction";
		}

		public override bool IsAdminCommand()
		{
			return false;
		}

		public override bool AllowedInConsole()
		{
			return true;
		}

		public override bool IsClientOnly()
		{
			return true;
		}

		public override bool HandleCommand( ulong userId, string command )
		{
			string[ ] words = command.Split( ' ' );
			if ( !words.Any( ) )
			{
				Communication.SendClientMessage(userId, "/message Server " + GetHelp());
			}

			string userName = PlayerMap.Instance.GetPlayerNameFromSteamId(userId);
			Communication.SendFactionClientMessage(userId, string.Format("/message F:{0} {1}", userName, string.Join(" ", words)));
//			Communication.SendClientMessage(userId, string.Format("/message Server Sent faction message"));
			return true;
		}
	}
}

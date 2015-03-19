﻿using EssentialsPlugin.Utility;

namespace EssentialsPlugin.ChatHandlers
{
	public class HandleAdminSettings : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "";
		}
		public override string GetCommandText()
		{
			return "/admin settings";
		}

		public override bool IsAdminCommand()
		{
			return true;
		}

		public override bool AllowedInConsole()
		{
			return true;
		}

		// admin nobeacon scan
		public override bool HandleCommand( ulong userId, string command )
		{
			string[ ] words = command.Split( ' ' );
			string results = PluginSettings.Instance.GetOrSetSettings( string.Join( " ", words ) );
			Communication.SendPrivateInformation(userId, results);
			return true;
		}
	}
}

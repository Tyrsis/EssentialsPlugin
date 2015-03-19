﻿using EssentialsPlugin.Utility;

namespace EssentialsPlugin.ChatHandlers
{
	public class HandleAdminScanCleanup : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "This command allows you to scan for grids that are considered trash.  Usage: /admin scan trash";
		}
		public override string GetCommandText()
		{
			return "/admin scan cleanup";
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
			CubeGrids.ScanCleanup( userId, words );
			return true;
		}
	}
}

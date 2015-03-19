﻿namespace EssentialsPlugin.ChatHandlers.AdminScan
{
	using EssentialsPlugin.Utility;

	public class HandleAdminScanGrids : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "This command allows you to scan for grids that meet criteria.  Usage: /admin scan grids";
		}
		public override string GetCommandText()
		{
			return "/admin scan grids";
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
		public override bool HandleCommand(ulong userId, string command)
		{
			CubeGrids.ScanGrids(userId, command);
			return true;
		}
	}
}

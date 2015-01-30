﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

using EssentialsPlugin.Utility;
using EssentialsPlugin.EntityManagers;

using Sandbox.ModAPI;
using Sandbox.Common.ObjectBuilders;

using VRageMath;
using VRage.Common.Utils;

using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;

using SEModAPIInternal.API.Common;

namespace EssentialsPlugin.ChatHandlers
{
	public class HandleUtilityGridsRefresh : ChatHandlerBase
	{
		private static HashSet<ulong> m_refreshTrack = new HashSet<ulong>();

		public static HashSet<ulong> RefreshTrack
		{
			get { return HandleUtilityGridsRefresh.m_refreshTrack; }
		}

		public override string GetHelp()
		{
			return "This will refresh your ships.  Usage: /utility grids refresh";
		}
		public override string GetCommandText()
		{
			return "/utility grids refresh";
		}

		public override bool IsAdminCommand()
		{
			return false;
		}

		public override bool AllowedInConsole()
		{
			return true;
		}

		public override bool HandleCommand(ulong userId, string[] words)
		{
			if (m_refreshTrack.Contains(userId))
			{
				Communication.SendPrivateInformation(userId, "You may only refresh once per login.  Please relog and try again.");
				return true;
			}

			if (!EntityManagement.ToggleMedbayGrids(userId))
			{
				Communication.SendPrivateInformation(userId, "Unable to refresh grids, server busy.");
				return true;
			}

			m_refreshTrack.Add(userId);
			Communication.SendPrivateInformation(userId, "Grids being refreshed ... please wait a moment");
			return true;
		}

	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

using EssentialsPlugin.Utility;

using Sandbox.ModAPI;
using Sandbox.Common.ObjectBuilders;

using VRageMath;

using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;
using SEModAPIInternal.API.Common;

using EssentialsPlugin.ProcessHandler;

namespace EssentialsPlugin.ChatHandlers
{
	public class HandleAdminRestart : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "Force a restart to occur within the next X minutes.  Usage: /admin restart <minutes>";
		}

		public override string GetCommandText()
		{
			return "/admin restart";
		}

		public override bool IsAdminCommand()
		{
			return true;
		}

		public override bool AllowedInConsole()
		{
			return true;
		}

		public override bool HandleCommand(ulong userId, string[] words)
		{
			if (words.Length != 1)
			{
				Communication.SendPrivateInformation(userId, GetHelp());
				return true;
			}

			int minutes = -1;
			if (!int.TryParse(words[0], out minutes))
			{
				Communication.SendPrivateInformation(userId, string.Format("Invalid reboot time.  Must be an integer."));
				return true;
			}

			if(minutes < 1)
			{
				Communication.SendPrivateInformation(userId, string.Format("Invalid reboot time.  Must be greater than 0."));
				return true;
			}

			ProcessRestart.ForcedRestart = DateTime.Now.AddMinutes(minutes);
			Communication.SendPublicInformation(string.Format("[NOTICE]: The administrator is forcing a restart to occur in {0} minute(s).", minutes));

			return true;
		}
	}
}

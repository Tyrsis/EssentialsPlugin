﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EssentialsPlugin.Utility;
using Sandbox.ModAPI;
using SEModAPIInternal.API.Common;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using VRage.Common.Utils;
using System.Text.RegularExpressions;
using System.Threading;

using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid.CubeBlock;
using SEModAPIInternal.API.Entity;

using Sandbox.Common.ObjectBuilders;

using EssentialsPlugin.Settings;
using EssentialsPlugin.ChatHandlers;
using EssentialsPlugin.EntityManagers;

using Sandbox.Definitions;

namespace EssentialsPlugin.ProcessHandler
{
	public class ProcessConceal : ProcessHandlerBase
	{
		private static DateTime m_lastConcealCheck;
		private static DateTime m_lastRevealCheck;

		public static DateTime LastRevealCheck
		{
			get { return m_lastRevealCheck; }
			set { m_lastRevealCheck = value; }
		}

		public ProcessConceal()
		{
			//EntityManagement.RevealAll();
		}

		public override int GetUpdateResolution()
		{
			return 1000;
		}

		public override void Handle()
		{
			if (!PluginSettings.Instance.DynamicConcealEnabled)
				return;

			if (DateTime.Now - m_lastConcealCheck > TimeSpan.FromSeconds(30))
			{
				//Logging.WriteLineAndConsole("CheckAndConcealEntities");
				EntityManagement.CheckAndConcealEntities();
				m_lastConcealCheck = DateTime.Now;
			}

			if (DateTime.Now - m_lastRevealCheck > TimeSpan.FromSeconds(5))
			{
				//Logging.WriteLineAndConsole("CheckAndRevealEntities");
				EntityManagement.CheckAndRevealEntities();
				m_lastRevealCheck = DateTime.Now;
			}

			base.Handle();
		}

		public override void OnPlayerJoined(ulong remoteUserId)
		{
			if (!PluginSettings.Instance.DynamicConcealEnabled)
				return;

			if (HandleUtilityGridsRefresh.RefreshTrack.Contains(remoteUserId))
				HandleUtilityGridsRefresh.RefreshTrack.Remove(remoteUserId);

			base.OnPlayerJoined(remoteUserId);
		}

		public override void OnPlayerWorldSent(ulong remoteUserId)
		{
			EntityManagement.SetOnline(remoteUserId, true);

			if (!PluginSettings.Instance.DynamicConcealEnabled)
				return;

			EntityManagement.CheckAndRevealEntities();
			m_lastRevealCheck = DateTime.Now;

			Logging.WriteLineAndConsole(string.Format("Check Reveal due to: {0}", remoteUserId));

			base.OnPlayerWorldSent(remoteUserId);
		}

		public override void OnPlayerLeft(ulong remoteUserId)
		{
			EntityManagement.SetOnline(remoteUserId, false);
			base.OnPlayerLeft(remoteUserId);
		}

	}
}


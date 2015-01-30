﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SEModAPIExtensions.API;
using SEModAPIExtensions.API.Plugin;
using SEModAPIExtensions.API.Plugin.Events;

using SEModAPIInternal.API.Entity.Sector.SectorObject.CubeGrid;
using SEModAPIInternal.API.Entity.Sector.SectorObject;

using Sandbox.ModAPI;

using EssentialsPlugin.Utility;

namespace EssentialsPlugin.ProcessHandler
{
	public abstract class ProcessHandlerBase
	{
		private DateTime m_lastUpdate;
		public DateTime LastUpdate
		{
			get { return m_lastUpdate; }
			set { m_lastUpdate = value; }
		}

		/// <summary>
		/// Initializer
		/// </summary>
		public ProcessHandlerBase()
		{
			m_lastUpdate = DateTime.Now;

			//Logging.WriteLineAndConsole(string.Format("Added process handler: Raised every {0}ms", GetUpdateResolution()));
		}

		/// <summary>
		/// Returns whether this handler is ready to be run
		/// </summary>
		/// <returns></returns>
		public bool CanProcess()
		{
			if(DateTime.Now - m_lastUpdate > TimeSpan.FromMilliseconds(GetUpdateResolution()))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Gets the processing resolution of this handler in milliseconds.
		/// </summary>
		/// <returns>resolution in ms</returns>
		public virtual int GetUpdateResolution()
		{
			return 1000;
		}

		/// <summary>
		/// Called when CanProcess() returns true.  This gets overriden and is the main handling function
		/// </summary>
		public virtual void Handle()
		{
			m_lastUpdate = DateTime.Now;
		}

		/// <summary>
		/// Called when a player joins
		/// </summary>
		/// <param name="remoteUserId"></param>
		public virtual void OnPlayerJoined(ulong remoteUserId)
		{

		}

		/// <summary>
		/// Called when a player leaves
		/// </summary>
		/// <param name="remoteUserId"></param>
		public virtual void OnPlayerLeft(ulong remoteUserId)
		{

		}

		public virtual void OnPlayerWorldSent(ulong remoteUserId)
		{

		}

		public virtual void OnEntityAdd(IMyEntity entity)
		{

		}

		public virtual void OnEntityRemove(IMyEntity entity)
		{

		}

		public virtual void OnCubeGridCreated(CubeGridEntity entity)
		{

		}

		public virtual void OnCubeGridDeleted(CubeGridEntity entity)
		{

		}

		public virtual void OnSectorSaved()
		{

		}
	}
}

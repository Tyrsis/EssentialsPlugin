﻿namespace EssentialsPlugin.ChatHandlers.AdminDelete
{
	using System.Collections.Generic;
	using System.Linq;
	using EssentialsPlugin.Utility;
	using Sandbox.Common.ObjectBuilders;
	using Sandbox.ModAPI;
	using SEModAPIInternal.API.Common;
	using SEModAPIInternal.API.Entity;

	public class HandleAdminDeleteGrids : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "This command allows you to delete grids that meet criteria.  Usage: /admin delete grids";
		}
		public override string GetCommandText()
		{
			return "/admin delete grids";
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
			HashSet<IMyEntity> grids = CubeGrids.ScanGrids( userId, command );

			bool confirm = true;
			/*
			if (words.FirstOrDefault(x => x.ToLower() == "confirm") != null)
			{
				confirm = true;
			}
			*/
			int count = 0;
			foreach (IMyEntity entity in grids)
			{
				if (!(entity is IMyCubeGrid))
					continue;

				IMyCubeGrid grid = (IMyCubeGrid)entity;
				MyObjectBuilder_CubeGrid gridBuilder = CubeGrids.SafeGetObjectBuilder(grid);
				if(confirm)
					BaseEntityNetworkManager.BroadcastRemoveEntity(entity, true);

				long ownerId = 0;
				string ownerName = "";
				if (CubeGrids.GetBigOwners(gridBuilder).Count > 0)
				{
					ownerId = CubeGrids.GetBigOwners(gridBuilder).First();
					ownerName = PlayerMap.Instance.GetPlayerItemFromPlayerId(ownerId).Name;
				}

				if(confirm)
					Logging.WriteLineAndConsole("Cleanup", string.Format("Cleanup Removed Grid - Id: {0} Display: {1} OwnerId: {2} OwnerName: {3}", entity.EntityId, entity.DisplayName, ownerId, ownerName));

				count++;
			}

			Communication.SendPrivateInformation(userId, string.Format("Operation deletes {0} grids", count));
			return true;
		}
	}
}

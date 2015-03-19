﻿using System.Linq;
using EssentialsPlugin.Utility;
using VRageMath;

namespace EssentialsPlugin.ChatHandlers
{
	public class HandleAdminMovePlayerPosition : ChatHandlerBase
	{
		public override string GetHelp()
		{
			return "This command allows you to move a player to another location.  Usage: /admin move player position [USERNAME] [X] [Y] [Z]";
		}
		public override string GetCommandText()
		{
			return "/admin move player position";
		}

		public override bool IsAdminCommand()
		{
			return true;
		}

		public override bool AllowedInConsole()
		{
			return true;
		}

		// /admin movefrom x y z x y z radius
		public override bool HandleCommand( ulong userId, string command )
		{
			string[ ] words = command.Split( ' ' );
			if ( words.Count( ) != 4 && words.Count( ) != 0 )
				return false;

			if (words.Count() != 4)
			{
				Communication.SendPrivateInformation(userId, GetHelp());
				return true;
			}

			// Test Input
			float test = 0;
			for(int r = 1; r < 4; r++)
			{
				if(!float.TryParse(words[r], out test))
				{
					Communication.SendPrivateInformation(userId, string.Format("The value at position {0} - '{1}' is invalid.  Please try the command again.", r + 1, words[r]));
					return true;
				}
			}

			string userName = words[0];
			Vector3D startPosition = new Vector3D(double.Parse(words[1]), double.Parse(words[2]), double.Parse(words[3]));
			if(!Player.Move(userName, startPosition))
			{
				Communication.SendPrivateInformation(userId, string.Format("Unable to move player: {0}", userName));
			}

			/*
			CharacterEntity charEntity = SectorObjectManager.Instance.GetTypedInternalData<CharacterEntity>().Where(x => x.DisplayName.ToLower() == userName.ToLower() && x.Health > 0).First();
			CubeGridEntity gridEntity = new CubeGridEntity(new FileInfo(Essentials.PluginPath + "MovePlayer.sbc"));
			gridEntity.EntityId = BaseEntity.GenerateEntityId();
			foreach (MyObjectBuilder_CubeBlock block in gridEntity.BaseCubeBlocks)
			{
				// set ownership
				if(block is MyObjectBuilder_Cockpit)
				{
					MyObjectBuilder_Cockpit cockpit = (MyObjectBuilder_Cockpit)block;
					cockpit.Pilot = (MyObjectBuilder_Character)charEntity.Export();								
				}
			}

			gridEntity.PositionAndOrientation = new MyPositionAndOrientation(startPosition, Vector3.Forward, Vector3.Up);

			Wrapper.GameAction(() =>
			{
				MyObjectBuilder_EntityBase baseEntity = gridEntity.Export();
				IMyEntity entity = MyAPIGateway.Entities.CreateFromObjectBuilderAndAdd(baseEntity);
				Type someManager = SandboxGameAssemblyWrapper.Instance.GetAssemblyType(SectorObjectManager.EntityBaseNetManagerNamespace, SectorObjectManager.EntityBaseNetManagerClass);
				Wrapper.InvokeStaticMethod(someManager, SectorObjectManager.EntityBaseNetManagerSendEntity, new object[] { entity.GetObjectBuilder() });
				gridEntity = new CubeGridEntity((MyObjectBuilder_CubeGrid)entity.GetObjectBuilder(), entity);
			});


			int count = 0;
			while (gridEntity.IsLoading)
			{
				Thread.Sleep(100);
				count++;
				if (count > 10)
					break;
			}

			if (gridEntity.IsLoading)
				return true;

			foreach (CubeBlockEntity block in gridEntity.CubeBlocks)
			{
				if(block is CockpitEntity)
				{
					block.IntegrityPercent = 0.1f;
					Logging.WriteLineAndConsole("Removing User From Cockpit");
				}
			}
			
			gridEntity.Dispose();
			*/

			return true;
		}
	}
}

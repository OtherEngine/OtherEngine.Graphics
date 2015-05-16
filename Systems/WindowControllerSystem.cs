using System.Collections.Generic;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Data;
using OtherEngine.Core.Systems;
using OtherEngine.Graphics.Components;
using OpenTK;
using OtherEngine.Core.Exceptions;
using System;
using OtherEngine.Graphics.Events;

namespace OtherEngine.Graphics.Systems
{
	public class WindowControllerSystem : GameSystem
	{
		[TrackComponent(typeof(GameWindowComponent))]
		public IEnumerable<GameEntity> Windows { get; private set; }

		public GameEntity CreateWindow()
		{
			if (State.IsDisabled())
				throw new GameSystemStateException(this, GameSystemState.Running);
			
			var window = new GameWindow();
			var entity = new GameEntity(Game);

			window.UpdateFrame += (sender, e) =>
				Game.Events.Fire(new WindowUpdateEvent(window, entity));
			window.RenderFrame += (sender, e) =>
				Game.Events.Fire(new WindowRenderEvent(window, entity));

			entity.Add(new GameWindowComponent(window));
			return entity;
		}

		public void RunGameLoop(GameEntity windowEntity)
		{
			if (State.IsDisabled())
				throw new GameSystemStateException(this, GameSystemState.Running);
			
			if (windowEntity == null)
				throw new ArgumentNullException("windowEntity");
			var component = windowEntity.GetOrThrow<GameWindowComponent>();

			component.Window.Run();
		}

		public void DestroyWindow(GameEntity windowEntity)
		{
			if (State.IsDisabled())
				throw new GameSystemStateException(this, GameSystemState.Running);
			
			if (windowEntity == null)
				throw new ArgumentNullException("windowEntity");
			var component = windowEntity.GetOrThrow<GameWindowComponent>();
			
			component.Window.Close();
			windowEntity.Remove(component);
		}
	}
}


using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OtherEngine.Core;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Components;
using OtherEngine.Graphics.Components;
using OtherEngine.Graphics.Events;

namespace OtherEngine.Graphics.Controllers
{
	[AutoEnable]
	public class WindowController : Controller
	{
		[TrackComponent(typeof(GameWindowComponent))]
		public IReadOnlyCollection<Entity> Windows { get; private set; }

		public Entity Create()
		{
			var window = new GameWindow(1200, 725,
				GraphicsMode.Default, "",
				GameWindowFlags.FixedWindow, DisplayDevice.Default,
				3, 3, GraphicsContextFlags.Default);
			
			var entity = new Entity(Game){
				new TypeComponent { Value = "Window" },
				new GameWindowComponent(window)
			};

			window.Load += (sender, e) =>
				Game.Events.Fire(new WindowLoadEvent(window, entity));
			window.Resize += (sender, e) =>
				Game.Events.Fire(new WindowResizeEvent(window, entity));
			
			window.UpdateFrame += (sender, e) =>
				Game.Events.Fire(new WindowUpdateEvent(window, entity, TimeSpan.FromSeconds(e.Time)));
			window.RenderFrame += (sender, e) =>
				Game.Events.Fire(new WindowRenderEvent(window, entity, TimeSpan.FromSeconds(e.Time)));

			return entity;
		}

		public void RunGameLoop(Entity windowEntity)
		{
			if (windowEntity == null)
				throw new ArgumentNullException("windowEntity");
			var component = windowEntity.GetOrThrow<GameWindowComponent>();

			component.Window.Run(60, 60);
		}

		public void Destroy(Entity windowEntity)
		{
			if (windowEntity == null)
				throw new ArgumentNullException("windowEntity");
			var component = windowEntity.GetOrThrow<GameWindowComponent>();
			
			component.Window.Close();
			windowEntity.Remove(component);
		}
	}
}


using System;
using OpenTK;
using OpenTK.Graphics;
using OtherEngine.Core;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Components;
using OtherEngine.Core.Tracking;
using OtherEngine.Graphics.Components;
using OtherEngine.Graphics.Events;

namespace OtherEngine.Graphics.Controllers
{
	[AutoEnable]
	public class WindowController : Controller
	{
		[TrackComponent]
		public EntityCollection<TextureComponent> Windows { get; private set; }

		public EntityRef<GameWindowComponent> Create()
		{
			var gameWindow = new GameWindow(1200, 725,
				GraphicsMode.Default, "",
				GameWindowFlags.FixedWindow, DisplayDevice.Default,
				3, 3, GraphicsContextFlags.Default);
			
			var window = new Entity(Game) { new TypeComponent { Value = "Window" } }
				.AddRef(new GameWindowComponent(gameWindow));

			gameWindow.Load += (sender, e) =>
				Game.Events.Fire(new WindowLoadEvent(gameWindow, window));
			gameWindow.Resize += (sender, e) =>
				Game.Events.Fire(new WindowResizeEvent(gameWindow, window));
			
			gameWindow.UpdateFrame += (sender, e) =>
				Game.Events.Fire(new WindowUpdateEvent(gameWindow, window, TimeSpan.FromSeconds(e.Time)));
			gameWindow.RenderFrame += (sender, e) =>
				Game.Events.Fire(new WindowRenderEvent(gameWindow, window, TimeSpan.FromSeconds(e.Time)));

			return window;
		}

		public void RunGameLoop(EntityRef<GameWindowComponent> window)
		{
			window.Component.Window.Run(60, 60);
		}

		public void Destroy(EntityRef<GameWindowComponent> window)
		{
			window.Component.Window.Close();
			window.Entity.Remove(window.Component);
		}
	}
}


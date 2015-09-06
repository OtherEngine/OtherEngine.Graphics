using System;
using OpenTK;
using OtherEngine.Core;

namespace OtherEngine.Graphics.Window
{
	public abstract class WindowEvent : Event
	{
		public GameWindow GameWindow { get; private set; }
		public EntityRef<WindowComponent> Window { get; private set; }

		internal WindowEvent(GameWindow gameWindow, Entity window)
		{
			GameWindow = gameWindow;
			Window = window;
		}
	}

	public class WindowLoadEvent : WindowEvent
	{
		internal WindowLoadEvent(GameWindow gameWindow, EntityRef<WindowComponent> window)
			: base(gameWindow, window) {  }
	}

	public class WindowResizeEvent : WindowEvent
	{
		internal WindowResizeEvent(GameWindow gameWindow, EntityRef<WindowComponent> window)
			: base(gameWindow, window) {  }
	}

	public class WindowUpdateEvent : WindowEvent
	{
		public TimeSpan Delta { get; private set; }

		internal WindowUpdateEvent(GameWindow gameWindow, EntityRef<WindowComponent> window, TimeSpan delta)
			: base(gameWindow, window)
		{
			Delta = delta;
		}
	}

	public class WindowRenderEvent : WindowEvent
	{
		public TimeSpan Delta { get; private set; }

		internal WindowRenderEvent(GameWindow gameWindow, EntityRef<WindowComponent> window, TimeSpan delta)
			: base(gameWindow, window)
		{
			Delta = delta;
		}
	}
}


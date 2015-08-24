using System;
using OpenTK;
using OtherEngine.Core;

namespace OtherEngine.Graphics.Events
{
	public abstract class WindowEvent : Event
	{
		public GameWindow Window { get; private set; }
		public Entity WindowEntity { get; private set; }

		internal WindowEvent(GameWindow window, Entity windowEntity)
		{
			Window = window;
			WindowEntity = windowEntity;
		}
	}

	public class WindowLoadEvent : WindowEvent
	{
		internal WindowLoadEvent(GameWindow window, Entity windowEntity)
			: base(window, windowEntity) {  }
	}

	public class WindowResizeEvent : WindowEvent
	{
		internal WindowResizeEvent(GameWindow window, Entity windowEntity)
			: base(window, windowEntity) {  }
	}

	public class WindowUpdateEvent : WindowEvent
	{
		public TimeSpan Delta { get; private set; }

		internal WindowUpdateEvent(GameWindow window, Entity windowEntity, TimeSpan delta)
			: base(window, windowEntity)
		{
			Delta = delta;
		}
	}

	public class WindowRenderEvent : WindowEvent
	{
		public TimeSpan Delta { get; private set; }

		internal WindowRenderEvent(GameWindow window, Entity windowEntity, TimeSpan delta)
			: base(window, windowEntity)
		{
			Delta = delta;
		}
	}
}


using OpenTK;
using OtherEngine.Core.Data;
using OtherEngine.Core.Events;

namespace OtherEngine.Graphics.Events
{
	public abstract class WindowEvent : IGameEvent
	{
		public GameWindow Window { get; private set; }
		public GameEntity WindowEntity { get; private set; }

		internal WindowEvent(GameWindow window, GameEntity windowEntity)
		{
			Window = window;
			WindowEntity = windowEntity;
		}
	}

	public class WindowUpdateEvent : WindowEvent
	{
		internal WindowUpdateEvent(GameWindow window, GameEntity windowEntity)
			: base(window, windowEntity) {  }
	}

	public class WindowRenderEvent : WindowEvent
	{
		internal WindowRenderEvent(GameWindow window, GameEntity windowEntity)
			: base(window, windowEntity) {  }
	}
}


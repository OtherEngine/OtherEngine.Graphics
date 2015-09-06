using OpenTK;
using OtherEngine.Core;

namespace OtherEngine.Graphics.Window
{
	public class WindowComponent : Component
	{
		public GameWindow Window { get; private set; }

		public int Width { get { return Window.Width; } }
		public int Height { get { return Window.Height; } }

		internal WindowComponent(GameWindow window)
		{
			Window = window;
		}
	}
}


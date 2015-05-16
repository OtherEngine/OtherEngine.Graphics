using OpenTK;
using OtherEngine.Core.Data;

namespace OtherEngine.Graphics.Components
{
	public class GameWindowComponent : GameComponent
	{
		public GameWindow Window { get; private set; }

		public int Width { get { return Window.Width; } }
		public int Height { get { return Window.Height; } }

		internal GameWindowComponent(GameWindow window)
		{
			Window = window;
		}
	}
}


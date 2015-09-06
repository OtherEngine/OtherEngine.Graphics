using OtherEngine.Core;
using OpenTK.Graphics.OpenGL4;

namespace OtherEngine.Graphics.Components
{
	public class BufferComponent : Component
	{
		public BufferTarget Target { get; private set; }

		internal BufferComponent(BufferTarget target)
		{
			Target = target;
		}
	}
}


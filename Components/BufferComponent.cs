using OtherEngine.Core;
using OpenTK.Graphics.OpenGL4;

namespace OtherEngine.Graphics.Components
{
	public class BufferComponent : Component
	{
		public int Handle { get; private set; }

		public BufferTarget Target { get; private set; }


		internal BufferComponent(int handle, BufferTarget target)
		{
			Handle = handle;
			Target = target;
		}
	}
}


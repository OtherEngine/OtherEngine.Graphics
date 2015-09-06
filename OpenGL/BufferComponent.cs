using OtherEngine.Core;
using OpenTK.Graphics.OpenGL4;

namespace OtherEngine.Graphics.OpenGL
{
	public class BufferComponent : Component
	{
		public BufferTarget Target { get; private set; }


		internal VertexAttribPointerType PointerType { get; set; }

		internal int Size { get; set; }


		internal BufferComponent(BufferTarget target)
		{
			Target = target;
		}
	}
}


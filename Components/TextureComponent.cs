using OtherEngine.Core;

namespace OtherEngine.Graphics.Components
{
	public class TextureComponent : Component
	{
		public int Handle { get; private set; }

		internal TextureComponent(int handle)
		{
			Handle = handle;
		}
	}
}


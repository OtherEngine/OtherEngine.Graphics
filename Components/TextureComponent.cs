using OtherEngine.Core;

namespace OtherEngine.Graphics.Components
{
	public class TextureComponent : Component
	{
		public int ID { get; private set; }

		internal TextureComponent(int id)
		{
			ID = id;
		}
	}
}


using OtherEngine.Core;

namespace OtherEngine.Graphics.OpenGL
{
	public class ShaderAttribComponent : Component
	{
		public int Index { get; private set; }

		internal ShaderAttribComponent(int index)
		{
			Index = index;
		}
	}
}


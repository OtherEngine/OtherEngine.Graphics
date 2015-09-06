using OtherEngine.Core;

namespace OtherEngine.Graphics.Components
{
	public class ShaderProgramComponent : Component
	{
		/// <summary>
		/// Gets whether the program has been
		/// successfully linked and can be used.
		/// </summary>
		public bool Linked { get; internal set; }

		internal ShaderProgramComponent() {  }
	}
}


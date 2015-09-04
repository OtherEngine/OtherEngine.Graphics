using System;
using OtherEngine.Core;

namespace OtherEngine.Graphics.Components
{
	public class ShaderProgramComponent : Component
	{
		/// <summary>
		/// Gets the handle of the shader program.
		/// </summary>
		public int Handle { get; private set; }


		/// <summary>
		/// Gets whether the program has been
		/// successfully linked and can be used.
		/// </summary>
		public bool Linked { get; internal set; }

		/// <summary>
		/// Gets the information log for the most recent
		/// linking of the program. May be null if the
		/// program has not been linked yet.
		/// </summary>
		public string InfoLog { get; internal set; }


		internal ShaderProgramComponent(int handle)
		{
			Handle = handle;
		}
	}
}


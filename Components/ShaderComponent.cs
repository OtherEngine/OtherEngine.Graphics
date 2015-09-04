using OpenTK.Graphics.OpenGL4;
using OtherEngine.Core;

namespace OtherEngine.Graphics.Components
{
	public class ShaderComponent : Component
	{
		/// <summary>
		/// Gets the handle of the shader.
		/// </summary>
		public int Handle { get; private set; }

		/// <summary>
		/// Gets the type of the shader.
		/// </summary>
		// TODO: Use OpenTK's enum or a custom one?
		public ShaderType Type { get; private set; }


		/// <summary>
		/// Gets whether the shader has been
		/// successfully compiled and can be used.
		/// </summary>
		public bool Compiled { get; internal set; }

		/// <summary>
		/// Gets the source string of the shader.
		/// May be null if it hasn't been set yet.
		/// (Though what would you do with an empty shader?)
		/// </summary>
		public string Source { get; internal set; }

		/// <summary>
		/// Gets the information log for the most recent
		/// compilation of the shader. May be null if the
		/// shader has not been compiled yet.
		/// </summary>
		public string InfoLog { get; internal set; }


		internal ShaderComponent(int id, ShaderType type)
		{
			Handle = id;
			Type = type;
		}
	}
}


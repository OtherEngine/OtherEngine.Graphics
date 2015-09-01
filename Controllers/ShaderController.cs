using System;
using OpenTK.Graphics.OpenGL4;
using OtherEngine.Core;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Components;
using OtherEngine.Core.Events;
using OtherEngine.Core.Tracking;
using OtherEngine.Graphics.Components;

namespace OtherEngine.Graphics.Controllers
{
	[AutoEnable]
	public class ShaderController : Controller
	{
		[TrackComponent]
		public EntityCollection<TextureComponent> Shaders { get; private set; }


		public EntityRef<ShaderComponent> Create(ShaderType type)
		{
			var handle = GL.CreateShader(type);

			return new Entity(Game) { new TypeComponent { Value = "Shader" } }
				.AddRef(new ShaderComponent(handle, type));
		}

		public void SetSource(EntityRef<ShaderComponent> shader, string source)
		{
			GL.ShaderSource(shader.Component.ID, source);
			shader.Component.Source = source;
		}

		public void Compile(EntityRef<ShaderComponent> shader)
		{
			GL.CompileShader(shader.Component.ID);

			int compileStatus;
			GL.GetShader(shader.Component.ID, ShaderParameter.CompileStatus, out compileStatus);

			shader.Component.Compiled = (compileStatus > 0);
			shader.Component.InfoLog = GL.GetShaderInfoLog(shader.Component.ID);
		}

		public void Delete(EntityRef<ShaderComponent> shader)
		{
			shader.Entity.Remove(shader.Component);
		}


		public EntityRef<ShaderComponent> CreateAndCompile(ShaderType type, string source)
		{
			var shader = Create(type);
			SetSource(shader, source);
			Compile(shader);
			return shader;
		}


		[SubscribeEvent]
		void OnShaderComponentRemoved(ComponentRemovedEvent<ShaderComponent> ev)
		{
			GL.DeleteShader(ev.Component.ID);
		}
	}
}


using System;
using OpenTK.Graphics.OpenGL4;
using OtherEngine.Core;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Events;
using OtherEngine.Core.Hierarchy;
using OtherEngine.Core.Tracking;
using OtherEngine.Graphics.Components;
using OtherEngine.Core.Components;

namespace OtherEngine.Graphics.Controllers
{
	[AutoEnable]
	public class ShaderController : Controller
	{
		[TrackComponent]
		public EntityCollection<ShaderComponent> Shaders { get; private set; }

		[TrackComponent]
		public EntityCollection<ShaderProgramComponent> Programs { get; private set; }

		[TrackComponent]
		public EntityCollection<ShaderAttribComponent> Attribs { get; private set; }


		#region Shader related

		public EntityRef<ShaderComponent> CreateShader(ShaderType type)
		{
			var handle = GL.CreateShader(type);
			return new Entity(Game).AddTypeRef(new ShaderComponent(handle, type));
		}

		public void SetShaderSource(EntityRef<ShaderComponent> shader, string source)
		{
			GL.ShaderSource(shader.Component.Handle, source);
			shader.Component.Source = source;
		}

		public void CompileShader(EntityRef<ShaderComponent> shader)
		{
			GL.CompileShader(shader.Component.Handle);

			int compileStatus;
			GL.GetShader(shader.Component.Handle, ShaderParameter.CompileStatus, out compileStatus);

			shader.Component.Compiled = (compileStatus > 0);
			shader.Component.InfoLog = GL.GetShaderInfoLog(shader.Component.Handle);
		}

		public void DeleteShader(EntityRef<ShaderComponent> shader)
		{
			shader.Entity.Remove(shader.Component);
		}


		public EntityRef<ShaderComponent> CreateAndCompile(ShaderType type, string source)
		{
			var shader = CreateShader(type);
			SetShaderSource(shader, source);
			CompileShader(shader);
			return shader;
		}


		[SubscribeEvent]
		void OnShaderComponentRemoved(ComponentRemovedEvent<ShaderComponent> ev)
		{
			GL.DeleteShader(ev.Component.Handle);
		}

		#endregion

		#region Program related

		public EntityRef<ShaderProgramComponent> CreateProgram()
		{
			var handle = GL.CreateProgram();
			return new Entity(Game){ "Shaders", "Attribs", "Uniforms" }
				.AddTypeRef(new ShaderProgramComponent(handle));
		}

		public void AttachShader(EntityRef<ShaderProgramComponent> program, EntityRef<ShaderComponent> shader)
		{
			GL.AttachShader(program.Component.Handle, shader.Component.Handle);
			program.Entity.GetChild("Shaders").Add(shader);
		}

		public void LinkProgram(EntityRef<ShaderProgramComponent> program)
		{
			GL.LinkProgram(program.Component.Handle);

			int linkStatus;
			GL.GetProgram(program.Component.Handle, GetProgramParameterName.LinkStatus, out linkStatus);

			program.Component.Linked = (linkStatus > 0);
			program.Component.InfoLog = GL.GetProgramInfoLog(program.Component.Handle);
		}

		public void UseProgram(EntityRef<ShaderProgramComponent> program)
		{
			GL.UseProgram(program.Component.Handle);
		}

		#endregion

		#region Attrib related

		public EntityRef<ShaderAttribComponent> DefineAttrib(string name, int index)
		{
			return new Entity(Game){ new NameComponent { Value = name } }
				.AddTypeRef(new ShaderAttribComponent(index));
		}

		public void BindAttrib(EntityRef<ShaderProgramComponent> program, EntityRef<ShaderAttribComponent> attrib)
		{
			program.Entity.GetChild("Attribs").AddLink(attrib.Component.Name, attrib);
			GL.BindAttribLocation(program.Component.Handle, attrib.Component.Index, attrib.Component.Name);
		}

		#endregion
	}
}


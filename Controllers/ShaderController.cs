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

		public EntityRef<ShaderComponent, GLHandleComponent> CreateShader(ShaderType type)
		{
			var handle = GL.CreateShader(type);
			return new Entity(Game).AddTypeRef(
				new ShaderComponent(type),
				new GLHandleComponent(handle));
		}

		public void SetShaderSource(EntityRef<ShaderComponent, GLHandleComponent> shader, string source)
		{
			GL.ShaderSource(shader.Second.Value, source);
			shader.First.Source = source;
		}

		public void CompileShader(EntityRef<ShaderComponent, GLHandleComponent> shader)
		{
			GL.CompileShader(shader.Second.Value);

			int compileStatus;
			GL.GetShader(shader.Second.Value, ShaderParameter.CompileStatus, out compileStatus);

			shader.First.Compiled = (compileStatus > 0);
			shader.Entity.GetOrCreate<InfoLogComponent>().Value = GL.GetShaderInfoLog(shader.Second.Value);
		}

		public void DeleteShader(EntityRef<ShaderComponent, GLHandleComponent> shader)
		{
			GL.DeleteShader(shader.Second.Value);
		}


		public EntityRef<ShaderComponent, GLHandleComponent> CreateAndCompile(ShaderType type, string source)
		{
			var shader = CreateShader(type);
			SetShaderSource(shader, source);
			CompileShader(shader);
			return shader;
		}


		[SubscribeEvent]
		void OnShaderComponentRemoved(ComponentRemovedEvent<ShaderProgramComponent> ev)
		{
			DeleteProgram(ev.Entity);
		}

		#endregion

		#region Program related

		public EntityRef<ShaderProgramComponent, GLHandleComponent> CreateProgram()
		{
			var handle = GL.CreateProgram();
			return new Entity(Game){ "Shaders", "Attribs", "Uniforms" }.AddTypeRef(
				new ShaderProgramComponent(),
				new GLHandleComponent(handle));
		}

		public void AttachShader(EntityRef<ShaderProgramComponent, GLHandleComponent> program,
		                         EntityRef<ShaderComponent, GLHandleComponent> shader)
		{
			GL.AttachShader(program.Second.Value, shader.Second.Value);
			program.Entity.GetChild("Shaders").Add(shader);
		}

		public void LinkProgram(EntityRef<ShaderProgramComponent, GLHandleComponent> program)
		{
			GL.LinkProgram(program.Second.Value);

			int linkStatus;
			GL.GetProgram(program.Second.Value, GetProgramParameterName.LinkStatus, out linkStatus);

			program.First.Linked = (linkStatus > 0);
			program.Entity.GetOrCreate<InfoLogComponent>().Value = GL.GetProgramInfoLog(program.Second.Value);
		}

		public void UseProgram(EntityRef<ShaderProgramComponent, GLHandleComponent> program)
		{
			GL.UseProgram(program.Second.Value);
		}

		public void DeleteProgram(EntityRef<ShaderProgramComponent, GLHandleComponent> program)
		{
			GL.DeleteProgram(program.Second.Value);
		}

		#endregion

		#region Attrib related

		public EntityRef<ShaderAttribComponent> DefineAttrib(string name, int index)
		{
			return new Entity(Game){ new NameComponent { Value = name } }
				.AddTypeRef(new ShaderAttribComponent(index));
		}

		public void BindAttrib(EntityRef<ShaderProgramComponent, GLHandleComponent> program,
		                       EntityRef<ShaderAttribComponent> attrib)
		{
			var name = attrib.Entity.GetOrThrow<NameComponent>().Value;
			program.Entity.GetChild("Attribs").AddLink(name, attrib);
			GL.BindAttribLocation(program.Second.Value, attrib.Component.Index, name);
		}

		#endregion
	}
}


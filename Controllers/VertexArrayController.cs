using System;
using OpenTK.Graphics.OpenGL4;
using OtherEngine.Core;
using OtherEngine.Core.Components;
using OtherEngine.Core.Hierarchy;
using OtherEngine.Core.Tracking;
using OtherEngine.Graphics.Components;

namespace OtherEngine.Graphics.Controllers
{
	public class VertexArrayController : Controller
	{
		[TrackComponent]
		public EntityCollection<VertexArrayComponent> VertexArrays { get; private set; }


		public EntityRef<VertexArrayComponent, GLHandleComponent> CurrentVertexArray { get; private set; }


		public EntityRef<VertexArrayComponent, GLHandleComponent> Generate()
		{
			int handle = GL.GenVertexArray();
			return new Entity(Game).AddTypeRef(
				new VertexArrayComponent(),
				new GLHandleComponent(handle));
		}

		public void Bind(EntityRef<VertexArrayComponent, GLHandleComponent> vertexArray)
		{
			GL.BindVertexArray(vertexArray.Second.Value);
			CurrentVertexArray = vertexArray;
		}

		public void Unbind()
		{
			if (CurrentVertexArray == null)
				throw new IndexOutOfRangeException("No vertex array currently bound");

			GL.BindVertexArray(0);
			CurrentVertexArray = null;
		}

		public void BindBuffer(EntityRef<BufferComponent, GLHandleComponent> buffer)
		{
			if (CurrentVertexArray == null)
				throw new IndexOutOfRangeException("No vertex array currently bound");
			
			GL.BindBuffer(buffer.First.Target, buffer.Second.Value);
			CurrentVertexArray.Entity.AddLink(buffer);
		}

		public void BindBufferToAttrib(EntityRef<BufferComponent, GLHandleComponent> buffer,
		                               EntityRef<ShaderAttribComponent> attrib)
		{
			if (CurrentVertexArray == null)
				throw new IndexOutOfRangeException("No vertex array currently bound");

			var name = attrib.Entity.GetOrThrow<NameComponent>().Value;
			GL.EnableVertexAttribArray(attrib.Component.Index);
			GL.BindBuffer(buffer.First.Target, buffer.Second.Value);
			GL.VertexAttribPointer(attrib.Component.Index, buffer.First.Size, buffer.First.PointerType, false, 0, 0);
			CurrentVertexArray.Entity.AddLink(name, buffer);
		}
	}
}


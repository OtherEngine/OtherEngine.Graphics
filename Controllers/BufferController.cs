using OtherEngine.Core;
using OtherEngine.Core.Tracking;
using OtherEngine.Graphics.Components;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Events;
using OpenTK;

namespace OtherEngine.Graphics.Controllers
{
	[AutoEnable]
	public class BufferController : Controller
	{
		[TrackComponent]
		public EntityCollection<BufferComponent> Buffers { get; private set; }


		public EntityRef<BufferComponent, GLHandleComponent> Generate(BufferTarget target)
		{
			int handle = GL.GenBuffer();
			return new Entity(Game).AddTypeRef(
				new BufferComponent(target),
				new GLHandleComponent(handle));
		}

		public void Bind(EntityRef<BufferComponent, GLHandleComponent> buffer)
		{
			GL.BindBuffer(buffer.First.Target, buffer.Second.Value);
		}

		public void Unbind(EntityRef<BufferComponent, GLHandleComponent> buffer)
		{
			GL.BindBuffer(buffer.First.Target, 0);
		}

		#region Data method definition and overloads

		public void Data<T>(EntityRef<BufferComponent, GLHandleComponent> buffer, T[] data,
		                    VertexAttribPointerType pointerType, int size) where T : struct
		{
			buffer.First.PointerType = pointerType;
			buffer.First.Size = size;

			Bind(buffer);
			var bufferSize = Marshal.SizeOf<T>() * data.Length;
			GL.BufferData<T>(buffer.First.Target, new IntPtr(bufferSize), data, BufferUsageHint.StaticDraw);
			Unbind(buffer);
		}

		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, float[] data)
		{
			Data(buffer, data, VertexAttribPointerType.Float, 1);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector2[] data)
		{
			Data(buffer, data, VertexAttribPointerType.Float, 2);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector3[] data)
		{
			Data(buffer, data, VertexAttribPointerType.Float, 3);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector4[] data)
		{
			Data(buffer, data, VertexAttribPointerType.Float, 4);
		}

		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, double[] data)
		{
			Data(buffer, data, VertexAttribPointerType.Double, 1);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector2d[] data)
		{
			Data(buffer, data, VertexAttribPointerType.Double, 2);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector3d[] data)
		{
			Data(buffer, data, VertexAttribPointerType.Double, 3);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector4d[] data)
		{
			Data(buffer, data, VertexAttribPointerType.Double, 4);
		}

		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Half[] data)
		{
			Data(buffer, data, VertexAttribPointerType.HalfFloat, 1);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector2h[] data)
		{
			Data(buffer, data, VertexAttribPointerType.HalfFloat, 2);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector3h[] data)
		{
			Data(buffer, data, VertexAttribPointerType.HalfFloat, 3);
		}
		public void Data(EntityRef<BufferComponent, GLHandleComponent> buffer, Vector4h[] data)
		{
			Data(buffer, data, VertexAttribPointerType.HalfFloat, 4);
		}

		#endregion

		public void Delete(EntityRef<BufferComponent, GLHandleComponent> buffer)
		{
			GL.DeleteBuffer(buffer.Second.Value);
		}


		public EntityRef<BufferComponent, GLHandleComponent> GenerateAndData<T>(BufferTarget target, T[] data) where T : struct
		{
			var buffer = Generate(target);
			Data<T>(buffer, data);
			return buffer;
		}


		[SubscribeEvent]
		void OnBufferComponentRemoved(ComponentRemovedEvent<BufferComponent> ev)
		{
			Delete(ev.Entity);
		}
	}
}


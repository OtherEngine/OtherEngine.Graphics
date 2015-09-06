using OtherEngine.Core;
using OtherEngine.Core.Tracking;
using OtherEngine.Graphics.Components;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Events;

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

		public void Data<T>(EntityRef<BufferComponent, GLHandleComponent> buffer, T[] data) where T : struct
		{
			Bind(buffer);
			var size = Marshal.SizeOf<T>() * data.Length;
			GL.BufferData<T>(buffer.First.Target, new IntPtr(size), data, BufferUsageHint.StaticDraw);
			Unbind(buffer);
		}

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


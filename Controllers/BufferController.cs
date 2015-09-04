using OtherEngine.Core;
using OtherEngine.Core.Tracking;
using OtherEngine.Graphics.Components;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;
using OtherEngine.Core.Attributes;

namespace OtherEngine.Graphics.Controllers
{
	[AutoEnable]
	public class BufferController : Controller
	{
		[TrackComponent]
		public EntityCollection<BufferComponent> Buffers { get; private set; }


		public EntityRef<BufferComponent> Generate(BufferTarget target)
		{
			int handle = GL.GenBuffer();
			return new Entity(Game).AddTypeRef(new BufferComponent(handle, target));
		}

		public void Bind(EntityRef<BufferComponent> buffer)
		{
			GL.BindBuffer(buffer.Component.Target, buffer.Component.Handle);
		}

		public void Unbind(EntityRef<BufferComponent> buffer)
		{
			GL.BindBuffer(buffer.Component.Target, 0);
		}

		public void Data<T>(EntityRef<BufferComponent> buffer, T[] data) where T : struct
		{
			Bind(buffer);
			var size = Marshal.SizeOf<T>() * data.Length;
			GL.BufferData<T>(buffer.Component.Target, new IntPtr(size), data, BufferUsageHint.StaticDraw);
			Unbind(buffer);
		}

		public void Delete(EntityRef<BufferComponent> buffer)
		{
			GL.DeleteBuffer(buffer.Component.Handle);
		}


		public EntityRef<BufferComponent> GenerateAndData<T>(BufferTarget target, T[] data) where T : struct
		{
			var buffer = Generate(target);
			Data<T>(buffer, data);
			return buffer;
		}
	}
}


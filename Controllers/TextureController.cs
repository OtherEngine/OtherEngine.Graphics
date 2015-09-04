using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OtherEngine.Core;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Components;
using OtherEngine.Core.Events;
using OtherEngine.Core.Tracking;
using OtherEngine.Graphics.Components;
using Imaging = System.Drawing.Imaging;

namespace OtherEngine.Graphics.Controllers
{
	[AutoEnable]
	public class TextureController : Controller
	{
		[TrackComponent]
		public EntityCollection<TextureComponent> Textures { get; private set; }


		public EntityRef<TextureComponent> Load(string file)
		{
			if (file == null)
				throw new ArgumentNullException("file");
			
			var bmp = new Bitmap(file);
			var data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
				Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb);

			var id = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, id);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

			GL.TexImage2D(TextureTarget.Texture2D, 0,
				PixelInternalFormat.Rgba, data.Width, data.Height, 0,
				PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

			bmp.UnlockBits(data);

			return new Entity(Game).AddTypeRef(new TextureComponent(id));
		}

		public void Bind(EntityRef<TextureComponent> texture)
		{
			GL.BindTexture(TextureTarget.Texture2D, texture.Component.Handle);
		}

		public void Release(EntityRef<TextureComponent> texture)
		{
			texture.Entity.Remove(texture.Component);
		}


		[SubscribeEvent]
		void OnTextureComponentRemoved(ComponentRemovedEvent<TextureComponent> ev)
		{
			GL.DeleteTexture(ev.Component.Handle);
		}
	}
}


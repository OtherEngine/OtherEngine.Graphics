using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OtherEngine.Core;
using OtherEngine.Core.Attributes;
using OtherEngine.Core.Events;
using OtherEngine.Graphics.Components;
using Imaging = System.Drawing.Imaging;

namespace OtherEngine.Graphics.Controllers
{
	[AutoEnable]
	public class TextureController : Controller
	{
		[TrackComponent(typeof(TextureComponent))]
		public IReadOnlyCollection<Entity> Textures { get; private set; }

		public Entity Load(string file)
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

			return new Entity(Game){ new TextureComponent(id) };
		}

		public void Bind(Entity textureEntity)
		{
			if (textureEntity == null)
				throw new ArgumentNullException("textureEntity");
			var component = textureEntity.GetOrThrow<TextureComponent>();

			GL.BindTexture(TextureTarget.Texture2D, component.ID);
		}

		public void Release(Entity textureEntity)
		{
			if (textureEntity == null)
				throw new ArgumentNullException("textureEntity");
			var component = textureEntity.GetOrThrow<TextureComponent>();

			textureEntity.Remove(component);
		}

		[SubscribeEvent]
		void OnTextureComponentRemoved(ComponentRemovedEvent<TextureComponent> ev)
		{
			GL.DeleteTexture(ev.Component.ID);
		}
	}
}


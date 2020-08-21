using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorEngine.Contents
{
    public class Drawer : IDisposable
    {
        public Vector2 OffsetsVector;
        public Rectangle OffsetsRectangle;
        public SpriteBatch spriteBatch;
        public Drawer paternalDrawer = null;
        private bool isVisible = true;
        private bool isDisponse = false;

        public bool IsVisible { get => isVisible; }

        public Drawer(Vector2 offsetsVector, Rectangle offsetsRectangle, SpriteBatch spriteBatch, Drawer paternalDrawer = null)
        {
            OffsetsVector = offsetsVector;
            OffsetsRectangle = offsetsRectangle;
            this.spriteBatch = spriteBatch;
            this.paternalDrawer = paternalDrawer;
        }



        //
        // Сводка:
        //     Submit a sprite for drawing in the current batch.
        //
        // Параметры:
        //   texture:
        //     A texture.
        //
        //   destinationRectangle:
        //     The drawing bounds on screen.
        //
        //   sourceRectangle:
        //     An optional region on the texture which will be rendered. If null - draws full
        //     texture.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this sprite.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this sprite.
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            if (!isDisponse)
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        public void Draw(Texture2D texture, Vector2 position, Point? size, float layerDepth)
        {
            Draw(texture, position, size, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
        }


        //
        // Сводка:
        //     Submit a sprite for drawing in the current batch.
        //
        // Параметры:
        //   texture:
        //     A texture.
        //
        //   destinationRectangle:
        //     The drawing bounds on screen.
        //
        //   sourceRectangle:
        //     An optional region on the texture which will be rendered. If null - draws full
        //     texture.
        //
        //   color:
        //     A color mask.
        //
        //   rotation:
        //     A rotation of this sprite.
        //
        //   origin:
        //     Center of the rotation. 0,0 by default.
        //
        //   effects:
        //     Modificators for drawing. Can be combined.
        //
        //   layerDepth:
        //     A depth of the layer of this sprite.
        public void Draw(Texture2D texture, Vector2 position, Point? size, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            if(size == null)
            {
                size = new Point(texture.Width, texture.Height);
            }



            Rectangle destinationRectangle = new Rectangle((OffsetsVector + position).ToPoint(), new Point(size.Value.X, size.Value.Y));
            Rectangle sourceRectangle = new Rectangle(Point.Zero, new Point(texture.Width, texture.Height));

            DrawSettings drawSettings = new DrawSettings(texture, position, size, color, rotation, origin, effects, layerDepth, this, paternalDrawer);

            if (drawSettings.IsVisible)
                drawSettings.Draw();

            isVisible = drawSettings.IsVisible;
        }

        public void Dispose()
        {
            isDisponse = true;
        }
    }
}

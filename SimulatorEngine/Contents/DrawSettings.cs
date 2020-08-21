using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorEngine.Contents
{
    public class DrawSettings
    {
        Texture2D texture;
        Vector2 position;
        Point? size;
        Color color;
        float rotation;
        Vector2 origin;
        SpriteEffects effects;
        float layerDepth;

        Rectangle destinationRectangle;
        Rectangle sourceRectangle;

        Drawer drawer;
        Drawer paternalDrawer;

        bool isVisible = true;

        public DrawSettings(Texture2D texture, Vector2 position, Point? size, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth, Drawer drawer, Drawer paternalDrawer)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.color = color;
            this.rotation = rotation;
            this.origin = origin;
            this.effects = effects;
            this.layerDepth = layerDepth;
            this.drawer = drawer;
            this.paternalDrawer = paternalDrawer;

            int w;
            int h;

            if (size == null)
            {
                w = texture.Width;
                h = texture.Height;
            }
            else
            {
                w = size.Value.X;
                h = size.Value.Y;
            }



            destinationRectangle = new Rectangle((drawer.OffsetsVector + position).ToPoint(), new Point(w, h));
            sourceRectangle = new Rectangle(Point.Zero, new Point(texture.Width, texture.Height));

            if (paternalDrawer == null)
                return;

            int Up = 0, Right = 0, Down = 0, Left = 0;





            Vector2 UpLeftVector = (drawer.OffsetsVector - paternalDrawer.OffsetsVector);

            if (UpLeftVector.Y <= 0f)
                Up = (int)Math.Abs(UpLeftVector.Y);
            if (UpLeftVector.X <= 0f)
                Left = (int)Math.Abs(UpLeftVector.X);

            Vector2 DownRightVector = paternalDrawer.OffsetsRectangle.Size.ToVector2() - new Vector2(UpLeftVector.X + w, UpLeftVector.Y + h);

            if (DownRightVector.X <= 0f)
                Right = (int)Math.Abs(DownRightVector.X);
            if (DownRightVector.Y <= 0f)
                Down = (int)Math.Abs(DownRightVector.Y);

            RemoveSides(Up, Right, Down, Left);

            if (Math.Max(Up, Down) >= h || Math.Max(Right, Left) >= w)
            {
                isVisible = false;
            }
            else
            {
                isVisible = true;
            }
        }

        float dsMultX;
        float dsMultY;

        public bool IsVisible { get => isVisible; }

        private void RemoveSides(int Up, int Right, int Down, int Left)
        {
            //R.I.P. MEGA BUG   xDDD


            dsMultX = (float)sourceRectangle.Size.X / (float)destinationRectangle.Size.X;
            dsMultY = (float)sourceRectangle.Size.Y / (float)destinationRectangle.Size.Y;


            //Искажатель
            destinationRectangle = new Rectangle((drawer.OffsetsVector + position).ToPoint(),
                new Point(//size
                    x: destinationRectangle.Width - Left - Right,
                    y: destinationRectangle.Height - Up - Down
                    ));

            //Гашение смещение текстуры
            destinationRectangle.Location = new Point(
                    destinationRectangle.Location.X + Left,
                    destinationRectangle.Location.Y + Up
                );

            //Обрезка текстуры
            sourceRectangle = new Rectangle(
                new Point(//смещение текстуры
                    (int)Math.Round((float)(Left) * dsMultX),
                    (int)Math.Round((float)(Up) * dsMultY)
                ),
                new Point(//size
                    x: sourceRectangle.Width - (int)Math.Round((float)(Left + Right) * dsMultX),
                    y: sourceRectangle.Height - (int)Math.Round((float)(Up + Down) * dsMultY)
                    ));



        }

        public void Draw()
        {
            drawer.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
        }
    }
}

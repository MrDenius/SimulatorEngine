using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimulatorEngine.Logic;
using SimulatorEngine.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorEngine.Contents
{
    public abstract class Content : IDisposable
    {
        private static List<Content> contents = new List<Content>();

        public abstract Texture2D BackgroundTexture { get; }
        public static List<Content> Contents {
            get 
            {
                lock (LContent)
                {
                    List<Content> re = new List<Content>();
                    re.AddRange(contents);
                    return re;
                }
            } 
        }

        public Drawer Drawer;
        public Content paternalContent;
        private bool isDisponse = false;


        static object LContent = new object();
        protected Content(Vector2 OffsetsVector, Rectangle OffsetsRectangle, SpriteBatch spriteBatch, Content paternalContent = null)
        {
            lock (LContent)
            {
                Drawer = new Drawer(OffsetsVector, OffsetsRectangle, spriteBatch);
                if (paternalContent != null)
                {
                    paternalContent.InnerContent.Add(this);
                    this.paternalContent = paternalContent;
                    Drawer.paternalDrawer = paternalContent.Drawer;
                }



                //Групировка экземпляра
                if (this is Content)
                {
                    contents.Add(this);
                }
                if (this is IEntity)
                {
                    Entity.entities.Add((IEntity)this);
                }
                if (this is IMovable)
                {
                    Movable.movables.Add((IMovable)this);
                }
            }
        }

        internal List<Content> InnerContent = new List<Content>();

        public abstract void DrawContent(Drawer Drawer);

        public void Draw(float ld = 0f)
        {
            //Первый ректенгл = искажение исходника
            //Второй ректенгл = изменение (обрезка) исхлдника

            //Drawer.Draw(BackgroundTexture, new Rectangle(400-64,240-64,128,128), new Rectangle(0,0,128,128), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);

            //f += 0.01f;
            //Drawer.Draw(BackgroundTexture, new Rectangle(200,200,128,100), new Rectangle(0,28,128,100), Color.White, f, new Vector2(0,0), SpriteEffects.None, 0);
            Drawer.Draw(BackgroundTexture, Vector2.Zero, Drawer.OffsetsRectangle.Size, Color.White, 0f, Vector2.Zero, SpriteEffects.None, ld);

            DrawContent(Drawer);



            foreach(Content content in InnerContent)
            {
                content.Draw(ld+1f);
            }
        }


        //EVENTS
        public delegate void ContentActivity(MouseState mouseState, MouseState LastMouseState);
        public event ContentActivity LBMPressed;
        public event ContentActivity LBMDown;
        public event ContentActivity LBMUp;
        public event ContentActivity RBMPressed;
        public event ContentActivity RBMDown;
        public event ContentActivity RBMUp;

        public void MouseHandler(MouseState mouseState, MouseState LastMouseState)
        {
            if (isDisponse)
            {
                return;
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                LBMPressed?.Invoke(mouseState, LastMouseState);
            }
            if (mouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released)
            {
                LBMDown?.Invoke(mouseState, LastMouseState);
            }
            if (mouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed)
            {
                LBMUp?.Invoke(mouseState, LastMouseState);
            }


            if (mouseState.RightButton == ButtonState.Pressed)
            {
                RBMPressed?.Invoke(mouseState, LastMouseState);
            }
            if (mouseState.RightButton == ButtonState.Pressed && LastMouseState.RightButton == ButtonState.Released)
            {
                RBMDown?.Invoke(mouseState, LastMouseState);
            }
            if (mouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed)
            {
                RBMUp?.Invoke(mouseState, LastMouseState);
            }
        }




        public void Dispose()
        {
            isDisponse = true;

            foreach (Content content in Contents)
            {
                if (content.paternalContent == this)
                {
                    Dispose();
                }
            }


            if (this is Content)
            {
                Contents.Remove(this);
            }
            if (this is IEntity)
            {
                Entity.entities.Remove((IEntity)this);
            }
            if (this is IMovable)
            {
                Movable.movables.Remove((IMovable)this);
            }
            Drawer.Dispose();
        }
    }
}

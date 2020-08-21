using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorEngine.Contents
{
    public class MainPanel : Content
    {
        public MainPanel(Vector2 OffsetsVector, Rectangle OffsetsRectangle, SpriteBatch spriteBatch, Content paternalContent = null) : base(OffsetsVector, OffsetsRectangle, spriteBatch, paternalContent)
        {
        }

        public override Texture2D BackgroundTexture => Textures.Get("MainBackground");


        public override void DrawContent(Drawer Drawer)
        {
            
        }
    }
}

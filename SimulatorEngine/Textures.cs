using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorEngine
{
    public static class Textures
    {
        static List<string> name = new List<string>();
        static List<Texture2D> texture = new List<Texture2D>();

        public static void Add(string textureName, Texture2D texture2D)
        {
            name.Add(textureName);
            texture.Add(texture2D);
        }

        public static Texture2D Get(string name)
        {
            return texture[Textures.name.IndexOf(name)];
        }

        public static void Remove(string name)
        {
            texture.RemoveAt(Textures.name.IndexOf(name));
            Textures.name.Remove(name);
        }
    }
}

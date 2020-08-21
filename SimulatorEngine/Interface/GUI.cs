using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimulatorEngine.Contents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SimulatorEngine.Interface
{
    public class GUI
    {
        Size windowSize;

        public MainPanel mainPanel;
        public AdditionalPanel additionalPanel;

        Vector2 additionalPanelVector;
        Rectangle additionalPanelRectangle;


        public GUI(Size windowSize, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            this.windowSize = windowSize;

            float margin = (float)(windowSize.Height * 0.05);

            //mainPanel = new MainPanel(Vector2.Zero, new Rectangle(0, 0, windowSize.Width, windowSize.Height), spriteBatch);
            mainPanel = new MainPanel(new Vector2(0,0), new Rectangle(0, 0, windowSize.Width-0, windowSize.Height-0), spriteBatch);

            additionalPanelVector = new Vector2((float)(mainPanel.Drawer.OffsetsRectangle.Width - (mainPanel.Drawer.OffsetsRectangle.Width * 0.2)), margin);
            additionalPanelRectangle = new Rectangle(0, 0, mainPanel.Drawer.OffsetsRectangle.Width - (int)additionalPanelVector.X - (int)margin, mainPanel.Drawer.OffsetsRectangle.Height - ((int)margin * 2));

            additionalPanel = new AdditionalPanel(additionalPanelVector, additionalPanelRectangle, spriteBatch, mainPanel);

            additionalPanel.LBMPressed += (mouseState, lastMouseState) =>
            {
                Vector2 VectorDifference = (lastMouseState.Position - mouseState.Position).ToVector2();
                Debug.WriteLine(VectorDifference);
                additionalPanel.Drawer.OffsetsVector = additionalPanel.Drawer.OffsetsVector - VectorDifference;
            };

            List<TestItem> testItems = new List<TestItem>();
            mainPanel.RBMUp += (mouseState, lastMouseState) =>
            {
                TestItem testItem = new TestItem(mouseState.Position.ToVector2() - new Vector2(16.16f), new Rectangle(Point.Zero, new Point(32, 32)), spriteBatch, mainPanel);
                testItems.Add(testItem);
                Debug.WriteLine(testItems.Count);


                testItem.LBMUp += (_mouseState, _lastMouseState) =>
                {
                    mouseState = _mouseState;
                    lastMouseState = _lastMouseState;

                    Debug.WriteLine("DISPONSE");
                    testItems.Remove(testItem);
                    testItem.Dispose();
                };
            };
        }

        public void Draw()
        {
            mainPanel.Draw();
        }

        public void Move(Vector2 vector)
        {

        }
    }
}

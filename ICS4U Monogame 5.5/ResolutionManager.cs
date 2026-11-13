using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS4U_Monogame_5._5
{
    internal class ResolutionManager
{
        private readonly int _virtualWidth;
        private readonly int _virtualHeight;

        public int VirtualWidth {  get { return _virtualWidth; } }
        public int VirtualHeight { get { return _virtualHeight; } }


        public ResolutionManager(int virtualWidth, int virtualHeight)
        {
            _virtualWidth = virtualWidth;
            _virtualHeight = virtualHeight;
        }

        public Matrix GetTransformationMatrix(GraphicsDevice graphicsDevice)
        {
            float windowWidth = graphicsDevice.PresentationParameters.BackBufferWidth;
            float windowHeight = graphicsDevice.PresentationParameters.BackBufferHeight;

            float scaleX = windowWidth / _virtualWidth;
            float scaleY = windowHeight / _virtualHeight;

            float uniformScale = Math.Min(scaleX, scaleY);

            float offsetX = (windowWidth - _virtualWidth * uniformScale) * 0.5f;
            float offsetY = (windowHeight - _virtualHeight * uniformScale) * 0.5f;

            return Matrix.CreateScale(uniformScale, uniformScale, 1.0f) * Matrix.CreateTranslation(offsetX, offsetY, 0f);

        }
    }
}

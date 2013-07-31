using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Common.Input;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace HelloWaveProject
{
    class CubeBehavior : Behavior
    {

        [RequiredComponent]
        public Spinner spinner;

        public CubeBehavior() : base("CubeBehavior")
        {
            spinner = null;
        }

        protected override void Update(TimeSpan gameTime)
        {
            // touch panel
            // Keyboard
            var keyboard = WaveServices.Input.KeyboardState;
            if (keyboard.Right == ButtonState.Pressed)
            {
                spinner.IncreaseX += 1;
            }
            if (keyboard.Right == ButtonState.Release)
            {
                if (spinner.IncreaseX > 0)
                {
                    spinner.IncreaseX -= 1;
                }
              spinner.IncreaseX = 0;
            }
            if (keyboard.Left == ButtonState.Pressed)
            {
                spinner.IncreaseY += 1;
            }
            if (keyboard.Left == ButtonState.Release)
            {
                if (spinner.IncreaseY > 0)
                {
                    spinner.IncreaseY -= 1;
                }
              spinner.IncreaseY = 0;
            }
            
        }
    }
}

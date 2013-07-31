using System;
using System.IO;
using System.Reflection;
using HelloWaveProject;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Input;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.Services;

namespace HelloWave
{
    public class App : WaveEngine.Adapter.Application
    {
        HelloWaveProject.Game game;
        SpriteBatch spriteBatch;
        Texture2D splashScreen;
        bool splashState = true;
        TimeSpan time;
        Vector2 position;
        Color backgroundSplashColor;

        public App()
        {
            this.Width = 800;
            this.Height = 600;
            this.FullScreen = false;
            this.WindowTitle = "HelloWave";
        }

        public override void Initialize()
        {
            this.game = new HelloWaveProject.Game();
            this.game.Initialize(this.Adapter);

         
            this.backgroundSplashColor = new Color(32, 32, 32, 255);
            this.spriteBatch = new SpriteBatch(WaveServices.GraphicsDevice);

            var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string name = string.Empty;

            foreach (string item in resourceNames)
            {
                if (item.Contains("SplashScreen.wpk"))
                {
                    name = item;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidProgramException("License terms not agreed.");
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                this.splashScreen = WaveServices.Assets.Global.LoadAsset<Texture2D>(name, stream);
            }

            position = new Vector2();
            position.X = (this.Width / 2.0f) - (this.splashScreen.Width / 2.0f);
            position.Y = (this.Height / 2.0f) - (this.splashScreen.Height / 2.0f);
        }

        public override void Update(TimeSpan elapsedTime)
        {
            if (this.game != null && !this.game.HasExited)
            {
                if (WaveServices.Input.KeyboardState.F10 == ButtonState.Pressed)
                {
                    this.FullScreen = !this.FullScreen;
                }

                if (this.splashState)
                {
                    this.time += elapsedTime;
                    if (time > TimeSpan.FromSeconds(2))
                    {
                        this.splashState = false;
                    }
                }
                else
                {
                    if (WaveServices.Input.KeyboardState.Escape == ButtonState.Pressed)
                    {
                        this.Exit();
                        this.game.Unload();
                    }
                    else
                    {
                        this.game.UpdateFrame(elapsedTime);
                    }
                }
            }
        }

        public override void Draw(TimeSpan elapsedTime)
        {
            if (this.game != null && !this.game.HasExited)
            {
                if (this.splashState)
                {
                    WaveServices.GraphicsDevice.RenderTargets.SetRenderTarget(null);
                    WaveServices.GraphicsDevice.Clear(ref this.backgroundSplashColor, ClearFlags.Target, 1);
                    this.spriteBatch.Begin();
                    this.spriteBatch.Draw(this.splashScreen, this.position, Color.White);
                    this.spriteBatch.End();
                }
                else
                {
                    this.game.DrawFrame(elapsedTime);
                }
            }
        }
    }
}


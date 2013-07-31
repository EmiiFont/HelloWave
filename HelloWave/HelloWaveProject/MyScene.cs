using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.Services;
using WaveEngine.Components;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Materials;
using WaveEngine.Components.Cameras;
using WaveEngine.Framework.Graphics;
using Random = System.Random;

namespace HelloWaveProject
{
    public class MyScene : Scene
    {

        protected override void CreateScene()
        {
            this.AddSceneBehavior(new MainSceneBehavior("PruebaCubo", this.EntityManager), new SceneBehavior.Order());

            RenderManager.BackgroundColor = Color.White;
            var camera = new FreeCamera("Free Camera", Vector3.One * 1.9f, Vector3.Zero);
            EntityManager.Add(camera);

            var floor = new Entity("LargeCube")
                .AddComponent(Model.CreateCube())
                .AddComponent(new MaterialsMap(new BasicMaterial("Content/crate.wpk")))
                .AddComponent(new Transform3D() { Scale = new Vector3(1, 1, 1), Position = new Vector3(0, -1, 0) })
                .AddComponent(new BoxCollider())
                .AddComponent(new ModelRenderer())
                .AddComponent(new RigidBody3D() { IsKinematic = true });

            EntityManager.Add(floor);

            var rand = new Random();

            var cube = new Entity("Cube")
                .AddComponent(Model.CreateCube())
                .AddComponent(new MaterialsMap(new BasicMaterial("Content/crate.wpk")))
                .AddComponent(new Transform3D()
                {
                    Scale = Vector3.One / 2f,
                    Position = new Vector3(0, 2, 0),
                })
                .AddComponent(new BoxCollider())
                .AddComponent(new ModelRenderer())
                .AddComponent(new RigidBody3D() { IsKinematic = false })
                .AddComponent(new Spinner())
                .AddComponent(new CubeBehavior());

            EntityManager.Add(cube);
        }
    }
}

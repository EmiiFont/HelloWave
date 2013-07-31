using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Common.Input;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Materials;

namespace HelloWaveProject
{
    class MainSceneBehavior : SceneBehavior
    {
        public static int CubeNumber = 1;
        private EntityManager _entityEntityManager;

        public MainSceneBehavior(string name) : base(name)
        {
        }

        public MainSceneBehavior(string name, EntityManager entityManager) : this(name)
        {
            _entityEntityManager = entityManager;
        }

        protected override void ResolveDependencies()
        {
            
        }

        protected override void Update(TimeSpan gameTime)
        {
            if (WaveEngine.Framework.Services.WaveServices.Input.MouseState.LeftButton == ButtonState.Pressed)
            {
                var cube = new Entity("Cube" + CubeNumber++)
                    .AddComponent(Model.CreateCube())
                    .AddComponent(new MaterialsMap(new BasicMaterial("Content/crate.wpk")))
                    .AddComponent(new Transform3D()
                        {
                            Scale = Vector3.One/2f,
                            Position = new Vector3(0, 2, 0),
                        })
                    .AddComponent(new BoxCollider())
                    .AddComponent(new ModelRenderer())
                    .AddComponent(new RigidBody3D() {IsKinematic = false})
                    .AddComponent(new Spinner());

                _entityEntityManager.Add(cube);
            }
        }
    }
}

#region File Description
//-----------------------------------------------------------------------------
// Game1.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game4D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        Vector3 camera;
        float angle = 0;

        float w = 0;

        IPrimitive[] primitives = new IPrimitive[4];

        VertexDeclaration vertexDeclaration;
        Matrix View, Projection;
        protected override void Initialize()
        {
            primitives[0] = new Cube3D(new Vector3(-2, 1, -4), new Vector3(4, 2, 5));
            primitives[1] = new Cube3D(new Vector3(-2, -1, -4), new Vector3(1, 2, 5));
            primitives[2] = new Cube3D(new Vector3(1, -1, -4), new Vector3(1, 2, 5));

            primitives[3] = new Quad4D(
                new Vector4(-1, -1, 1, 0), new Vector4(-1, -1, -4, 1),
                new Vector4(-1, 1, 1, 0), new Vector4(-1, 1, -4, 1),
                new Vector4(1, -1, 1, 0), new Vector4(1, -1, -4, 1),
                new Vector4(1, 1, 1, 0), new Vector4(1, 1, -4, 1)
                );

            camera = new Vector3(0, 0, 5);
            View = Matrix.CreateLookAt(camera, Vector3.Zero,
                Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 4.0f / 3.0f, 1, 500);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        BasicEffect quadEffect;
        Texture2D texture;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Glass");
            quadEffect = new BasicEffect(graphics.GraphicsDevice);
            quadEffect.EnableDefaultLighting();

            quadEffect.World = Matrix.Identity;
            quadEffect.View = View;
            quadEffect.Projection = Projection;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = texture;

            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                }
            );
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

#if WINDOWS
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                angle += 0.05f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                angle -= 0.05f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camera = new Vector3(0, 0, camera.Z - 0.05f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camera = new Vector3(0, 0, camera.Z + 0.05f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                w = Math.Min(w + 0.05f, 1);

                foreach (var cube in primitives.OfType<IPrimitive4D>())
                {
                    cube.Project(w);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                w = Math.Max(w - 0.05f, 0);

                foreach (var cube in primitives.OfType<IPrimitive4D>())
                {
                    cube.Project(w);
                }
            }
#endif

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            quadEffect.View = Matrix.CreateRotationY(angle) * Matrix.CreateLookAt(camera, Vector3.Zero, Vector3.Up);

            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                foreach (var cube in primitives)
                {
                    cube.Render(GraphicsDevice);
                }
            }

            base.Draw(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game4D
{
    public class Quad3D : IPrimitive
    {
        /* My current 3d projection */
        protected VertexPositionNormalTexture[] vertices;
        protected short[] indices;

        protected Quad3D()
        {
            vertices = new VertexPositionNormalTexture[4];
            indices = new short[6];
        }

        public Quad3D(
            Vector3 a,
            Vector3 b,
            Vector3 c,
            Vector3 d) : this()
        {
            vertices[0].Position = a;
            vertices[1].Position = b;
            vertices[2].Position = c;
            vertices[3].Position = d;

            var normal = Vector3.Cross(vertices[1].Position - vertices[0].Position, vertices[2].Position - vertices[0].Position);

            vertices[0].Normal = normal;
            vertices[1].Normal = normal;
            vertices[2].Normal = normal;
            vertices[3].Normal = normal;

            // Fill in texture coordinates to display full texture
            // on quad
            Vector2 textureUpperLeft = new Vector2(0.0f, 0.0f);
            Vector2 textureUpperRight = new Vector2(1.0f, 0.0f);
            Vector2 textureLowerLeft = new Vector2(0.0f, 1.0f);
            Vector2 textureLowerRight = new Vector2(1.0f, 1.0f);

            // Set the texture coordinate for each
            // vertex
            vertices[0].TextureCoordinate = textureLowerLeft;
            vertices[1].TextureCoordinate = textureUpperLeft;
            vertices[2].TextureCoordinate = textureLowerRight;
            vertices[3].TextureCoordinate = textureUpperRight;

            // Set the index buffer for each vertex, using
            // clockwise winding
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 2;
            indices[4] = 1;
            indices[5] = 3;
        }

        public virtual void Render(GraphicsDevice device)
        {
            device.DrawUserIndexedPrimitives
                <VertexPositionNormalTexture>(
                PrimitiveType.TriangleList,
                vertices, 0, 4,
                indices, 0, 2);
        }
    }
}

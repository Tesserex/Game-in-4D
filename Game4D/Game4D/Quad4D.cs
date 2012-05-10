using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game4D
{
    public class Quad4D
    {
        /* My "true" points */
        private Vector4 A1, A2;
        private Vector4 B1, B2;
        private Vector4 C1, C2;
        private Vector4 D1, D2;

        /* My current 3d projection */
        private VertexPositionNormalTexture[] vertices;
        private short[] indices;

        public Quad4D(
            Vector4 a1, Vector4 a2,
            Vector4 b1, Vector4 b2,
            Vector4 c1, Vector4 c2,
            Vector4 d1, Vector4 d2)
        {
            vertices = new VertexPositionNormalTexture[4];
            indices = new short[6];

            this.A1 = a1;
            this.A2 = a2;
            this.B1 = b1;
            this.B2 = b2;
            this.C1 = c1;
            this.C2 = c2;
            this.D1 = d1;
            this.D2 = d2;

            SetupVertices();
        }

        private void SetupVertices()
        {
            Project(0);

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

        public void Project(float w)
        {
            vertices[0].Position = Project(A1, A2, w);
            vertices[1].Position = Project(B1, B2, w);
            vertices[2].Position = Project(C1, C2, w);
            vertices[3].Position = Project(D1, D2, w);

            var normal = Vector3.Cross(vertices[1].Position - vertices[0].Position, vertices[2].Position - vertices[0].Position);

            vertices[0].Normal = normal;
            vertices[1].Normal = normal;
            vertices[2].Normal = normal;
            vertices[3].Normal = normal;
        }

        private Vector3 Project(Vector4 a, Vector4 b, float w)
        {
            float amount = (w - a.W) / (b.W - a.W);
            var lerp = Vector4.Lerp(a, b, amount);
            return new Vector3(lerp.X, lerp.Y, lerp.Z);
        }

        public void Render(GraphicsDevice device)
        {
            device.DrawUserIndexedPrimitives
                <VertexPositionNormalTexture>(
                PrimitiveType.TriangleList,
                vertices, 0, 4,
                indices, 0, 2);
        }
    }
}

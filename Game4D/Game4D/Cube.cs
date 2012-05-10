using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game4D
{
    public struct Cube4D
    {
        private Quad4D top, bottom, left, right, front, back;

        public Cube4D(Vector3 origin1, Vector3 origin2, float size1, float size2)
        {
            // z negative (back) points
            var a1 = new Vector4(origin1.X - size1, origin1.Y - size1, origin1.Z - size1, 0);
            var a2 = new Vector4(origin2.X - size2, origin2.Y - size2, origin2.Z - size2, 1);

            var b1 = new Vector4(origin1.X - size1, origin1.Y + size1, origin1.Z - size1, 0);
            var b2 = new Vector4(origin2.X - size2, origin2.Y + size2, origin2.Z - size2, 1);

            var c1 = new Vector4(origin1.X + size1, origin1.Y - size1, origin1.Z - size1, 0);
            var c2 = new Vector4(origin2.X + size2, origin2.Y - size2, origin2.Z - size2, 1);

            var d1 = new Vector4(origin1.X + size1, origin1.Y + size1, origin1.Z - size1, 0);
            var d2 = new Vector4(origin2.X + size2, origin2.Y + size2, origin2.Z - size2, 1);

            // z positive (front) points
            var e1 = new Vector4(origin1.X - size1, origin1.Y - size1, origin1.Z + size1, 0);
            var e2 = new Vector4(origin2.X - size2, origin2.Y - size2, origin2.Z + size2, 1);

            var f1 = new Vector4(origin1.X - size1, origin1.Y + size1, origin1.Z + size1, 0);
            var f2 = new Vector4(origin2.X - size2, origin2.Y + size2, origin2.Z + size2, 1);

            var g1 = new Vector4(origin1.X + size1, origin1.Y - size1, origin1.Z + size1, 0);
            var g2 = new Vector4(origin2.X + size2, origin2.Y - size2, origin2.Z + size2, 1);

            var h1 = new Vector4(origin1.X + size1, origin1.Y + size1, origin1.Z + size1, 0);
            var h2 = new Vector4(origin2.X + size2, origin2.Y + size2, origin2.Z + size2, 1);

            var rand = new Random();
            a1 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            a2 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            b1 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            b2 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            c1 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            c2 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            d1 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            d2 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            e1 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            e2 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            f1 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            f2 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            g1 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            g2 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            h1 *= (float)(rand.NextDouble() * 0.5 + 0.5);
            h2 *= (float)(rand.NextDouble() * 0.5 + 0.5);

            // faces
            back = new Quad4D(c1, c2, d1, d2, a1, a2, b1, b2);
            front = new Quad4D(e1, e2, f1, f2, g1, g2, h1, h2);

            top = new Quad4D(f1, f2, b1, b2, h1, h2, d1, d2);
            bottom = new Quad4D(a1, a2, e1, e2, c1, c2, g1, g2);

            left = new Quad4D(a1, a2, b1, b2, e1, e2, f1, f2);
            right = new Quad4D(g1, g2, h1, h2, c1, c2, d1, d2);
        }

        public void Project(float w)
        {
            top.Project(w);
            bottom.Project(w);
            left.Project(w);
            right.Project(w);
            front.Project(w);
            back.Project(w);
        }

        public void Render(GraphicsDevice device)
        {
            top.Render(device);
            bottom.Render(device);
            left.Render(device);
            right.Render(device);
            front.Render(device);
            back.Render(device);
        }
    }
}

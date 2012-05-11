using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game4D
{
    /// <summary>
    /// Represents a block that doesn't change in the 4th dimension.
    /// </summary>
    public class Cube3D : IPrimitive
    {
        private Quad3D top, bottom, left, right, front, back;

        public Cube3D(Vector3 origin, Vector3 size)
        {
            // z negative (back) points
            var a = new Vector3(origin.X, origin.Y, origin.Z);

            var b = new Vector3(origin.X, origin.Y + size.Y, origin.Z);

            var c = new Vector3(origin.X + size.X, origin.Y, origin.Z);

            var d = new Vector3(origin.X + size.X, origin.Y + size.Y, origin.Z);

            // z positive (front) points
            var e = new Vector3(origin.X, origin.Y, origin.Z + size.Z);

            var f = new Vector3(origin.X, origin.Y + size.Y, origin.Z + size.Z);

            var g = new Vector3(origin.X + size.X, origin.Y, origin.Z + size.Z);

            var h = new Vector3(origin.X + size.X, origin.Y + size.Y, origin.Z + size.Z);

            // faces
            back = new Quad3D(c, d, a, b);
            front = new Quad3D(e, f, g, h);

            top = new Quad3D(f, b, h, d);
            bottom = new Quad3D(a, e, c, g);

            left = new Quad3D(a, b, e, f);
            right = new Quad3D(g, h, c, d);
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

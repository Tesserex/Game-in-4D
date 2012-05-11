using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Game4D
{
    public interface IPrimitive
    {
        void Render(GraphicsDevice device);
    }

    public interface IPrimitive4D : IPrimitive
    {
        void Project(float w);
    }
}

// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Colour;
using osuTK.Graphics;

namespace osu.Framework.Graphics
{
    public struct DrawColourInfo : IEquatable<DrawColourInfo>
    {
        public ColourInfo Colour;
        public BlendingParameters Blending;
        public bool LerpToBlack;

        public DrawColourInfo(ColourInfo? colour = null, BlendingParameters? blending = null, bool lerpToBlack = false)
        {
            Colour = colour ?? ColourInfo.SingleColour(Color4.White);
            Blending = blending ?? BlendingParameters.Inherit;
            LerpToBlack = lerpToBlack;
        }

        public readonly bool Equals(DrawColourInfo other) => Colour.Equals(other.Colour) && Blending == other.Blending && LerpToBlack == other.LerpToBlack;
    }
}

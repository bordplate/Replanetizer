﻿// Copyright (C) 2018-2021, The Replanetizer Contributors.
// Replanetizer is free software: you can redistribute it
// and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
// Please see the LICENSE.md file for more details.

using LibReplanetizer.LevelObjects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Replanetizer.Frames;
using Replanetizer.Utils;
using System;

namespace Replanetizer.Tools
{
    class ScalingTool : BasicTransformTool
    {
        public override ToolType toolType => ToolType.Scaling;

        public ScalingTool(Toolbox toolbox) : base(toolbox)
        {
            const float length = 1f;
            const float thickness = length / 2.0f;
            const float thickness2 = length / 3.0f;

            vb = new[]{
                thickness,     -thickness2,   0,
                thickness,     thickness2,     0,
                length,         0,              0,

                -thickness,     - thickness2,   0,
                -thickness,     thickness2,     0,
                -length,         0,              0,


                thickness,     0,   - thickness2,
                thickness,     0,     thickness2,
                length,         0,              0,

                -thickness,     0,   - thickness2,
                -thickness,     0,     thickness2,
                -length,         0,              0,


                -thickness2,    thickness,     0,
                thickness2,     thickness,     0,
                0,              length,         0,

                -thickness2,    -thickness,    0,
                thickness2,     -thickness,    0,
                0,              -length,        0,

                0,    thickness,     -thickness2,
                0,     thickness,     thickness2,
                0,              length,         0,

                0,    -thickness,    -thickness2,
                0,     -thickness,    thickness2,
                0,              -length,        0,


                -thickness2,    0,              -thickness,
                thickness2,     0,              -thickness,
                0,              0,              -length,

                -thickness2,    0,              thickness,
                thickness2,     0,              thickness,
                0,              0,              length,

                0,    -thickness2,              -thickness,
                0,     thickness2,              -thickness,
                0,              0,              -length,

                0,    -thickness2,              thickness,
                0,     thickness2,              thickness,
                0,              0,              length,
            };
        }

        public override void Render(Matrix4 mat, LevelFrame frame)
        {
            GetVbo();

            GL.UniformMatrix4(frame.shaderIDTable.uniformModelToWorldMatrix, false, ref mat);

            GL.Uniform1(frame.shaderIDTable.uniformColorLevelObjectNumber, 0);
            GL.Uniform4(frame.shaderIDTable.uniformColor, new Vector4(1, 0, 0, 1));
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 3, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 6, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 9, 3);

            GL.Uniform1(frame.shaderIDTable.uniformColorLevelObjectNumber, 1);
            GL.Uniform4(frame.shaderIDTable.uniformColor, new Vector4(0, 1, 0, 1));
            GL.DrawArrays(PrimitiveType.Triangles, 12, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 15, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 18, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 21, 3);

            GL.Uniform1(frame.shaderIDTable.uniformColorLevelObjectNumber, 2);
            GL.Uniform4(frame.shaderIDTable.uniformColor, new Vector4(0, 0, 1, 1));
            GL.DrawArrays(PrimitiveType.Triangles, 24, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 27, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 30, 3);
            GL.DrawArrays(PrimitiveType.Triangles, 33, 3);
        }

        public override void Transform(LevelObject obj, Vector3 pivot, TransformToolData data)
        {
            // TODO: For the global transformation space a different method could be used
            // that scales along the correct axis
            float prevDist = getLineIntersectionDist(pivot, data.axisDir, data.cameraPos, data.mousePrevDir);
            float currDist = getLineIntersectionDist(pivot, data.axisDir, data.cameraPos, data.mouseCurrDir);

            float prevScale = MathF.Abs(prevDist);
            float currScale = MathF.Abs(currDist);

            float sign = MathF.Sign(prevDist * currDist);
            float change = currScale - prevScale;

            // Otherwise we flip signs on all axis
            // It is a bit tricky, when should we flip and when shouldn't we?
            // Also sometimes the signs flip for just one frame so we
            // make sure that that only happens when we are close to 0
            float signX = (obj.scale.X < 1.0f && data.axisDir.X != 0.0f) ? sign : 1.0f;
            float signY = (obj.scale.Y < 1.0f && data.axisDir.Y != 0.0f) ? sign : 1.0f;
            float signZ = (obj.scale.Z < 1.0f && data.axisDir.Z != 0.0f) ? sign : 1.0f;

            float scaleX = signX * MathF.Max(0.01f, (data.axisDir.X * change / prevScale + 1.0f));
            float scaleY = signY * MathF.Max(0.01f, (data.axisDir.Y * change / prevScale + 1.0f));
            float scaleZ = signZ * MathF.Max(0.01f, (data.axisDir.Z * change / prevScale + 1.0f));

            obj.scale *= new Vector3(scaleX, scaleY, scaleZ);
            obj.UpdateTransformMatrix();
        }

        protected override Vector3 ProcessVec(Vector3 direction, Vector3 magnitude)
        {
            return base.ProcessVec(direction, magnitude) + Vector3.One;
        }
    }
}

﻿// Copyright (C) 2018-2021, The Replanetizer Contributors.
// Replanetizer is free software: you can redistribute it
// and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
// Please see the LICENSE.md file for more details.

using OpenTK.Mathematics;
using static LibReplanetizer.DataFunctions;

namespace LibReplanetizer.Models.Animations
{
    public class BoneData
    {
        public Vector3 translation;
        public short unk0x0C;
        public short parent;

        //The first 12 bytes are 3 floats which are exactly the translation from the BoneMatrix
        //Last 4 bytes are equal to the last 4 bytes in the corresponding BoneMatrix

        public BoneData(byte[] boneDataBlock, int num)
        {
            int offset = num * 0x10;
            float translationX = ReadFloat(boneDataBlock, offset + 0x00);
            float translationY = ReadFloat(boneDataBlock, offset + 0x04);
            float translationZ = ReadFloat(boneDataBlock, offset + 0x08);

            translation = new Vector3(translationX / 1024.0f, translationY / 1024.0f, translationZ / 1024.0f);

            //0 for root and some constant else (0b0111000000000000 = 0x7000 = 28672)
            unk0x0C = ReadShort(boneDataBlock, offset + 0x0C);
            parent = (short) (ReadShort(boneDataBlock, offset + 0x0E) / 0x40);
        }

        public byte[] Serialize()
        {
            byte[] outBytes = new byte[0x10];

            WriteFloat(outBytes, 0x00, translation.X * 1024.0f);
            WriteFloat(outBytes, 0x04, translation.Y * 1024.0f);
            WriteFloat(outBytes, 0x08, translation.Z * 1024.0f);
            WriteShort(outBytes, 0x0C, unk0x0C);
            WriteShort(outBytes, 0x0E, (short) (parent * 0x40));

            return outBytes;
        }
    }
}

﻿using System.ComponentModel;
using System.Drawing;
using System.IO;
using OpenTK;
using static LibReplanetizer.DataFunctions;

namespace LibReplanetizer.LevelObjects
{
    public class LevelVariables
    {
        [Category("Attributes"), DisplayName("Background Color")]
        public Color backgroundColor { get; set; }

        [Category("Attributes"), DisplayName("Fog Color")]
        public Color fogColor { get; set; }

        [Category("Attributes"), DisplayName("Fog Near Distance")]
        public float fogNearDistance { get; set; }

        [Category("Attributes"), DisplayName("Fog Far Distance")]
        public float fogFarDistance { get; set; }

        [Category("Attributes"), DisplayName("Fog Near Intensity")]
        public float fogNearIntensity { get; set; }

        [Category("Attributes"), DisplayName("Fog Far Intensity")]
        public float fogFarIntensity { get; set; }

        [Category("Attributes"), DisplayName("Deathplane Z")]
        public float deathPlaneZ { get; set; }

        [Category("Attributes"), DisplayName("Is Spherical World?")]
        public int isSphericalWorld { get; set; }

        [Category("Attributes"), DisplayName("Sphere Center")]
        public Vector3 sphereCentre { get; set; }

        [Category("Attributes"), DisplayName("Ship Position")]
        public Vector3 shipPosition { get; set; }

        [Category("Attributes"), DisplayName("Ship Rotation")]
        public float shipRotation { get; set; }

        [Category("Unknown")]
        public int off_48 { get; set; }

        [Category("Unknown")]
        public int off_4c { get; set; }

        [Category("Unknown")]
        public int off_50 { get; set; }

        [Category("Unknown")]
        public int off_54 { get; set; }

        [Category("Unknown")]
        public int off_58 { get; set; }

        [Category("Unknown")]
        public Color unkColor { get; set; }

        public LevelVariables(GameType game, FileStream fileStream, int levelVarPointer)
        {
            if (levelVarPointer == 0) return;

            switch (game.num)
            {
                case 1:
                    GetRC1Vals(fileStream, levelVarPointer);
                    break;
                case 2:
                case 3:
                    GetRC23Vals(fileStream, levelVarPointer);
                    break;
                case 4:
                    GetDLVals(fileStream, levelVarPointer);
                    break;
                default:
                    GetRC23Vals(fileStream, levelVarPointer);
                    break;
            }
        }

        private void GetRC1Vals(FileStream fileStream, int levelVarPointer)
        {
            byte[] levelVarBlock = ReadBlock(fileStream, levelVarPointer, 0x50);

            int bgRed = ReadInt(levelVarBlock, 0x00);
            int bgGreen = ReadInt(levelVarBlock, 0x04);
            int bgBlue = ReadInt(levelVarBlock, 0x08);
            int r = ReadInt(levelVarBlock, 0x0c);

            int g = ReadInt(levelVarBlock, 0x10);
            int b = ReadInt(levelVarBlock, 0x14);
            fogNearDistance = ReadFloat(levelVarBlock, 0x18);
            fogFarDistance = ReadFloat(levelVarBlock, 0x1C);

            fogNearIntensity = ReadFloat(levelVarBlock, 0x20);
            fogFarIntensity = ReadFloat(levelVarBlock, 0x24);
            deathPlaneZ = ReadFloat(levelVarBlock, 0x28);
            float shipPositionX = ReadFloat(levelVarBlock, 0x2C);

            float shipPositionY = ReadFloat(levelVarBlock, 0x30);
            float shipPositionZ = ReadFloat(levelVarBlock, 0x34);
            shipRotation = ReadFloat(levelVarBlock, 0x38);
            int unkColorR = ReadInt(levelVarBlock, 0x3C);

            int unkColorG = ReadInt(levelVarBlock, 0x40);
            int unkColorB = ReadInt(levelVarBlock, 0x44);
            off_48 = ReadInt(levelVarBlock, 0x48);
            off_4c = ReadInt(levelVarBlock, 0x4C);

            backgroundColor = Color.FromArgb(bgRed, bgGreen, bgBlue);
            fogColor = Color.FromArgb(r, g, b);
            unkColor = Color.FromArgb(unkColorR, unkColorG, unkColorB);

            sphereCentre = Vector3.Zero;
            shipPosition = new Vector3(shipPositionX, shipPositionY, shipPositionZ);
        }

        private void GetRC23Vals(FileStream fileStream, int levelVarPointer)
        {
            byte[] levelVarBlock = ReadBlock(fileStream, levelVarPointer, 0x5C);

            int bgRed = ReadInt(levelVarBlock, 0x00);
            int bgGreen = ReadInt(levelVarBlock, 0x04);
            int bgBlue = ReadInt(levelVarBlock, 0x08);
            int r = ReadInt(levelVarBlock, 0x0c);

            int g = ReadInt(levelVarBlock, 0x10);
            int b = ReadInt(levelVarBlock, 0x14);
            fogNearDistance = ReadFloat(levelVarBlock, 0x18);
            fogFarDistance = ReadFloat(levelVarBlock, 0x1C);

            fogNearIntensity = ReadFloat(levelVarBlock, 0x20);
            fogFarIntensity = ReadFloat(levelVarBlock, 0x24);
            deathPlaneZ = ReadFloat(levelVarBlock, 0x28);
            isSphericalWorld = ReadInt(levelVarBlock, 0x2C);

            float sphereCentreX = ReadFloat(levelVarBlock, 0x30);
            float sphereCentreY = ReadFloat(levelVarBlock, 0x34);
            float sphereCentreZ = ReadFloat(levelVarBlock, 0x38);
            float shipPositionX = ReadFloat(levelVarBlock, 0x3C);

            float shipPositionY = ReadFloat(levelVarBlock, 0x40);
            float shipPositionZ = ReadFloat(levelVarBlock, 0x44);
            shipRotation = ReadFloat(levelVarBlock, 0x48);
            int unkColorR = ReadInt(levelVarBlock, 0x4C);

            int unkColorG = ReadInt(levelVarBlock, 0x50);
            int unkColorB = ReadInt(levelVarBlock, 0x54);
            off_58 = ReadInt(levelVarBlock, 0x58);

            backgroundColor = Color.FromArgb(bgRed, bgGreen, bgBlue);
            fogColor = Color.FromArgb(r, g, b);
            unkColor = Color.FromArgb(unkColorR, unkColorG, unkColorB);

            sphereCentre = new Vector3(sphereCentreX, sphereCentreY, sphereCentreZ);
            shipPosition = new Vector3(shipPositionX, shipPositionY, shipPositionZ);
        }

        private void GetDLVals(FileStream fileStream, int levelVarPointer)
        {
            byte[] levelVarBlock = ReadBlock(fileStream, levelVarPointer, 0x5C);

            int bgRed = ReadInt(levelVarBlock, 0x00);
            int bgGreen = ReadInt(levelVarBlock, 0x04);
            int bgBlue = ReadInt(levelVarBlock, 0x08);
            int r = ReadInt(levelVarBlock, 0x0c);

            int g = ReadInt(levelVarBlock, 0x10);
            int b = ReadInt(levelVarBlock, 0x14);
            fogNearDistance = ReadFloat(levelVarBlock, 0x18);
            fogFarDistance = ReadFloat(levelVarBlock, 0x1C);

            fogNearIntensity = ReadFloat(levelVarBlock, 0x20);
            fogFarIntensity = ReadFloat(levelVarBlock, 0x24);
            deathPlaneZ = ReadFloat(levelVarBlock, 0x28);
            isSphericalWorld = ReadInt(levelVarBlock, 0x2C);

            float sphereCentreX = ReadFloat(levelVarBlock, 0x30);
            float sphereCentreY = ReadFloat(levelVarBlock, 0x34);
            float sphereCentreZ = ReadFloat(levelVarBlock, 0x38);
            float shipPositionX = ReadFloat(levelVarBlock, 0x3C);

            float shipPositionY = ReadFloat(levelVarBlock, 0x40);
            float shipPositionZ = ReadFloat(levelVarBlock, 0x44);
            shipRotation = ReadFloat(levelVarBlock, 0x48);
            off_4c = ReadInt(levelVarBlock, 0x4C);

            off_50 = ReadInt(levelVarBlock, 0x50);
            off_50 = ReadInt(levelVarBlock, 0x54);
            off_58 = ReadInt(levelVarBlock, 0x58);

            backgroundColor = Color.FromArgb(bgRed, bgGreen, bgBlue);
            fogColor = Color.FromArgb(r, g, b);
            unkColor = Color.Black;

            sphereCentre = new Vector3(sphereCentreX, sphereCentreY, sphereCentreZ);
            shipPosition = new Vector3(shipPositionX, shipPositionY, shipPositionZ);
        }

        public byte[] Serialize(GameType game)
        {
            switch (game.num)
            {
                case 1:
                    return SerializeRC1();
                case 2:
                case 3:
                    return SerializeRC23();
                case 4:
                    return SerializeDL();
                default:
                    return SerializeRC23();
            }
        }

        private byte[] SerializeRC1()
        {
            byte[] bytes = new byte[0x50];

            WriteUint(bytes, 0x00, backgroundColor.R);
            WriteUint(bytes, 0x04, backgroundColor.G);
            WriteUint(bytes, 0x08, backgroundColor.B);
            WriteUint(bytes, 0x0C, fogColor.R);

            WriteUint(bytes, 0x10, fogColor.G);
            WriteUint(bytes, 0x14, fogColor.B);
            WriteFloat(bytes, 0x18, fogNearDistance);
            WriteFloat(bytes, 0x1C, fogFarDistance);

            WriteFloat(bytes, 0x20, fogNearIntensity);
            WriteFloat(bytes, 0x24, fogFarIntensity);
            WriteFloat(bytes, 0x28, deathPlaneZ);
            WriteFloat(bytes, 0x2C, shipPosition.X);

            WriteFloat(bytes, 0x30, shipPosition.Y);
            WriteFloat(bytes, 0x34, shipPosition.Z);
            WriteFloat(bytes, 0x38, shipRotation);
            WriteInt(bytes, 0x3C, unkColor.R);

            WriteInt(bytes, 0x40, unkColor.G);
            WriteInt(bytes, 0x44, unkColor.B);
            WriteInt(bytes, 0x48, off_48);
            WriteInt(bytes, 0x4C, off_4c);

            return bytes;
        }

        private byte[] SerializeRC23()
        {
            byte[] bytes = new byte[0x5C];

            WriteUint(bytes, 0x00, backgroundColor.R);
            WriteUint(bytes, 0x04, backgroundColor.G);
            WriteUint(bytes, 0x08, backgroundColor.B);
            WriteUint(bytes, 0x0C, fogColor.R);

            WriteUint(bytes, 0x10, fogColor.G);
            WriteUint(bytes, 0x14, fogColor.B);
            WriteFloat(bytes, 0x18, fogNearDistance);
            WriteFloat(bytes, 0x1C, fogFarDistance);

            WriteFloat(bytes, 0x20, fogNearIntensity);
            WriteFloat(bytes, 0x24, fogFarIntensity);
            WriteFloat(bytes, 0x28, deathPlaneZ);
            WriteInt(bytes, 0x2C, isSphericalWorld);

            WriteFloat(bytes, 0x30, sphereCentre.X);
            WriteFloat(bytes, 0x34, sphereCentre.Y);
            WriteFloat(bytes, 0x38, sphereCentre.Z);
            WriteFloat(bytes, 0x3C, shipPosition.X);

            WriteFloat(bytes, 0x40, shipPosition.Y);
            WriteFloat(bytes, 0x44, shipPosition.Z);
            WriteFloat(bytes, 0x48, shipRotation);
            WriteInt(bytes, 0x4C, unkColor.R);

            WriteInt(bytes, 0x50, unkColor.G);
            WriteInt(bytes, 0x54, unkColor.B);
            WriteInt(bytes, 0x58, off_58);

            return bytes;
        }

        private byte[] SerializeDL()
        {
            byte[] bytes = new byte[0x5C];

            WriteUint(bytes, 0x00, backgroundColor.R);
            WriteUint(bytes, 0x04, backgroundColor.G);
            WriteUint(bytes, 0x08, backgroundColor.B);
            WriteUint(bytes, 0x0C, fogColor.R);

            WriteUint(bytes, 0x10, fogColor.G);
            WriteUint(bytes, 0x14, fogColor.B);
            WriteFloat(bytes, 0x18, fogNearDistance);
            WriteFloat(bytes, 0x1C, fogFarDistance);

            WriteFloat(bytes, 0x20, fogNearIntensity);
            WriteFloat(bytes, 0x24, fogFarIntensity);
            WriteFloat(bytes, 0x28, deathPlaneZ);
            WriteInt(bytes, 0x2C, isSphericalWorld);

            WriteFloat(bytes, 0x30, sphereCentre.X);
            WriteFloat(bytes, 0x34, sphereCentre.Y);
            WriteFloat(bytes, 0x38, sphereCentre.Z);
            WriteFloat(bytes, 0x3C, shipPosition.X);

            WriteFloat(bytes, 0x40, shipPosition.Y);
            WriteFloat(bytes, 0x44, shipPosition.Z);
            WriteFloat(bytes, 0x48, shipRotation);
            WriteInt(bytes, 0x4C, unkColor.R);

            WriteInt(bytes, 0x50, off_50);
            WriteInt(bytes, 0x54, off_54);
            WriteInt(bytes, 0x58, off_58);

            return bytes;
        }

    }
}

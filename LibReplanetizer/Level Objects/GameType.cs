﻿// Copyright (C) 2018-2021, The Replanetizer Contributors.
// Replanetizer is free software: you can redistribute it
// and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
// Please see the LICENSE.md file for more details.

namespace LibReplanetizer
{
    public class GameType
    {
        public static readonly int[] MOBY_SIZES = { 0x78, 0x88, 0x88, 0x70 };

        public int num;
        public int mobyElemSize;
        public int engineSize;

        public GameType(int gameNum)
        {
            num = gameNum;

            mobyElemSize = MOBY_SIZES[gameNum - 1];
        }
    }
}

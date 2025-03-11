﻿using System;
using UnityEngine;

namespace StupidTemplate.Classes
{
    public class ExtGradient
    {
        public GradientColorKey[] colors = new GradientColorKey[]
        {
            new GradientColorKey(new Color(0.06f, 0.06f, 0.06f), 1f),
        };

        public bool isRainbow = false;
        public bool copyRigColors = false;
    }
}

﻿using System;
using System.Diagnostics;

namespace ParticleSimulation.Utils
{
    public class LogicTimer
    {
        public const float FramesPerSecond = 60;
        public static float DeltaTime = FixedDelta;

        public const float FixedDelta = 1.0f / FramesPerSecond;

        private double _accumulator;
        private long _lastTime;

        private readonly Stopwatch _stopwatch;
        private readonly Action _action;

        public float LerpAlpha => (float)_accumulator / FixedDelta;

        public LogicTimer(Action action)
        {
            _stopwatch = new Stopwatch();
            _action = action;
        }

        public void Start()
        {
            _lastTime = 0;
            _accumulator = 0.0;
            _stopwatch.Restart();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public void Update()
        {
            long elapsedTicks = _stopwatch.ElapsedTicks;
            _accumulator += (double)(elapsedTicks - _lastTime) / Stopwatch.Frequency;
            _lastTime = elapsedTicks;
            
            if (_accumulator >= FixedDelta)
            {
                _action();
            }

            while (_accumulator >= FixedDelta)
            {
                _accumulator -= FixedDelta;
            }
        }
    }
}

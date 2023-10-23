using System;
using System.Diagnostics;

namespace Mister.Framework
{
    public class GameTime
    {
        private Stopwatch stopwatch;
        private TimeSpan previousTime;

        public float DeltaTime { get; private set; }
        public float TotalTime { get; private set; }

        public GameTime()
        {
            stopwatch = new Stopwatch();
        }

        public void Start()
        {
            stopwatch.Start();
            previousTime = stopwatch.Elapsed;
        }

        public void Update()
        {
            TimeSpan currentTime = stopwatch.Elapsed;
            DeltaTime = (float)(currentTime - previousTime).TotalSeconds;
            TotalTime = (float)currentTime.TotalSeconds;
            previousTime = currentTime;
        }

        public void Stop()
        {
            stopwatch.Stop();
        }
    }
}

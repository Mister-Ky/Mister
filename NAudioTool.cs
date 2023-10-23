using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Mister.Audio
{
    //  NAudioTool class

    //  A tool that facilitates work with NAudio Library

    public class AudioPlayer : IDisposable
    {
        private WaveOutEvent waveOut;
        private AudioFileReader audioFile;
        private string tempFilePath;
        private bool disposed;

        public bool IsPlaying;
        public bool IsLoop;
        public float VolumeGet { get; private set; }

        public AudioPlayer()
        {
            waveOut = new WaveOutEvent();
            tempFilePath = null;
            disposed = false;
            IsPlaying = false;
            IsLoop = false;
            waveOut.Volume = 1.0f;
            VolumeGet = waveOut.Volume * 100;
            OnPlaybackEnded();
        }

        private void OnPlaybackEnded()
        {
            waveOut.PlaybackStopped += (sender, e) =>
            {
                IsPlaying = false;
                if (IsLoop)
                {
                    if (audioFile != null)
                    {
                        audioFile.Dispose();
                        audioFile = null;
                    }
                    if (!waveOut.PlaybackState.Equals(PlaybackState.Playing))
                    {
                        audioFile = new AudioFileReader(tempFilePath);
                        waveOut.Init(audioFile);
                        waveOut.Play();
                        IsPlaying = true;
                    }
                }
            };
        }

        public async Task<bool> PlaybackEnded()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            waveOut.PlaybackStopped += (sender, e) =>
            {
                tcs.SetResult(true);
            };

            await tcs.Task;
            return true;
        }

        public void Play(string audioFilePath)
        {
            Stop();
            audioFile = new AudioFileReader(audioFilePath);

            waveOut.Init(audioFile);
            waveOut.Play();
            IsPlaying = true;
        }

        public void Play(byte[] audioBytes)
        {
            Stop();
            tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, audioBytes);

            audioFile = new AudioFileReader(tempFilePath);

            waveOut.Init(audioFile);
            waveOut.Play();
            IsPlaying = true;
        }

        public void Play(byte[] audioBytes, int sampleRate, int channels, int bitsPerSample)
        {
            Stop();
            tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, audioBytes);

            audioFile = new AudioFileReader(tempFilePath);

            using (MemoryStream memory = new MemoryStream(audioBytes))
            {
                waveOut.Init(new RawSourceWaveStream(memory, new WaveFormat(sampleRate, channels, bitsPerSample)));
                memory.Dispose();
            }
            waveOut.Play();
            IsPlaying = true;
        }

        public void Play(byte[] audioBytes, WaveFormat format)
        {
            Stop();
            tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, audioBytes);

            audioFile = new AudioFileReader(tempFilePath);

            using (MemoryStream memory = new MemoryStream(audioBytes))
            {
                waveOut.Init(new RawSourceWaveStream(memory, format));
                memory.Dispose();
            }
            waveOut.Play();
            IsPlaying = true;
        }

        public void Pause()
        {
            if (IsPlaying)
            {
                waveOut.Pause();
                IsPlaying = false;
            }
        }

        public void Resume()
        {
            if (!IsPlaying)
            {
                waveOut.Play();
                IsPlaying = true;
            }
        }

        public void Volume(int volume)
        {
            if (volume < 0)
            {
                volume = 0;
            }
            else if (100 < volume)
            {
                volume = 100;
            }
            waveOut.Volume = volume / 100f;
            VolumeGet = waveOut.Volume * 100f;
        }

        public void VolumeMinusOne()
        {
            Volume((int)Math.Round(waveOut.Volume * 100) - 1);
        }

        public void VolumePlusOne()
        {
            Volume((int)Math.Round(waveOut.Volume * 100) + 1);
        }

        public void VolumeMin()
        {
            waveOut.Volume = 0f;
            VolumeGet = waveOut.Volume * 100;
        }

        public void VolumeMax()
        {
            waveOut.Volume = 1.0f;
            VolumeGet = waveOut.Volume * 100;
        }

        public void VolumeAlt()
        {
            if (waveOut.Volume == 0f)
            {
                waveOut.Volume = 1.0f;
                VolumeGet = waveOut.Volume * 100;
            }
            else
            {
                waveOut.Volume = 0f;
                VolumeGet = waveOut.Volume * 100;
            }
        }

        public void Stop()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                if (disposed)
                {
                    waveOut.Dispose();
                    waveOut = null;
                }
            }
            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
            if (tempFilePath != null)
            {
                File.Delete(tempFilePath);
                tempFilePath = null;
            }
            IsPlaying = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Stop();
                }
                disposed = true;
            }
        }

        ~AudioPlayer()
        {
            Dispose(false);
        }
    }
}
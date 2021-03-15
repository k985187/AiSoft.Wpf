using System;
using System.Media;
using System.Reflection;
using System.Threading.Tasks;

namespace AiSoft.Wpf.Utils
{
    /// <summary>
    /// 音频播放
    /// </summary>
    public class SoundPlay
    {
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="mediaFilePath">音频文件路径</param>
        public static void Play(string mediaFilePath)
        {
            Task.Run(() =>
            {
                var runWavStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mediaFilePath);
                var soundPlayer = new SoundPlayer {Stream = runWavStream};
                soundPlayer.Play();
            });
        }
    }
}
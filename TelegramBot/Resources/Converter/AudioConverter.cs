using System;
using System.IO;
using VideoLibrary;
using MediaToolkit;
using MediaToolkit.Model;
using System.Threading.Tasks;
using System.Net;

namespace TelegramBot.Resources.Converter
{
    public class AudioConverter
    {
        public string AudioName { get; private set; }
        public string AudioPath { get; private set; }
        private YouTubeVideo Video { get; set; }

        public async Task CreateMP3Async(string SaveToFolder, string MP3Name)
        {
            await Task.Run(() => CreateMP3(SaveToFolder, MP3Name));
        }

        public async Task<string> CheckMP3PathAsync()
        {
            return await Task.FromResult(CheckMP3Path());
        }

        public async Task<bool> CheckUrlAsync(string url)
        {
            return await Task.FromResult(CheckUrl(url));
        }

        public async Task<bool> TryGetVideoAsync(string url)
        {
            return await Task.FromResult(TryGetVideo(url));
        }

        public async Task CheckAudioLengthAsync()
        {
            await Task.Run(() => CheckAudioLength());
        }

        public async Task DeleteAudioFileAsync(string path)
        {
            await Task.Run(() => DeleteAudioFile(path));
        }

        public bool CheckUrl(string url)
        {
            try
            {
                WebRequest _webRequest = HttpWebRequest.Create(url);
                _webRequest.Method = "HEAD";
                try
                {
                    using (WebResponse webResponse = _webRequest.GetResponse())
                    {
                        Console.WriteLine("URL exists");
                        return true;
                    }
                }
                catch (WebException e)
                {
                    Console.WriteLine("URL not exists: " + e.Message);
                    return false;
                }
            }
            catch(System.UriFormatException)
            {
                Console.WriteLine("URL not exists: ");
                return false;
            }
        }

        public void CreateMP3(string SaveToFolder, string MP3Name)
        {
            string _source = @SaveToFolder;
            string _videoPath = Path.Combine(_source, Video.FullName);
 
            File.WriteAllBytes(_videoPath, Video.GetBytes());

            var _inputFile = new MediaFile { Filename = Path.Combine(_source, Video.FullName) };
            var _outputFile = new MediaFile { Filename = Path.Combine(_source, $"{MP3Name}.mp3") };

            using (var engine = new Engine())
            {
                engine.GetMetadata(_inputFile);
                engine.Convert(_inputFile, _outputFile);
            }

            AudioName = Video.FullName;
            AudioPath = _outputFile.Filename;

            File.Delete(Path.Combine(_source, Video.FullName));
            System.GC.Collect();
        }

        public string CheckMP3Path()
        {
            return AudioPath;
        }

        public void DeleteAudioFile(string path)
        {
            File.Delete(path);
        }

        public bool TryGetVideo(string url)
        {
            try
            {
                var _youtube = YouTube.Default;
                Video = _youtube.GetVideo(url);
                return true;
            }
            catch (VideoLibrary.Exceptions.UnavailableStreamException)
            {
                return false;
            }
        }

        public long SizeOfAudioFile()
        {
            if (AudioPath == null)
                return 0;
            
            long _length = new System.IO.FileInfo(AudioPath).Length;
            return _length;
        }

        public void CheckAudioLength()
        {
            if (Video == null)
                return;

            if (Video.Info.LengthSeconds > Constants.VIDEOMAXLENGTH)
            {
                throw new VideoLengthExceededException("Video is too long");
            }
        }
    }
}

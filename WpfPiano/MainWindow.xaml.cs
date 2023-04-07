using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NAudio.Wave;

namespace WpfPiano
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            string noteName = null;
            switch (e.Key)
            {
                case Key.Q:
                    noteName = "C";
                    break;
                case Key.W:
                    noteName = "D";
                    break;
                case Key.E:
                    noteName = "E";
                    break;
                case Key.R:
                    noteName = "F";
                    break;
                case Key.T:
                    noteName = "G";
                    break;
                case Key.Y:
                    noteName = "A";
                    break;
                case Key.U:
                    noteName = "B";
                    break;              
                
                case Key.D1:
                    noteName = "HighC";
                    break;
                case Key.D2:
                    noteName = "HighD";
                    break;
                case Key.D3:
                    noteName = "HighE";
                    break;
                case Key.D4:
                    noteName = "HighF";
                    break;
                case Key.D5:
                    noteName = "HighG";
                    break;
                case Key.D6:
                    noteName = "HighA";
                    break;

            }

            if (noteName != null)
            {
                PlayNoteWithNAudio(noteName);
            }
        }

        private void PlayNoteByName(string noteName)
        {
            string soundFilePath = $"{noteName}.wav";
            if (File.Exists(soundFilePath))
            {
                SoundPlayer player = new SoundPlayer(soundFilePath);
                player.Play();
            }
            else
            {
                MessageBox.Show($"Файл {soundFilePath} не найден.");
            }
        }
        private void PlayNote(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string noteName = button.Name;
            PlayNoteWithNAudio(noteName);
        }

        private void PlayNoteWithNAudio(string noteName)
        {
            string soundFilePath = $"{noteName}.wav";
            if (File.Exists(soundFilePath))
            {
                var audioFileReader = new AudioFileReader(soundFilePath);
                var waveOut = new WaveOutEvent();
                waveOut.Init(audioFileReader);
                waveOut.Play();

                waveOut.PlaybackStopped += (sender, e) =>
                {
                    waveOut.Dispose();
                    audioFileReader.Dispose();
                };
            }
            else
            {
                MessageBox.Show($"Файл {soundFilePath} не найден.");
            }
        }

    }
}

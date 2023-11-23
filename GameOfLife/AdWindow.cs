using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameOfLife
{
    class AdWindow : Window
    {
        private readonly DispatcherTimer adTimer;
        private int imgNmb;     // the number of the image currently shown
        private string link;    // the URL where the currently shown ad leads to
        private readonly Dictionary<int, ImageBrush> adImages;
        private static readonly Random rnd = new Random();


        public AdWindow(Window owner)
        {
            Owner = owner;
            Width = 350;
            Height = 100;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.ToolWindow;
            Title = "Support us by clicking the ads";
            Cursor = Cursors.Hand;
            ShowActivated = false;
            MouseDown += OnClick;

            adImages = new Dictionary<int, ImageBrush>
            {
                {1, new ImageBrush { ImageSource = new BitmapImage(new Uri("ad1.jpg", UriKind.Relative))}},
                {2, new ImageBrush { ImageSource = new BitmapImage(new Uri("ad2.jpg", UriKind.Relative))}},
                {3, new ImageBrush { ImageSource = new BitmapImage(new Uri("ad3.jpg", UriKind.Relative))}}
            };

            imgNmb = rnd.Next(1, 3);
            ChangeAds(this, new EventArgs());

            // Run the timer that changes the ad's image 
            adTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            adTimer.Tick += ChangeAds;
            adTimer.Start();
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            OpenUrl(link);
            Close();
        }

        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            Unsubscribe();
            base.OnClosed(e);
        }

        public void Unsubscribe()
        {
            adTimer.Tick -= ChangeAds;
        }

        private void ChangeAds(object sender, EventArgs eventArgs)
        {

            ImageBrush myBrush = new ImageBrush();

            switch (imgNmb)
            {
                case 1:
                    Background = adImages[1];
                    Background = myBrush;
                    link = "http://example.com";
                    imgNmb++;
                    break;
                case 2:
                    Background = adImages[2];
                    Background = myBrush;
                    link = "http://example.com";
                    imgNmb++;
                    break;
                case 3:
                    Background = adImages[3];
                    Background = myBrush;
                    link = "http://example.com";
                    imgNmb = 1;
                    break;
            }
        }
    }
}
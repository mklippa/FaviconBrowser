using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FaviconBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly List<string> s_Domains = new List<string>
                                                             {
                                                                 "google.com",
                                                                 "bing.com",
                                                                 "oreilly.com",
                                                                 "simple-talk.com",
                                                                 "microsoft.com",
                                                                 "facebook.com",
                                                                 "twitter.com",
                                                                 "reddit.com",
                                                                 "baidu.com",
                                                                 "bbc.co.uk"
                                                             };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (string domain in s_Domains)
            {
                AddAFavicon(domain);
            }
        }

        private void AddAFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadDataCompleted += OnWebClientOnDownloadDataCompleted;
            webClient.DownloadDataAsync(new Uri("http://" + domain + "/favicon.ico"));
        }

        private void OnWebClientOnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs args)
        {
            Image imageControl = MakeImageControl(args.Result);
            m_WrapPanel.Children.Add(imageControl);
        }

        private static Image MakeImageControl(byte[] bytes)
        {
            Image imageControl = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            imageControl.Source = bitmapImage;
            imageControl.Width = 16;
            imageControl.Height = 16;
            return imageControl;
        }
    }
}

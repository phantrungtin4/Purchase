﻿using Microsoft.Win32;
using LTUDQL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace LTUDQL2.Views
{
    /// <summary>
    /// Interaction logic for AddMovie.xaml
    /// </summary>
    public partial class AddMovie : Window
    {
        public AddMovie()
        {
            InitializeComponent();
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == true)
            {
                posterMovie.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
        
        private void LoadVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a video";
            op.Filter = "All Videos Files |*.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; " +
                  " *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm";

            if (op.ShowDialog() == true)
            {
                videoMovie.Source = new Uri(op.FileName);
            }
        }

        public byte[] imageToByteArray(string filename)
        {
            var image = new BitmapImage(new Uri(filename, UriKind.Absolute));
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        public byte[] videoToByteArray(string filename)
        {
            string localPath = new Uri(filename).LocalPath;
            FileStream fs = null;
            BinaryReader br = null;

            fs = new FileStream(localPath, FileMode.Open, FileAccess.Read);
            br = new BinaryReader(fs);
            byte[] buffer = br.ReadBytes(1024);

            return buffer;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            DataProvider.Ins.DB.Movies.Add(new Movy() {
                MovieName = nameMovie.Text,
                MoviePath = "Không hiểu đây là gì",
                DesccriptionMovie = Discription.Text,
                IDCategory = 1,
                MovieData = videoToByteArray(videoMovie.Source.ToString()),
                PosterPath = imageToByteArray(posterMovie.Source.ToString()),
            });

            DataProvider.Ins.DB.SaveChanges();

            MessageBox.Show("Thêm Thành Công!");
        }
    }
}

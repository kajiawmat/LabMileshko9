using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using ReactiveUI;
using Avalonia.Media.Imaging;

namespace LabMileshko9.Models
{
    public class MyImage : ReactiveObject
    {
        public ObservableCollection<MyImage> Subfolders { get; set; }
        public string Name { get; }
        public string StrPath { get; set; }
        public Bitmap Image { get; set; }
        public MyImage Parent { get; set; }
        public MyImage(string strPath, bool isImage = false, MyImage parent = null)
        {
            Subfolders = new ObservableCollection<MyImage>();
            StrPath = strPath;
            Name = Path.GetFileName(strPath);
            if (Name == "")
            {
                Name = strPath;
            }
            if (isImage)
            {
                Image = new Bitmap(strPath);
                Parent = parent;
            }
        }
        public void LoadSubfolders()
        {
            try
            {
                string[] subdirs = Directory.GetDirectories(StrPath, "*", SearchOption.TopDirectoryOnly);

                foreach (string dir in subdirs)
                {
                    MyImage currentNode = new MyImage(dir);
                    Subfolders.Add(currentNode);
                }
                LoadImages();
            }
            catch { }
        }
        public void LoadImages()
        {
            List<string> images = new List<string>();
            images.AddRange(Directory.GetFiles(StrPath, "*.*", SearchOption.TopDirectoryOnly)
                             .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png")).ToArray());
            if (images.Count > 0)
            {
                foreach (string image in images)
                {
                    MyImage imageNode = new MyImage(image, true, this);
                    Subfolders.Add(imageNode);
                }
            }
        }
    }
}

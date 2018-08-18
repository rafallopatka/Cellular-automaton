using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDescriptionDocument("documents/wprowadzennie");
        }

        private void LoadDescriptionDocument(string filename)
        {
            TextRange textRange;
            FileStream fileStream;
            if (System.IO.File.Exists(filename))
            {
                textRange = new TextRange(rtf.Document.ContentStart, rtf.Document.ContentEnd);
                using (fileStream = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }
            }
        }
    }
}

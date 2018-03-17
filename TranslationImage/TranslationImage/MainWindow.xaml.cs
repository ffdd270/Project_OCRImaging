using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Drawing;
using Shapes = System.Windows.Shapes;
using System.Threading;

using Point = System.Windows;
using System.Windows.Shapes;

using System.Windows.Media.Imaging;

using Tesseract;
using System.Windows.Controls;

namespace TranslationImage
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        private string changeLanguge = "";
        private string SelcetedLanguge = "English";
        private TesseractEngine engine;
        private string targetLanguge = "ko";

        private Bitmap nowBitmap;
        private ImageSperartor sperartor;
        private Translater translater;
        private List<Point.Point> renderPoints;
        private List<Point.Point> points;
        private Thread thread;

        public MainWindow()
        {
            sperartor = new ImageSperartor();
            renderPoints = new List<Point.Point>();
            points = new List<System.Windows.Point>();
            translater = new Translater();

            engine = new TesseractEngine(Environment.CurrentDirectory + "/Traning", "eng");
            InitializeComponent();
        }

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            string translate = translater.Translate(TranslateInput.Text, targetLanguge);
            TranslateOutput.Text = translate;
        }

        private void JsonOpenButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON Files (*.json)|*.json";

            // Display OpenFileDialog by calling ShowDialog method
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                translater.SetAuthExplicit(filename);
            }
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private string IsSingleFile(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] fileNames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                if (fileNames.Length == 1)
                {
                    if (File.Exists(fileNames[0]))
                    {
                        return fileNames[0];
                    }
                }
            }
            return null;
        }

        private void InputImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var inputPoint = e.GetPosition(InputImage);
            var RenderPoint = e.GetPosition(MainCanvas);

            points.Add(inputPoint);
            renderPoints.Add(RenderPoint);

            if (points.Count % 2 == 0 && points.Count > 1)
            {
                int index = points.Count - 1;

                Shapes.Rectangle rectangle = new Shapes.Rectangle();

                var x = Math.Min(renderPoints[index].X, renderPoints[index - 1].X);
                var y = Math.Min(renderPoints[index].Y, renderPoints[index - 1].Y);

                rectangle.BeginInit();

                RectangleInit(index, rectangle, x, y);

                rectangle.EndInit();

                System.Drawing.Rectangle rect = new System.Drawing.Rectangle();

                var realPointX = Math.Min(points[index].X, points[index - 1].X);
                var realPointY = Math.Min(points[index].Y, points[index - 1].Y);

                rect.X = (int)realPointX;
                rect.Y = (int)realPointY;

                rect.Width = (int)rectangle.Width;
                rect.Height = (int)rectangle.Height;

                sperartor.AddCropArea(rect, nowBitmap);
            }
        }

        private void RectangleInit(int index, Shapes.Rectangle rectangle, double x, double y)
        {
            rectangle.Width = Math.Abs(renderPoints[index].X - renderPoints[index - 1].X);
            rectangle.Height = Math.Abs(renderPoints[index].Y - renderPoints[index - 1].Y);
            rectangle.Stroke = Point.Media.Brushes.Blue;

            var margin = rectangle.Margin;
            margin.Left = x;
            margin.Top = y;
            rectangle.Margin = margin;

            MainCanvas.Children.Add(rectangle);
            rectangle.DragOver += Rectangle_Drag;
        }

        private void TessaractEngineLangugeChaning()
        {
            engine = new TesseractEngine(Environment.CurrentDirectory + "/Traning", changeLanguge);
        }

        private string GetLangugeFromComboBoxItem(object input)
        {
            var boxItem = input as ComboBoxItem;
            var languge = boxItem.Content as string;
            string returnValue = "";

            switch (languge)
            {
                case "English":
                    returnValue = "eng";
                    break;

                case "Korean":
                    returnValue = "kor";
                    break;

                case "Japan":
                    returnValue = "jpn";
                    break;

                default:
                    returnValue = "failed";
                    break;
            }
            return returnValue;
        }

        private void ComboBox_SelectionChanged(object sender, Point.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxItem boxItem = OCRLangugeSelect.SelectedItem as ComboBoxItem;
            var languge = boxItem.Content as string;

            if (languge != SelcetedLanguge)
            {
                switch (languge)
                {
                    case "English":
                        changeLanguge = "eng";
                        break;

                    case "Korean":
                        changeLanguge = "kor";
                        break;

                    case "Japen":
                        changeLanguge = "jpn";
                        break;
                }
                SelcetedLanguge = languge;
                engine.Dispose();
                thread = new Thread(new ThreadStart(TessaractEngineLangugeChaning));
                thread.Start();
            }
        }

        private void OutputLanguge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var languge = GetLangugeFromComboBoxItem(OutputLanguge.SelectedItem);
            targetLanguge = languge.Remove(languge.Length - 1);
        }

        private void Rectangle_Drag(object sender, DragEventArgs e)
        {
            Console.Error.WriteLine(sender);
        }

        private void Image_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (IsSingleFile(e) != null)
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }

        private void Image_PreviewDrop(object sender, DragEventArgs e)
        {
            if (thread == null || thread.ThreadState == ThreadState.Stopped)
            {
                e.Handled = true;

                string fileName = IsSingleFile(e);
                if (fileName == null) return;
                var image = new BitmapImage(new Uri(fileName));
                var ocrImage = new Bitmap(fileName, false);
                nowBitmap = ocrImage;

                var resultOCR = engine.Process(ocrImage);

                string Debug = resultOCR.GetText();
                InputImage.Source = image;
                TranslateInput.Text = Debug;

                resultOCR.Dispose();
            }
            else
            {
                MessageBox.Show("OCR 로더가 언어를 읽고 있습니다. 잠시만 기다려 주세요.");
            }
        }

        private void ListBox_PreviewDrop(object sender, DragEventArgs e)
        {
            try
            {
                e.Handled = true;
                string fileName = IsSingleFile(e);
                if (fileName == null) return;
                var image = new BitmapImage(new Uri(fileName));
                ListBoxItem item = new ListBoxItem();
                item.Content = image;
                //item.Name = fileName;
                ImageList.Items.Add(item);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Data);
            }
        }

        private void ListBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            Image_PreviewDragOver(sender, e);
        }

        private void ImageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ImageList.SelectedItem as ListBoxItem;
            InputImage.Source = selectedItem.Content as BitmapImage;
        }
    }
}
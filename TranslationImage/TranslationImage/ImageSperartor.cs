using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TranslationImage
{
    internal class ImageSperartor
    {
        private Dictionary<Rectangle, Bitmap> crops;

        public ImageSperartor()
        {
            crops = new Dictionary<Rectangle, Bitmap>();
        }

        public void AddCropArea(int x, int y, int width, int height, Bitmap bitmap)
        {
            var rect = new Rectangle(x, y, width, height);
            crops.Add(rect, bitmap.Clone(rect, bitmap.PixelFormat));
        }

        public void AddCropArea(Rectangle rect, Bitmap bitmap)
        {
            crops.Add(rect, bitmap.Clone(rect, bitmap.PixelFormat));
        }

        public Bitmap GetPositionBitmap(Bitmap original, Rectangle rect)
        {
            Bitmap getPosRect = original.Clone(rect, original.PixelFormat);
            return getPosRect;
        }

        public Bitmap GetCrops(int x, int y)
        {
            var rects = crops.Keys;

            foreach (Rectangle rect in rects)
            {
                if (rect.Contains(x, y))
                {
                    return crops[rect];
                }
            }

            return null;
        }

        public List<Bitmap> GetCropsImage()
        {
            return crops.Values.ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TranslationImage
{
    internal class BitmapResource
    {
        private Dictionary<string, Bitmap> fileLists;

        public BitmapResource()
        {
            fileLists = new Dictionary<string, Bitmap>();
        }

        public void AddList(string fileName, Bitmap bitmap)
        {
            if (fileLists[fileName] == null)
            {
                fileLists.Add(fileName, bitmap);
            }
            else
            {
                Console.Error.WriteLine("이미 존재하는 파일이름입니다. " + fileName);
            }
        }

        public Bitmap GetFile(string fileName)
        {
            var returnValue = fileLists[fileName];
            if (returnValue != null)
            {
                return returnValue;
            }
            else
            {
                Console.Error.WriteLine("Null. 존제하지 않는 파일을 찾고 있습니다. " + fileName);
            }
            return null;
        }

        private void GetPosition()
        {
        }
    }
}
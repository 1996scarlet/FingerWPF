using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace FIngerWPF.ToolClass
{
    class SimilarFinger
    {        
        private TransformedBitmap SourceImg;

        public SimilarFinger(BitmapImage bitmapImage)
        {
            var scare = new ScaleTransform(0.04, 0.04);
            SourceImg = new TransformedBitmap(bitmapImage, scare);
        }

        public String GetHash()
        {
            FormatConvertedBitmap grayBitmap = ReduceColor();
            byte[] temp = BulidPixels(grayBitmap);
            int average = CalcAverage(temp);
            String reslut = ComputeBits(temp, average);
            return reslut;
        }

        private FormatConvertedBitmap ReduceColor()
        {
            FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
            newFormatedBitmapSource.BeginInit();
            newFormatedBitmapSource.Source = SourceImg;
            newFormatedBitmapSource.DestinationFormat = PixelFormats.Gray8;
            newFormatedBitmapSource.EndInit();

            return newFormatedBitmapSource;
        }

        private string ComputeBits(byte[] imgPixels, int avg)
        {
            string res = string.Empty;

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (imgPixels[i * 16 + j] >= avg)
                    {
                        res += "1";
                    }
                    else
                    {
                        res += "0";
                    }
                }
            }

            return res;
        }

        private byte[] BulidPixels(FormatConvertedBitmap values)
        {
            byte[] imgPixels = new byte[16 * 16];
            values.CopyPixels(imgPixels, 16, 0);
            return imgPixels;
        }

        private int CalcAverage(byte[] imgPixels)
        {
            int sum = 0;
            for (int i = 0; i < 16; i++)
            {
                for(int j = 0; j < 16; j++)
                {
                    sum += imgPixels[i * 16 + j];
                }
            }

            return sum / 256;
        }

        //Compare hash
        public static Int32 CalcSimilarDegree(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException();
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    count++;
            }
            return count;
        }        
    }
}

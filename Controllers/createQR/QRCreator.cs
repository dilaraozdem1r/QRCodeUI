using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

namespace QRCodeUI
{
    public class QRCreator
    {
        public static IActionResult CreateQr(string name, string email)
        {
            // QR kod oluşturmak için ZXing.Net kütüphanesini kullan
            var qrCodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 4, // Marjı ayarlayın
                    CharacterSet = "UTF-8" // Karakter kümesini belirtin (isteğe bağlı)
                }
            };
            var pixelData = qrCodeWriter.Write($"{email} {name}");
            var barcodeBitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppArgb);

            for (int y = 0; y < pixelData.Height; y++)
            {
                for (int x = 0; x < pixelData.Width; x++)
                {
                    int offset = (y * pixelData.Width + x) * 4;
                    int alpha = pixelData.Pixels[offset + 3];
                    int red = pixelData.Pixels[offset + 2];
                    int green = pixelData.Pixels[offset + 1];
                    int blue = pixelData.Pixels[offset];

                    if (alpha == 0)
                    {
                        barcodeBitmap.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        Color color = Color.FromArgb(alpha, red, green, blue);
                        barcodeBitmap.SetPixel(x, y, color);
                    }
                }
            }

            // Oluşturulan QR kodunu resim olarak göstermek için FileContentResult olarak döndürün
            MemoryStream ms = new MemoryStream();
            barcodeBitmap.Save(ms, ImageFormat.Png);
            byte[] barcodeBytes = ms.ToArray();
            return new FileContentResult(barcodeBytes, "image/png");
        }
    }
}

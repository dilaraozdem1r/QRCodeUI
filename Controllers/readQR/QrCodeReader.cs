using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using System.Drawing;
using System;

namespace QRCodeUI
{
    public class QrCodeReader
    {
        private VideoCaptureDevice _videoSource;

        public QrCodeReader(FilterInfo captureDevice)
        {
            _videoSource = new VideoCaptureDevice(captureDevice.MonikerString);
            _videoSource.NewFrame += VideoSource_NewFrame;
        }

        public void StartReading()
        {
            _videoSource.Start();
        }

        public void StopReading()
        {
            _videoSource.SignalToStop();
            _videoSource.WaitForStop();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                var barcodeReader = new ZXing.BarcodeReaderGeneric();
                var bitmap = (Bitmap)eventArgs.Frame.Clone();
                var luminanceSource = new BitmapLuminanceSource(bitmap);
                var result = barcodeReader.Decode(luminanceSource);
                if (result != null)
                {
                    Console.WriteLine($"QR kod içeriği: {result.Text}");

                    _videoSource.SignalToStop();
                    _videoSource.WaitForStop();

                    // Uygulamadan çıkın
                    Environment.Exit(0);
                }
            }
            catch (Exception) { }
        }
    }
}

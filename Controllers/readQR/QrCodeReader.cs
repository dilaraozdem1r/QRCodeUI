using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using System.Drawing;
using System;
using System.IO;

namespace QRCodeUI
{
    public class QrCodeReader
    {
        private VideoCaptureDevice _videoSource;
        private Result _result = null;
        private string _previousResult = null;

        public QrCodeReader(FilterInfo captureDevice)
        {
            if (captureDevice != null)
            {
                _videoSource = new VideoCaptureDevice(captureDevice.MonikerString);
                _videoSource.NewFrame += VideoSource_NewFrame;
            }
            else
            {
                throw new ArgumentNullException(nameof(captureDevice), "Capture device is null.");
            }
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
                _result = barcodeReader.Decode(luminanceSource);
                if (_result != null)
                {
                    string filePath = Path.Combine("qrCodeResult.txt");

                    System.IO.File.WriteAllText(filePath, _result.Text);

                    // _videoSource.SignalToStop();
                    // _videoSource.WaitForStop();

                    // Uygulamadan çıkın
                    //Environment.Exit(0);
                    // Instead of exiting the application, we should stop reading the QR code.
                    // this.StopReading();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }
    }
}

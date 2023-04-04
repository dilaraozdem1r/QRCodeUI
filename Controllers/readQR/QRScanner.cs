using System;
using AForge.Video.DirectShow;
using System.Drawing;


namespace QRCodeUI

{
    public class QRScanner
    {
       public static void ScanQr()
        {

            var cameraDevice = new CameraDevice();
            var devices = cameraDevice.GetCaptureDevices();

            if (devices.Count == 0)
            {
                Console.WriteLine("Kamera bulunamadı!");
                return;
            }

            Console.WriteLine("Kamerayı seçin:");
            for (int i = 0; i < devices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {devices[i].Name}");
            }

            int selectedIndex = int.Parse(Console.ReadLine()) - 1;

            var qrCodeReader = new QrCodeReader(devices[selectedIndex]);
            qrCodeReader.StartReading();

            Console.WriteLine("QR kod okuyucu çalışıyor, 'q' tuşuna basarak çıkabilirsiniz.");
            while (Console.ReadKey().Key != ConsoleKey.Q) { }
            qrCodeReader.StopReading();
        }
    }
}

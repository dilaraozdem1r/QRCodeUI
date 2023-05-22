using System;
using AForge.Video.DirectShow;

namespace QRCodeUI
{
    public class QRScanner
    {
        private readonly string _camerasFilePath;
        private readonly CameraDevice _cameraDevice;

        public QRScanner(string camerasFilePath)
        {
            _camerasFilePath = camerasFilePath;
            _cameraDevice = new CameraDevice(camerasFilePath);
        }

        public static FilterInfo GetDeviceByIndex(int index, string camerasFilePath)
        {
            var cameraDevice = new CameraDevice(camerasFilePath);
            var devices = cameraDevice.GetCaptureDevices();

            if (index >= -1 && index < devices.Count)
            {
                return devices[index + 1];
            }

            throw new ArgumentOutOfRangeException(nameof(index), "Invalid index value.");
        }
    }
}

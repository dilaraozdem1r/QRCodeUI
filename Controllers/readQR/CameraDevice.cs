using AForge.Video.DirectShow;
using System.IO;
using System.Linq;

namespace QRCodeUI
{
    public class CameraDevice
    {
        private readonly string _camerasFilePath;

        public CameraDevice(string camerasFilePath)
        {
            _camerasFilePath = camerasFilePath;
        }

        public FilterInfoCollection GetCaptureDevices()
        {
            FilterInfoCollection devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (!File.Exists(_camerasFilePath))
            {
                File.WriteAllLines(_camerasFilePath, devices.Cast<FilterInfo>().Select(x => x.Name));
            }
            return devices;
        }
    }
}

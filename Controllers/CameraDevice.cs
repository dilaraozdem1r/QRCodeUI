using AForge.Video.DirectShow;

namespace QRCodeUI
{
    public class CameraDevice
    {
        public FilterInfoCollection GetCaptureDevices()
        {
            return new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }
    }
}

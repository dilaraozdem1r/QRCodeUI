namespace QRCodeUI.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

public class ScanQrViewModel
    {
        public List<string> CameraList { get; set; }
        public string QrCodeResult { get; set; }
    }

public class UserInput
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string Date { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Tel { get; set; }
    public string Job { get; set; }
    public string Education { get; set; }
    public string ExtraField { get; set; }
}

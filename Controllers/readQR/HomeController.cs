using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QRCodeUI.Models;
using QRCodeUI;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace QRCodeUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _camerasFilePath;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _camerasFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cameras.txt");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ScanQr()
        {
            if (!System.IO.File.Exists(_camerasFilePath))
            {
                var devices = new CameraDevice(_camerasFilePath).GetCaptureDevices();
            }
            var cameraList = System.IO.File.ReadLines(_camerasFilePath).ToList();

            var model = new ScanQrViewModel { CameraList = cameraList, QrCodeResult = null };
            return View(model);
        }

        public IActionResult CreateQr()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult RunQRScanner(string cameraSelection)
        {
            if (System.IO.File.Exists("qrCodeResult.txt"))
            {
                System.IO.File.Delete("qrCodeResult.txt");
            }
            int selectedIndex = int.Parse(cameraSelection) - 1;
            var qrCodeReader = new QrCodeReader(QRScanner.GetDeviceByIndex(selectedIndex, "cameras.txt"));
            qrCodeReader.StartReading();

            // Check every second if the file is created.
            while (!System.IO.File.Exists("qrCodeResult.txt"))
            {
                System.Threading.Thread.Sleep(1000);
            }

            return RedirectToAction("DisplayQRResult");
        }
        public IActionResult DisplayQRResult()
        {
            string qrCodeResult = System.IO.File.Exists("qrCodeResult.txt")
                                ? System.IO.File.ReadAllText("qrCodeResult.txt")
                                : "";
                                
            if (System.IO.File.Exists("qrCodeResult.txt"))
            {
                System.IO.File.Delete("qrCodeResult.txt");
            }

            var cameraList = System.IO.File.Exists(_camerasFilePath)
                                ? System.IO.File.ReadLines(_camerasFilePath).ToList()
                                : new List<string>();

            var model = new ScanQrViewModel { CameraList = cameraList, QrCodeResult = qrCodeResult };
            return View("ScanQr", model);
        }

        public IActionResult RunCreateQr(UserInput userInput)
        {
            string qrCodeResult = QRCreator.CreateQr(userInput);
            ViewBag.BarcodeImage = qrCodeResult;
            ViewBag.FormSubmitted = "True";
            return View("Index");
        }

        public IActionResult HomePage()
        {
            return View();
        }
    }

}

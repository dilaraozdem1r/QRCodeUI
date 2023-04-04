using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QRCodeUI.Models;
using QRCodeUI;

namespace QRCodeUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ScanQr()
    {
        return View();
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
public IActionResult RunQRScanner()
{
    // QR kod tarayıcı işlemini burada gerçekleştirin.
    // Gerekirse, program.cs dosyasından ilgili sınıf ve metodları çağırarak kullanabilirsiniz.
    QRScanner.ScanQr();
    // İşlem tamamlandığında, kullanıcıyı başka bir sayfaya yönlendirebilirsiniz.
    return RedirectToAction("Index"); // Örnek olarak, ana sayfaya yönlendirme yapıldı.
}

public IActionResult RunCreateQr(string name, string email)
{
    var qrCodeResult = QRCreator.CreateQr(name, email);

        // QR kod tarayıcı işlemini burada gerçekleştirin.
        // Gerekirse, program.cs dosyasından ilgili sınıf ve metodları çağırarak kullanabilirsiniz.
        return qrCodeResult;
        // İşlem tamamlandığında, kullanıcıyı başka bir sayfaya yönlendirebilirsiniz.
}



}

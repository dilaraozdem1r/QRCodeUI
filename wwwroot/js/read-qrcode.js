// Sayfa yüklendiğinde çalışacak olan fonksiyon
window.onload = function() {
    // Kamera erişimini kontrol etmek için bir obje oluşturun
    let constraints = { audio: false, video: true };
  
    // Video elementini seçin
    const video = document.querySelector("#qr-video");
  
    // Kamera erişim izni alın
    navigator.mediaDevices.getUserMedia(constraints)
      .then(function(stream) {
        // Stream'i video elementine ata
        video.srcObject = stream;
      })
      .catch(function(error) {
        console.error("Kamera erişimi hatası: ", error);
      });
  
    // QR kodu okumak için bir canvas elementi oluşturun
    const canvasElement = document.getElementById("qr-canvas");
    const canvas = canvasElement.getContext("2d");
  
    // Her 250 ms'de bir video ekranını tarayın
    setInterval(function() {
      canvasElement.width = video.videoWidth;
      canvasElement.height = video.videoHeight;
      canvas.drawImage(video, 0, 0, canvasElement.width, canvasElement.height);
  
      // Taranan görüntüyü bir ImageData nesnesine dönüştürün
      const imageData = canvas.getImageData(0, 0, canvasElement.width, canvasElement.height);
  
      // QR kodunu okuyun
      const code = ZXing.decode(imageData.data, imageData.width, imageData.height);
  
      // QR kodu bulunursa
      if (code) {
        console.log("QR kodu okundu: ", code);
        // QR kodu okunduğunda yapılacak işlemleri burada yazabilirsiniz.
      }
    }, 250);
  };
  
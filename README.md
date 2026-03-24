💰 Kim Milyoner Olmak İster? (C# WinForms)
Bu proje, dünyaca ünlü "Kim Milyoner Olmak İster?" yarışmasının C# ve Windows Forms kullanılarak geliştirilmiş masaüstü versiyonudur. Oyunculara zorluk seviyelerine göre sorular sorulur ve 3 farklı joker hakkı ile 1 Milyon TL'lik büyük ödüle ulaşmaya çalışırlar.

🚀 Özellikler
Dinamik Soru Sistemi: Sorular harici bir .txt dosyasından okunur, bu sayede yeni sorular eklemek çok kolaydır.

Zorluk Seviyeleri: Seviye ilerledikçe (1-12) soruların zorluk katsayısı otomatik olarak artar.

Para Ağacı: Klasik yarışma formatına uygun ödül sistemi (1.000 TL'den 1.000.000 TL'ye).

Baraj Puanları: Belirli seviyelerde (3. ve 8. sorular) elenseniz dahi teselli ikramiyesi kazanırsınız.

🃏 Joker Hakları
Oyunda stratejik olarak kullanabileceğiniz 3 adet joker bulunmaktadır:

50:50: Yanlış olan iki seçeneği eler.

Telefon Jokeri: Rastgele bir tanıdığınızın (Profesör, Arkadaş vb.) fikrini söyler.

Seyirci Jokeri: Seyircilerin şıklara verdiği oyları (%) simüle ederek gösterir.

🛠️ Kurulum ve Çalıştırma
Proje dosyalarını bilgisayarınıza indirin.

Visual Studio ile .sln dosyasını açın.

bin/Debug (veya projenin ana dizini) klasöründe sorular.txt dosyasının olduğundan emin olun.

Projeyi Build edin ve çalıştırın.

📝 Soru Dosyası Formatı (sorular.txt)
Sorular şu formatta eklenmelidir:
Soru Metni|A Şıkkı|B Şıkkı|C Şıkkı|D Şıkkı|DoğruCevapHarfi|ZorlukSeviyesi(1-5)

Örnek:

Türkiye'nin başkenti neresidir?|İstanbul|Ankara|İzmir|Bursa|B|1

💻 Kullanılan Teknolojiler
Dil: C#

Framework: .NET Framework / .NET Core (WinForms)

IDE: Visual Studio

Geliştirici Notu: Bu proje eğitim amaçlı geliştirilmiştir. Mantık hatalarını gidermek veya yeni özellikler (Ses efekti, zamanlayıcı vb.) eklemek için kod üzerinde değişiklik yapabilirsiniz.

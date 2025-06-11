# WebApplication1 - ASP.NET MVC PLC Yönetim Sistemi
Bu proje, ASP.NET MVC mimarisi ile geliştirilmiş bir PLC yönetim sistemidir. Projede kullanıcı girişi, rol tabanlı erişim kontrolü ve sayfa yönlendirme gibi temel işlevler bulunmaktadır. Veritabanı olarak SQLite kullanılmaktadır. Ayrıca, PLC cihazları ile iletişim için EasyModbus kütüphanesi kullanılmaktadır.

## Özellikler
- 🔐 **Giriş Sistemi:** Kullanıcılar kullanıcı adı, şifre veya e-posta ile giriş yapabilir.
- 👤 **Rol Tabanlı Yönlendirme:** Kullanıcının rolüne göre Admin veya PLC sayfalarına yönlendirme yapılır.
- 🛠️ **SQLite Veritabanı:** Proje, platformlar arası taşınabilirlik açısından SQLite veritabanı kullanmaktadır.
- 🔌 **Modbus TCP Desteği:** EasyModbus NuGet paketi ile PLC cihazlarına bağlanarak veri okuma/yazma işlemleri gerçekleştirilir.
- 📋 **Kullanıcı Yönetimi:** Admin, kullanıcıları görüntüleyebilir, silebilir ve rollerini yönetebilir.
- 🌐 **Çok Dilli Destek:** Proje, kullanıcı deneyimini artırmak için Türkçe, İngilizce ve Almanca dillerinde çalışabilen çok dilli arayüze sahiptir. Kullanıcılar, web sitesi üzerindeki dil seçici menü aracılığıyla tercih ettikleri dili seçebilir ve seçilen dil sayfaların metinlerine yansıtılır. Dil tercihi tarayıcıda saklanır ve sayfa yenilense bile tercih korunur.

## Sayfa ve Controller Yapısı
LoginController.cs: Giriş işlemleri

AdminController.cs: Kullanıcı yönetimi (Admin yetkili)

PLCController.cs: PLC sayfalarına yönlendirme

### Görünümler (View
Views/Admin/Index: Kullanıcı listeleme ve yönetim ekranı

Views/PLC/Index: Ana PLC sayfası

Views/PLC/IndexHidrolik: PLC Hidrolik ekranı

## Kurulum
Bu projeyi klonlayın:
git clone [https://github.com/EmreeYsr/WebApplication1.git](https://github.com/EmreeYsr/ASP.NET-MVC-PLC-Yonetim-Sistemi.git)

Projeyi Visual Studio ile açın.

App_Data klasörü altındaki SQLite veritabanı dosyasının bulunduğundan emin olun.

web.config içindeki bağlantı cümlesini SQLite'a göre yapılandırın.

Gerekli NuGet paketlerini yükleyin:

System.Data.SQLite

EasyModbusCore

Projeyi çalıştırın (Ctrl + F5).

##  Gereksinimler
Visual Studio 2019 veya üzeri

.NET Framework (MVC uyumlu sürüm)

NuGet Paketleri:

System.Data.SQLite

EasyModbusCore

## Geliştirici
Bu proje **Emre Yaşar** tarafından geliştirilmiştir.

📧 İletişim: ysr.emre.07@gmail.com

![Screenshot_6](https://github.com/user-attachments/assets/b03a4707-8c94-4a0c-9d12-86e2d3dd304d)

![Screenshot_1](https://github.com/user-attachments/assets/1da41670-ae83-4ca2-af42-1210a260cfec)

![Screenshot_8](https://github.com/user-attachments/assets/99573b66-b58e-46b5-9bd1-a2c6ad557768)

![Screenshot_9](https://github.com/user-attachments/assets/b999957d-eef9-4ba5-9fb6-5248e1281334)

![Screenshot_7](https://github.com/user-attachments/assets/40fa8007-89e7-417d-a755-32ae8ad449dd)









# BankingWebApi

# Banka API Projesi

Bu proje, .NET platformu üzerinde RESTful API geliştirmek amacıyla oluşturulmuştur. Gerçek bir bankacılık sistemi simülasyonu sunmaktadır.

## İçindekiler

- [Amaç](#amaç)
- [Özellikler](#özellikler)
- [Kullanım](#kullanım)
- [Testler](#testler)
- [Docker Entegrasyonu](#docker-entegrasyonu)
- [Kod Kalitesi](#kod-kalitesi)

## Amaç

Bu proje, öğrencilere .NET platformu üzerinde RESTful API tasarlama ve geliştirme konusunda pratik deneyim kazandırmayı amaçlamaktadır. Temel bankacılık işlevlerini içeren bir API geliştirerek, öğrencilerin güvenlik, veri tabanı yönetimi ve hizmet odaklı mimari konularında beceri kazanmalarını hedeflemektedir.

## Özellikler

- Kullanıcı kaydı ve yönetimi
- Hesap işlemleri, bakiye sorgulama ve güncelleme
- Para transferleri ve işlem kayıtları
- Kredi başvurusu, ödeme planları ve sorgulama
- Otomatik ödeme ayarları ve yönetimi
- Müşteri destek talepleri ve takibi

## Kullanım

Proje kullanımı için aşağıdaki adımları takip edebilirsiniz:

1. Proje dosyalarını bilgisayarınıza indirin.
2. Gerekli bağımlılıkları yüklemek için terminal veya komut istemcisinde `dotnet restore` komutunu çalıştırın.
3. Database bağlantısı için terminal veya komut istemcisinde 'add-migration "CreateDb" komutunu daha sonra 'update-database' komutunu çalıştırın.
4. Proje ana dizininde `dotnet run` komutu ile uygulamayı başlatın.

## Testler

Proje için birim testler ve entegrasyon testleri bulunmaktadır. Testleri çalıştırmak için terminalde `dotnet test` komutunu kullanabilirsiniz.

## Docker Entegrasyonu

Proje Docker konteynerleri içinde çalıştırılabilir. Docker Compose kullanarak uygulamanın çoklu konteyner uygulamalarını yönetebilirsiniz. 

Docker entegrasyonu için aşağıdaki adımları takip edebilirsiniz:

1. Dockerfile'ları oluşturun.
2. Docker Compose kullanarak servisleri başlatın.

## Kod Kalitesi

Proje, belirlenen kod kalitesi standartlarına uygun olarak geliştirilmiştir. Kod içerisindeki değişken isimleri anlamlı, metod isimleri açıklayıcıdır. 
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
- [Uygulama Tanıtımı](#uygulama-tanıtımı)

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
3. Proje ana dizininde `dotnet run` komutu ile uygulamayı başlatın.
4. Tarayıcıdan `http://localhost:5000` adresine giderek API'ye erişim sağlayabilirsiniz.

## Testler

Proje için birim testler ve entegrasyon testleri bulunmaktadır. Testleri çalıştırmak için terminalde `dotnet test` komutunu kullanabilirsiniz.

## Docker Entegrasyonu

Proje Docker konteynerleri içinde çalıştırılabilir. Docker Compose kullanarak uygulamanın çoklu konteyner uygulamalarını yönetebilirsiniz. 

Docker entegrasyonu için aşağıdaki adımları takip edebilirsiniz:

1. Dockerfile'ları oluşturun.
2. Docker Compose kullanarak servisleri başlatın.

## Kod Kalitesi

Proje, belirlenen kod kalitesi standartlarına uygun olarak geliştirilmiştir. Kod içerisindeki değişken isimleri anlamlı, metod isimleri açıklayıcıdır. 


# Uygulama Tanıtımı


## Digital Banking Api Diyagramı
![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/129de142-59ea-49ab-8258-884fb6d9149d)


## Kullanıcı Kaydı

Banka uygulamalarında kullanıcının ilk kaydını çalışanlar yapar. Bu yüzden register işlemini admin ve employee rolüne sahip kişiler yapabilir. Register yaptığımızda şifremiz database’e Hash olarak kaydolur. Bir kullanıcı kayıt olurken rol ataması yapılır.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/d97151c5-905e-4923-b758-a6375e1737cc)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/20621d8f-9525-45e1-8024-3534769feb6f)


## Kullanıcı Girişi

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/a2184f11-af20-4153-a5fb-cfe21447ec45)

Eğer kullanıcı bulunursa Hash kodumuz oluşur. Bunu kullanarak bearer’da giriş yapmamız gerekiyor. Giriş yaparken ne rolde olduğumuz çok önemli. Bu Hash’in kullanıcısı admin olduğu için her şeye erişebilir. 

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/2f0132fd-cbce-42f0-ada7-4cbca0c5be3e)

Oluşan Hash’imizi bearer ile giriş yaparak rolümüzün izin verdiği özelliklere erişim sağlıyoruz.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/4e3be95d-882b-4bad-bf87-18dd942ca22c)


Her register yaptığımızda bu bilgiler database’de Logins tablomda saklanır.
![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/af813b87-ac2a-4358-b616-8012e043d13c)

Eğer kullanıcı bulunamazsa:
![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/282c1f93-e0a9-4527-9ba9-7b501f4e8655)


## Yeni Müşteri Oluşturma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/37cd2039-f1f5-4269-9127-344f8de1fee3)

Bankalara bir müşteri ilk kayıt olduğunda bu işlemi çalışanlar yapabilir. Burada da yeni müşteri oluşturma olayını admin ve çalışanlar yapabiliyor.


## Müşteri Güncelleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/f0584731-2a93-44d5-8dde-daeb9058ecb5)

Güncelleme öncesi ve sonrası database’imiz bu şekildedir.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/9c3159f0-70bf-4b75-96c7-b905953158f8)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/c0e07d84-e221-4880-af3f-19a2c10a03ed)



## Validator Örneği

Her tablomuz için validatorlar vardır. Bir tane örneği bu şekildedir.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/9fb432b8-9e38-4d91-b328-ceddba2c9477)

Validatorlar gerekli olan her sınıf için düzgün şekilde çalışmaktadır. Örneğin müşteri güncelleme yaparken adress kısmında 10 karakterden az bir karakter girdiğimde 500 hatası dönecek hatanın ne olduğunu bize söylecektir.

## Kullanıcı Silme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/b9020cd4-7c39-46d1-bdfb-cf9bdc1363d7)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/af46b2fc-c9b5-4773-aeb1-01c5898b72f1)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/edce9f7f-a1bb-48a3-837b-e0b4027bf544)


## Kullanıcı Destek Kaydı Oluşturma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/e7e6eca0-9149-40b8-9330-896c50d08330)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/5ac2b54d-5401-4103-a0dd-6f6a8702320b)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/47e2afd7-678b-40d1-8fef-2bb593a13acc)

Destek kayıtlarını kullanıcılar oluşturabilir. Destek kaydını oluşturduğunda isAnswered’a otomatik olarak false atanır. Answered ve AnsweredDate boş olarak database’e kayıt edilir. 


## Müşteri ID ile Destek Kaydı Arama

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/07821187-117a-44b7-89e8-c7319343dbb9)

Bir müşterinin tüm kayıtlarını bu şekilde görebilmekteyiz. Bu kayıtları müşteriId ile arama olduğu için sadece admin ve çalışanlar görebilmektedir


## Destek Kaydı ID'si ile Kayıt Arama

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/b17b2bf8-5a5a-451f-bab1-8a7150f91e25)

Banka çalışanları müşteriId haricinde destek kayıtlarının ID’si ile ve kayıtları görebilmektedir.


## Cevaplanmamış Destek Kayıtlarını Listeleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/34a8b0e8-e4a7-4126-b694-39729af40adf)

Banka çalışanları isAnswered yani cevaplandı mı değişkeni false olan ilk destek kaydını alır ve bilgilerini görüntüler. Burada destek kayıtları tarih sırasına göre sıralıdır. Bu fonksiyon çalıştığında cevaplanmamış ve ilk yazılan destek kaydı görüntülenir.


## Destek Kaydını Cevaplama

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/6caac248-6709-47ae-bb30-75054957dcf3)

Banka çalışanı id ile görüntülemiş olduğu mesaja bu şekilde cevap verebilmektedir. Cevap verdikten sonra isAnswered değişkeni true olur ve destek kaydımızın boş olan kısımları (answered, isAnswered, answeredDate) doldurulur ve veritabanına işlenir.


## Cevaplanmamış Destek Kayıtlarını Listeleme

Eğer cevaplanmamış hiç kayıt kalmadıysa:

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/f566d6a0-2a36-4aa4-a911-153506d33f7b)

Eğer bütün destek kayıtları cevaplanırsa banka çalışanlarına “Support request not found” uyarısı verilecektir.


## Hesap Oluşturma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/ae14681e-8971-4af5-a78b-f8ae19af59ea)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/7f82a916-0442-4989-96e4-cfad5fe0d08e)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/4785977e-5fe0-4dfa-b50f-85ee5bb7bf43)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/aaad37a1-70ef-4018-8b9a-5c9619cdc0bf)


Hesap oluştururken bankScore default “0” olarak tanımlanır. Bu skor ihtiyaç duyulduğunda hesaplanır ve veritabanına işlenir. Kredi çekme gibi olaylarda BankScore hesaplanır. Hesap türleri “6” tanedir. 

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/e1cb5cb3-f54a-462f-8b7d-6308973f721a)

Bir müşterinin tüm hesapla bilgilerini bu şekilde görebilmekteyiz. 

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/93e7695a-510d-44cd-9c34-0531243cca2a)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/9b9bdb5e-590f-43cf-b206-8fef5ac1a503)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/d39c3f05-b6f4-4f9c-80f0-6407b3c80a1a)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/f28eae03-297d-466e-a6c8-32e60348dea3)

Müşterinin banka hesabındaki miktarı ve maaş bilgisini bu şekilde güncellemekteyiz.


## Para Çekme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/43e333f2-2f15-4a98-bfea-aab606b88b6b)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/7d8244d2-75cd-4b3d-a833-c5f61ba1e584)

Para çektikten sonra günlük transfer limitimize işlenmektedir. Bakiyemizden gönderilen para kadar miktar eksilir ve

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/d291a9f0-eb01-45cd-aa1c-e2998165daba)

bu transfer sonucunda database’ime transfer işlemlerimizbu şekilde kayıt altına alınmaktadır.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/e997afdd-4391-4af2-80ac-a67142df3f3b)

Çekme işleminde günlük limitin üstünde bir para çekersek yada bakiyemizin üstüne bir çekim talep ettiğimizde işlemi gerçekleştirmeden uyarı vermektedir.


## Para Yatırma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/67bae125-a862-4205-91df-02cf0f4b29a9)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/aef75dd0-5d42-40ca-b64c-d45c29e7e22e)

Para yatırma sonucunda transaction type 2 olarak para çekme sonucunda transaction type 1 olarak database’imize kayıt olmaktadır.


## Otomatik Ödeme Talimatları

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/2530e997-e536-4873-a9f9-dc5a61aa547d)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/ffb7e59a-ebb2-425b-bb68-637bcbc63f22)

Otomatik ödeme talimatlarını oluşturduğumuzda bir ödeme günü çekiyoruz. Bu ödeme gününde her ay geçtiğimiz gün ödeme talimatı veriliyor. Otomatik ödeme talimatları her gün bir service tarafından kontrol edilmektedir. Ödeme günü geldiğinde hesapta para olup olmadığı bakılıyor. Eğer para varsa fatura ödeniyor ve isPayment değişkeni true olurken hesaptan fatura miktarı kadar para eksiliyor.


## Müşterinin Tüm Otomatik Ödeme Talimatlarını Görüntüle

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/0e4b0316-720b-427d-b346-04e2279990f3)


## Faturayı Zamanı Gelmeden Ödeme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/49f49de3-962e-4f39-9de4-9b403f7e7aa1)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/c29911ef-9fda-4e4a-99c4-ce4615f6df3e)

Ödeme işlemini eğer istersek zamanı gelmeden ödeyebiliyoruz. Ödemeyi yaptığımızda isPayment değişkenimiz true şeklinde değiştirildi.


## Otomatik Ödeme Talimatlarını Silme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/aaa03733-6845-44b1-adc4-f93fd54fad92)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/926920a2-17c1-4284-9a64-62072f7bdd25)


## Kredi Oluşturma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/da8db56c-c424-4e43-beab-a2df374331d6)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/0ec380e9-8dcd-4859-90de-a9ba25519dbc)


## Tüm Kredileri Listeleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/d1721dc2-6c1e-46c2-bfde-599a8591d4d6)


## Müşteriye Ait Olan Kredileri Listeleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/92ec38fe-36ce-4d29-b214-f023952b8957)


## Kredi Çekme İşlemi

Kredi çekme işleminde iki tane fonksiyonumuz vardır. Bunların biri normal kredi çekme diğeri ise yüksek miktarda kredi çekmedir. Belirlediğimiz miktar (100bin) üstünde kredi çekmek için HighLevelUser rolüne sahip olmanız gerekmektedir. Eğer normal bir User rolüne sahipseniz 100bin’nin altında kredi çekebilirsiniz. Kredi çekme işlemini sadece banka çalışanları yapabilmektedir. Kredi çekme işlemi yaparken ilk önce banka skorunuz hesaplanır. 

Banka skoru şu şekilde hesaplanmaktadır : 
Bankalar gelirinizin %50 fazlasından yukarı bir aylık ödemeye sahip krediyi size vermeyecektir. Ben de hesaplama yaparken bu bilgi doğrultusunda hesaplama yaptım. Maaşımızın %50’sine hesaplıyoruz. Daha sonra tüm otomatik ödeme talimatlarımızın aylık gideriniz topluyoruz ve bunu maaşımızın %50’sinden çıkarıyoruz. Bu şekilde banka skoru hesaplanıyor ve çekebileceğimiz maksimum kredi tutarını banka çalışanı görüntüleyerek çekmek istediğiniz krediye onay yada red veriyor.

Örnek BankScore hesaplama => Maaş = 20.000, Otomatik ödemeler => Telefon = 200, Internet = 300
BankScore => (20.000) /2 = 10.000 => 10.000 – (200 + 300) = 9.500
Bu hesaplama sonrasında aylık kredisi 9.500 olan bir kredi çekebilmekteyiz. 


## Banka Skorumuzun Üstünde Bir Kredi Çekme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/8bd15a9d-9698-4164-b0b2-76d07d65274d)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/d010011e-7c9b-4aa3-8e1e-f3e61f7ff529)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/611b6dd1-ca30-4a26-93f2-634abfe1bf2c)

2200 Aylık ödemesi olan bir kredi çekebilirsiniz uyarısı verilmektedir. Bu krediye banka çalışanı red verecektir.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/852c0a38-c17d-4254-b6a6-9d3db4416961)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/14204fc8-7846-4fcb-91c8-5e6574773c4d)

High level bir müşteri olmadığımız için 100bin’in üstünde bir kredi çekmeye çalışırsak kredi red olacaktır.


Tüm parametreler onaylanırsa krediye onay verilecektir.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/10280842-8346-40dd-a994-ee40446ceaf1)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/1e9bc90b-c798-4c56-b25d-f26d66b0c846)


## Yüksek Miktarda Kredi Çekme (High Level Kullanıcılar İçin)
![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/8c991afb-d471-4491-a82c-67a497223edf)




# BankingWebApi

# Banka API Projesi

Bu proje, .NET platformu üzerinde RESTful API geliştirmek amacıyla oluşturulmuştur. Gerçek bir bankacılık sistemi simülasyonu sunmaktadır.

## İçindekiler

- [Amaç](#amaç)
- [Özellikler](#özellikler)
- [Kullanım](#kullanım)
- [Testler](#testler)
- [Docker Entegrasyonu](#docker-entegrasyonu)
- [Kodun Çalışması](#kodun-calismasi)

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
4. Projeyi çalıştırdıktan sonra açılan Swagger sayfasında role kısmına "Admin" yazarak kayıt olun. (Admin rolü tüm fonksiyonları çalıştırabilecektir.)
5. Login yaptıktan sonra oluşan HashKey'i koplayın ve sayfanın sağ üstündeki kilit işaretine tıklayarak başına bearer "HashKey" imizi girerek tüm fonksiyonlara erişim sağlayın.

## Testler

Proje için birim testler ve entegrasyon testleri bulunmaktadır. Testleri çalıştırmak için terminalde `dotnet test` komutunu kullanabilirsiniz.

## Docker Entegrasyonu

Proje Docker konteynerleri içinde çalıştırılabilir. Docker Compose kullanarak uygulamanın çoklu konteyner uygulamalarını yönetebilirsiniz. 

Docker entegrasyonu için aşağıdaki adımları takip edebilirsiniz:

1. Dockerfile'ları oluşturun.
2. Docker Compose kullanarak servisleri başlatın.

## Kodun Çalışması

## Digital Banking Api Diyagramı

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/288bd0eb-12cb-4fac-a09a-dd062e321150)


## Kullanıcı Kaydı

Banka uygulamalarında kullanıcının ilk kaydını çalışanlar yapar. Bu yüzden register işlemini admin ve employee rolüne sahip kişiler yapabilir. Register yaptığımızda şifremiz database’e Hash olarak kaydolur. Bir kullanıcı kayıt olurken rol ataması yapılır. Tanımlı roller harici bir giriş yapılırsa sistem uyarısı verir ve kayıt iptal olur.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/a4000e99-5c15-4d7a-9ae5-679767096d35)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/b1001c63-6562-4365-850d-29eed2f41d87)


## Kullanıcı Girişi

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/7f650d43-3899-4a28-935c-9eae827bb666)

Eğer kullanıcı bulunursa Hash kodumuz oluşur. Bunu kullanarak bearer’da giriş yapmamız gerekiyor. Giriş yaparken ne rolde olduğumuz çok önemli. Bu Hash’in kullanıcısı admin olduğu için her şeye erişebilir. 

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/adfc7ed4-7ec3-4246-8198-84fef95565b9)

Oluşan Hash’imizi bearer ile giriş yaparak rolümüzün izin verdiği özelliklere erişim sağlıyoruz.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/fc4364cd-8578-4be1-b004-9902beb99c31)

Her register yaptığımızda bu bilgiler database’de Logins tablomda saklanır.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/1ba3429f-5436-4903-b021-19368beec333)

Eğer kullanıcı bulunamazsa:

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/0606d824-a160-48d7-b208-ae9cf286e642)


## Yeni Müşteri Oluşturma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/25c26b16-4529-41d1-80d8-634689990cb7)

Bankalara bir müşteri ilk kayıt olduğunda bu işlemi çalışanlar yapabilir. Burada da yeni müşteri oluşturma olayını admin ve çalışanlar yapabiliyor.


## Müşteri Güncelleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/fa4da5a0-8123-4b17-8f65-644b30894842)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/9be3cc12-86bd-4c12-b72f-0ca547c87a7a)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/b4013f90-efad-4eeb-b5e4-842827f93dab)

Güncelleme öncesi ve sonrası database’imiz bu şekildedir.

## Validator Örneği

(HER TABLOMUZ İÇİN VALIDATORLAR VARDIR. BİR TANE ÖRNEĞİ BU ŞEKİLDEDİR)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/b4825691-2e46-49b5-87ee-079698c96b03)

Validatorlar gerekli olan her sınıf için düzgün şekilde çalışmaktadır. Örneğin müşteri güncelleme yaparken adress kısmında 10 karakterden az bir karakter girdiğimde system exception fırlatacaktır ve neden başarısız olduğunu bize söylecektir.


## Kullanıcı Silme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/283e4a05-8a5d-490a-9874-d883b23227ec)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/5fb2d883-115f-4b5e-8b8b-a4cf8a9b311b)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/49b4ed2f-f9d6-4129-bd49-6fdb3f724d12)


## Kullanıcı Destek Kaydı Oluşturma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/f76e00f2-494d-4f09-b806-8b8fcddcc070)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/ba4ed4f5-3d1b-49d8-b267-c035cb4f2ed8)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/b20df613-2806-4cd4-a2f2-e6a5093acd3b)

Destek kayıtlarını kullanıcılar oluşturabilir. Destek kaydını oluşturduğunda isAnswered’a otomatik olarak false atanır. Answered ve AnsweredDate boş olarak database’e kayıt edilir. 


## Müşteri ID ile Destek Kaydı Arama

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/4e735a8f-7289-445b-acf6-764903fdcbbe)

Bir müşterinin tüm kayıtlarını bu şekilde görebilmekteyiz. Bu kayıtları müşteriId ile arama olduğu için sadece admin ve çalışanlar görebilmektedir


## Destek Kaydı ID'si ile Kayıt Arama

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/3b217f7d-463a-416e-8e72-9f9ea8164d00)

Banka çalışanları müşteriId haricinde destek kayıtlarının ID’si ile ve kayıtları görebilmektedir.


## Cevaplanmamış Destek Kayıtlarını Listeleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/1ee0dd77-c8d6-4ccb-8452-f0cf1a25746b)

Banka çalışanları isAnswered yani cevaplandı mı değişkeni false olan ilk destek kaydını tarihe göre alır ve bilgilerini görüntüler. Burada destek kayıtları tarih sırasına göre sıralıdır. Bu fonksiyon çalıştığında cevaplanmamış ve ilk yazılan destek kaydı görüntülenir.


## Destek Kaydını Cevaplama

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/19b0cb84-7f8d-4d0f-920d-815e5d60be04)

Banka çalışanı id ile görüntülemiş olduğu mesaja bu şekilde cevap verebilmektedir. Cevap verdikten sonra isAnswered değişkeni true olur ve destek kaydımızın boş olan kısımları (answered, isAnswered, answeredDate) doldurulur ve veritabanına işlenir.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/8a8aee3d-9462-4451-b19a-6290c1bdca08)


## Cevaplanmamış Destek Kayıtlarını Listeleme (Eğer Cevaplanmamış Hiç Kayıt Kalmadıysa)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/ac2955a5-5979-48fb-989f-942f0d128fab)

Eğer bütün destek kayıtları cevaplanırsa banka çalışanlarına “Support request not found” uyarısı verilecektir.


## Hesap Oluşturma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/346cd10a-0f9c-4f8d-9faa-04a5c67d4688)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/08d09ac8-6eac-494b-a21f-e7b1d16b5c19)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/70fbf7e0-c666-4f85-9528-a7f1ef7547a1)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/3eca5c59-c7c9-4155-bd8c-66e37586cd10)

Hesap oluştururken bankScore default “0” olarak tanımlanır. Bu skor ihtiyaç duyulduğunda hesaplanır ve veritabanına işlenir. Kredi çekme gibi olaylarda BankScore hesaplanır. Hesap türleri “6” tanedir. 

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/b8f6a1c8-ef42-4362-8c76-0626177f181a)

Bir müşterinin tüm hesapla bilgilerini bu şekilde görebilmekteyiz. 

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/f2eaaf67-51a1-4644-9995-fceed01aa7cf)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/7e780804-107e-4f7a-aa59-92af9c2b2424)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/343ba5fc-3752-4b4b-b5be-d5bdccc20bc1)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/3a9cc739-471a-4894-bda1-372028671e1b)

Müşterinin banka hesabındaki miktarı ve maaş bilgisini bu şekilde güncellemekteyiz.


## Para Çekme (Limitli Kullanıcı)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/a0ca07d9-c0b7-43b0-a945-2ae03b59f4de)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/c24e3f50-90d4-4d39-9265-cfbfa4f0c674)

Para çektikten sonra günlük transfer limitimize işlenmektedir. Bakiyemizden gönderilen para kadar miktar eksilir ve

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/5ea9bf6f-d571-46a6-8cf6-b55e7dc31720)

bu transfer sonucunda database’ime transfer işlemlerimiz bu şekilde kayıt altına alınmaktadır.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/68613363-f2a1-42db-82a1-7a47ebbf2a52)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/185acc17-ccd4-4752-9ecc-64ad9a449c4b)

Çekme işleminde günlük limitin üstünde bir para çekersek yada bakiyemizin üstüne bir çekim talep ettiğimizde işlemi gerçekleştirmeden uyarı vermektedir.


## Para Çekme (Limitsiz Kullanıcı)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/cf908ab1-a61f-4e72-8ce7-98e782fc18ad)


## Para Yatırma (Limitli Kullanıcı)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/f124ccd1-cd0e-4dc5-8fee-7d1423d28ee1)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/232f3787-c347-4eb5-ac17-06a6f4914c00)

Para yatırma sonucunda transaction type 2 olarak para çekme sonucunda transaction type 1 olarak database’imize kayıt olmaktadır.


## Para Yatırma (Limitsiz Kullanıcı)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/d4021c17-94ad-4667-94e0-028e583bbc71)


## Otomatik Ödeme Talimatları (Faturalar)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/0ab0916a-9a1d-4ddc-b193-0313b695a13a)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/de072954-9097-42a6-a4fc-c25e49142c8d)

Otomatik ödeme talimatlarını oluşturduğumuzda bir ödeme günü çekiyoruz. Bu ödeme gününde her ay geçtiğimiz gün ödeme talimatı veriliyor. Otomatik ödeme talimatları her gün bir service tarafından kontrol edilmektedir. Ödeme günü geldiğinde hesapta para olup olmadığı bakılıyor. Eğer para varsa fatura ödeniyor ve isPayment değişkeni true olurken hesaptan fatura miktarı kadar para eksiliyor.


## Müşterinin Tüm Ödeme Talimatlarını Görüntüleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/7a4d2597-6949-4b14-9a75-56b2e35d0398)


## Faturayı Zamanı Gelmeden Ödeme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/01f6a4a1-afca-4a0a-83b0-cc8d42fdc3d3)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/e954a543-20fe-4775-8f27-8c829e660e47)

Ödeme işlemini eğer istersek zamanı gelmeden ödeyebiliyoruz. Ödemeyi yaptığımızda isPayment değişkenimiz true şeklinde değiştirildi.


## Otomatik Ödeme Talimatlarını Silme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/c5aff43f-8b3f-424d-aa01-cfd127a0e7f6)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/c382d305-851a-4dd5-89f9-bdd9abbdfb83)


## Kredi Oluşturma

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/458bdb73-24af-49ae-9bc0-1e3d2374b872)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/b58857f0-3d33-46a9-95f6-79d1044caf45)


## Tüm Kredileri Listeleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/7debbaab-7301-476e-bcdc-66e35612788d)


## Müşteriye Ait Olan Kredileri Listeleme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/3543af8e-64c1-48c9-824e-7abb2d386fac)


## Kredi Çekme İşlemi

Kredi çekme işleminde iki tane fonksiyonumuz vardır. Bunların biri normal kredi çekme diğeri ise yüksek miktarda kredi çekmedir. Belirlediğimiz miktar (100 bin) üstünde kredi çekmek için HighLevelUser rolüne sahip olmanız gerekmektedir. Eğer normal bir User rolüne sahipseniz 100 bin’nin altında kredi çekebilirsiniz. Kredi çekme işlemini sadece banka çalışanları yapabilmektedir. Kredi çekme işlemi yaparken ilk önce banka skorunuz hesaplanır. 
Banka skoru şu şekilde hesaplanmaktadır : 
Bankalar, gelirinizin %50 fazlasından yukarı bir aylık ödemeye sahip krediyi size vermeyecektir. Ben de hesaplama yaparken bu bilgi doğrultusunda hesaplama yaptım. Maaşımızın %50’sine hesaplıyoruz. Daha sonra tüm otomatik ödeme talimatlarımızın aylık giderini ve var olan kredilerinizin aylık ödemesini topluyoruz. Bunu maaşımızın %50’sinden çıkarıyoruz. Bu şekilde banka skoru hesaplanıyor ve çekebileceğimiz maksimum kredi tutarını banka çalışanı görüntüleyerek çekmek istediğiniz krediye onay yada red veriyor.
Örnek BankScore hesaplama => Maaş = 20.000, Otomatik ödemeler => Telefon = 200, Internet = 300,
				Krediler => Aylık Ödeme = 1500, 2500
BankScore => (20.000) /2 = 10.000 => 10.000 – ((200 + 300) + (1500 + 2500))= 5500
Bu hesaplama sonrasında aylık kredisi 5.500 olan bir kredi çekebilmekteyiz. 


## Banka Skorumuzun Üstünde Bir Kredi Çekme

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/4a1dd449-21c1-49c3-8386-04f80e234adb)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/bebb3eca-f303-4506-86f6-e6d99d84999b)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/d1ef7ca8-d564-4b2e-93b3-155c564ab8e6)

2200 Aylık ödemesi olan bir kredi çekebilirsiniz uyarısı verilmektedir. Bu krediye banka çalışanı red verecektir.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/93f8dba5-04a0-4eee-957a-74f2c36bcd8b)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/39a2622f-5717-4a92-94ea-19f2495a9dd5)

High level bir müşteri olmadığımız için 100bin’in üstünde bir kredi çekmeye çalışırsak kredi red olacaktır.

Tüm parametreler onaylanırsa krediye onay verilecektir.

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/5edf898d-e957-4c5a-856b-6d82638bd6a4)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/4d1c4945-eb29-45a2-878b-cd073edb1b93)


## Yüksek Miktarda Kredi Çekme (HighLevelUser Kullanıcılar İçin)

![image](https://github.com/emirhanersoz/BankingWebApi/assets/63202294/4159d3b9-628f-4132-8322-e73abe309e11)

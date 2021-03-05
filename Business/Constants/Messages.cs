using System.Runtime.Serialization;

namespace Business.Constants
{
    public static class Messages
    {
        public static string UsersListed = "Kullanıcılar listelendi";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserFound = "Kullanıcı bulundu";

        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomersListed = "Müşteriler listelendi";
        public static string CustomerFound = "Müşteri bulundu";

        public static string RentalAdded = "Araç kirası eklendi";
        public static string RentalsListed = "Araçlar listelendi";
        public static string RentalFound = "Araç bulundu";

        public static string CarAdded = "Araç eklendi";
        public static string CarsListed = "Araçlar listelendi";

        public static string ColorAdded = "Renk eklendi";

        public static string BrandAdded = "Marka eklendi";

        public static string CarRental = "Araç kirada";
        public static string OldHistoryCanNotBeRecorded = "Geçmiş tarihe kira kaydı yapılamaz";
        public static string CanNotRegisterForTheNextDate = "İleri tarihe kira kaydı yapılamaz";

        public static string CarImageAdded = "Araç resmi eklendi";
        public static string CarImageCountLimitExceeded = "Araç resmi ekleme limiti doldu";
        public static string CarImageModified = "Araç resmi değiştirildi";
        public static string CarImageDeleted = "Araç resmi silindi";
        public static string OldCarImageCouldNotBeDeleted = "Eski araba resmi silinemedi";
        public static string NewCarImageCouldNotBeAdded = "Yeni araba resmi eklenemedi";

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string UserRegistered = "Kullanıcı kaydı yapıldı";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Kullanıcı zaten mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";

        
    }
}
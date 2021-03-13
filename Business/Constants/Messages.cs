using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        // Brand
        public static string BrandSuccesfullyAdded = "Marka başarıyla eklendi.";
        public static string BrandSuccesfullyDeleted = "Marka başarıyla silindi";
        public static string AllBrandsSuccesfullyListed = "Tüm markalar listelendi";
        public static string GetBrandByIdSuccesfully = "Marka detayları getirildi.";
        public static string BrandSuccesfullyUpdated = "Marka başarıyla güncellendi";

        //Car
        public static string CarAddedSuccessfully = "Araç başarıyla eklendi.";
        public static string CarInvalidDailyPrice = "Günlük araç kira ücreti sıfır olamaz.";
        public static string InvalidCarName = "Araç ismi en az 2 karakter olmalıdır.";
        public static string CarDeletedSuccessfully = "Araç başarıyla silindi.";
        public static string AllCarsListedSuccessfully = "Tüm araçlar başarıyla listelendi.";
        public static string GetCarByIdSuccessfully = "Araç detayları başarıyla getirildi.";
        public static string GetCarDetailDtoSuccessfully = "Araç detaylarıyla başarıyla getirildi.";
        public static string GetCarsByBrandIdSuccessfully = "Markaya göre araç listesi başarıyla getirildi.";
        public static string GetCarsByColorIdSuccessfully = "Renge göre araç listesi başarıyla getirildi.";
        public static string CarUpdatedSuccessfully = "Araç başarıyla güncellendi.";

        //Color
        public static string ColorAddedSuccessfully = "Renk başarıyla eklendi";
        public static string ColorDeletedSuccessfully = "Renk başarıyla silindi";
        public static string GetAllColorsSuccessfully = "Tüm renkler başarıyla listelendi";
        public static string GetColorByIdSuccessfully = "Renk detayları başarıyla getirildi.";

        //User
        public static string ColorUpdatedSuccessfully = "Renk başarıyla güncellendi.";
        public static string UserAddedSuccessfully = "Kullanıcı başarıyla eklendi.";
        public static string UserDeletedSuccessfully = "Kullanıcı başarıyla silindi";
        public static string AllUsersListedSuccessfully = "Tüm kullanıcılar başarıyla listeledi";
        public static string GetUserByIdSuccessfully = "Kullanıcı detayları başarıyla getirildi.";
        public static string GetUserClaimsSuccessfully="Kullanıcı rolleri başarıyla getirildi.";
        

        //Customer
        public static string UserUpdatedSuccessfully = "Kullanıcı başarıyla güncellendi.";
        public static string CustomerAddedSuccessfully = "Müşteri başarıyla eklendi.";
        public static string CustomerDeletedSuccessfully = "Müşteri başarıyla silindi.";
        public static string GetAllCustomersSuccessfully = "Tüm müşteriler başarıyla listelendi.";
        public static string GetCustomerByIdSuccessfully = "Müşteri detayları başarıyla getirildi.";
        public static string CustomerUpdatedSuccessfully = "Müşteri başarıyla güncellendi.";

        //Rental
        public static string InvalidReturnDate = "Geçersiz geri dönüş tarihi.";
        public static string RantalAddedSuccessfully = "Araç başarıyla kiralandı";
        public static string RentalDeletedSuccessfully = "Kira işlemi başarıyla silindi.";
        public static string GetAllRentalsSuccessfully = "Tüm kiralama işlemleri başarıyla listelendi.";
        public static string GetRentalByIdSuccessfully = "Kira detayları başarıyla getirildi.";
        public static string RentalUpdatedSuccessfully = "Kira işlemi başarıyla güncellendi.";
        public static string RentDetailsListedSuccessfully="Kira detayları listesi başarıyla getirildi.";
        
        //CarImage
        public static string ImageAddedSuccessfully="Resim başarıyla eklendi.";
        public static string ImageDeletedSuccessfully="Resim başarıyla silindi.";
        public static string ImageUpdatedSuccessfully="Resim başarıyla güncellendi.";
        public static string MaksimumImageLimitReached="Bir araç için izin verilen en fazla resim sayısına ulaştınız.";
        
        //Auth
        public static string UserNotFound="Kullanıcı bulunamadı.";
        public static string PasswordError="Şifre hatalı.";
        public static string LoginSuccessfull="Giriş başarılı";
        public static string UserAlreadyExists="Bu kullanıcı zaten mevcut.";
        public static string UserRegistered="Kullanıcı başarıyla kaydedildi.";
        public static string AccessTokenCreated="Access Token başarıyla oluşturuldu.";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        
    }
}

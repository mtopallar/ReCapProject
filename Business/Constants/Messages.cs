using System;
using System.Collections.Generic;
using System.Text;

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
        public static string ColorAddedSuccessfully="Renk başarıyla eklendi";
        public static string ColorDeletedSuccessfully="Renk başarıyla silindi";
        public static string GetAllColorsSuccessfully="Tüm renkler başarıyla listelendi";
        public static string GetColorByIdSuccessfully="Renk detayları başarıyla getirildi.";
        public static string ColorUpdatedSuccessfully="Renk başarıyla güncellendi.";
    }
}

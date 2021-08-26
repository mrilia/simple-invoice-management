using SIM.Application.Handlers.Invoices.Command.AddNew;
using System;

namespace SIM.Application.Messages
{
    public class ResponseMessage
    {
        public static string ItemNotFound = "کالا پیدا نشد";
        public static string NameIsRequired = "وارد کردن مقدار فیلد نام ضروری است";        
        public static string FeeIsRequired = "وارد کردن مقدار فیلد قیمت واحد ضروری است";
        public static string IdIsRequired = "وارد کردن مقدار فیلد شناسه ضروری است";
        public static string ItemIdNotExists = "کالایی با این شناسه وجود ندارد";
        public static string ItemExists = "این کالا قبلا ثبت شده است";

        public static string InvoiceNotFound = "فاکتور پیدا نشد";
        public static string NumberIsRequired = "وارد کردن شماره فاکتور ضروری است";
        public static string BuyerNameIsRequired = "وارد کردن نام خریدار ضروری است";
        public static string InvoiceNumberNotUnique = "شماره فاکتور تکراری است";
        public static string PricesNotValid = "مجموع مبالغ فاکتور اشتباه است";
        public static string InvoiceIdNotExists = "فاکتوری با این شناسه وجود ندارد";

    }
}
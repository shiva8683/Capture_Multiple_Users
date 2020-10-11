using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_1_Capture_Multiple_Users.Enums;

namespace Test_1_Capture_Multiple_Users.Helpers
{
    public static class TempDataExtensions
    {
        public static void ToastError(this TempDataDictionary tempData, string message = null)
        {
            tempData.Add(ToastrNotificationsEnum.ValidationOrException.ToString(), string.IsNullOrWhiteSpace(message) ? "Action Required. Kindly review below." : message);
        }

        public static void ToastSuccess(this TempDataDictionary tempData, string message = null)
        {
            tempData.Add(ToastrNotificationsEnum.Success.ToString(), string.IsNullOrWhiteSpace(message) ? "Action Completed Successfully." : message);
        }

        public static string GetToastrMessageValue(this TempDataDictionary tempData, ToastrNotificationsEnum toastrNotificationsEnum)
        {
            return Convert.ToString(tempData[toastrNotificationsEnum.ToString()]);
        }
    }
}
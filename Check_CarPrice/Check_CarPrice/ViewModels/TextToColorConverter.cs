using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Check_CarPrice.ViewModels
{
    public class TextToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value as string;
            //if (text.Contains("ตรวจสอบแล้ว")||text.Contains("อนุมัติ")|| text.Contains("พิจารณายอดสินเชื่อเร่งด่วน") || text.Contains("ดำเนินการปรับปรุง,เปลี่ยน") 
            //    || text.Contains("ซ่อมแซม") || text.Contains("ขออนุมัติยอดสินเชื่อด้วยหลักเกณฑ์อื่น") || text.Contains("รายละเอียดไม่ถูกต้อง") || text.Contains("ไม่ผ่านหลักเกณฑ์"))
            if (text != null)
            {
                if (text=="ไม่ผ่านหลักเกณฑ์"||text=="รออนุมัติ")
                {
                    return Color.Red;
                }
                else if (text == "อนุมัติแล้ว")
                {
                    return Color.Green;
                }
                else if(text == "รอตรวจสอบ")
                {
                    return Color.OrangeRed;
                }
            }
            return Color.Blue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

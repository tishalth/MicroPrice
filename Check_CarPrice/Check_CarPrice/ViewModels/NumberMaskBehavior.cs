using Android.Icu.Text;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Check_CarPrice.ViewModels
{
    public class NumberMaskBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        ///
        /// Detaches when the page is destroyed.
        /// 

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("*** args.NewTextValue *** " + args.NewTextValue + "  -----  args.OldTextValue =  " + args.OldTextValue);
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                // If the new value is longer than the old value, the user is
                //if (args.OldTextValue != null && args.NewTextValue.Length < args.OldTextValue.Length)
                //    return;

                var value = args.NewTextValue;

                string actual_number = "";
                actual_number = value;
                actual_number = actual_number.Replace(",", "");

                string result_last = dataFormat(actual_number);
                System.Diagnostics.Debug.WriteLine("****   result = " + result_last);


                int actual_length = actual_number.Length;

                ((Entry)sender).Text = result_last;
            }
        }

        public static String dataFormat(String text)
        {
            DecimalFormat df = null;
            if (text.IndexOf(".") > 0)
            {//include decimal
                if (text.Length - text.IndexOf(".") - 1 == 0)
                {//include a decimal
                    df = new DecimalFormat("###,##0.");
                }
                else if (text.Length - text.IndexOf(".") - 1 == 1)
                {//include two decimals
                    df = new DecimalFormat("###,##0.0");
                }
                else
                {//include more than two decimal
                    df = new DecimalFormat("###,##0.00");
                }
            }
            else
            {//only integer
                df = new DecimalFormat("###,##0");
            }
            double number = 0.0;
            try
            {
                number = Double.Parse(text);
            }
            catch (Exception e)
            {
                number = 0.0;
            }
            return df.Format(number);

        }

        public  String FormatData (String text)
        {
            DecimalFormat df = null;
            if (text.IndexOf(".") > 0)
            {//include decimal
                if (text.Length - text.IndexOf(".") - 1 == 0)
                {//include a decimal
                    df = new DecimalFormat("###,##0.");
                }
                else if (text.Length - text.IndexOf(".") - 1 == 1)
                {//include two decimals
                    df = new DecimalFormat("###,##0.0");
                }
                else
                {//include more than two decimal
                    df = new DecimalFormat("###,##0.00");
                }
            }
            else
            {//only integer
                df = new DecimalFormat("###,##0");
            }
            double number = 0.0;
            try
            {
                number = Double.Parse(text);
            }
            catch (Exception e)
            {
                number = 0.0;
            }
            return df.Format(number);

        }
    }
}

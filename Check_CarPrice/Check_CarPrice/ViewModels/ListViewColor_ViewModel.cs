using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Check_CarPrice.ViewModels
{
    public class ListViewColor_ViewModel : ViewCell
    {
        public static readonly BindableProperty
           SelectedBackgroundColorProperty =
        BindableProperty.Create("SelectedBackgroundColor",
                                typeof(Color),
                                typeof(ListViewColor_ViewModel),
                                Xamarin.Forms.Color.Accent);
        public Color SelectedBackgroundColor
        {
            get
            {
                return (Color)GetValue
                (SelectedBackgroundColorProperty);
            }
            set
            {
                SetValue(SelectedBackgroundColorProperty, value);
            }
        }
    }
}

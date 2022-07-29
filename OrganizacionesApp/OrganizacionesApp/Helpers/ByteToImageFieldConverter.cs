using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace OrganizacionesApp.Helpers
{
    public class ByteToImageFieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;            
            
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                
                if (imageAsBytes.Length > 0)
                {
                    retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                } else
                {
                    retSource = ImageSource.FromResource("OrganizacionesApp.Resources.Images.noimage.png");
                }                
            }

            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

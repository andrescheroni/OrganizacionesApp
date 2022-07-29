using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace OrganizacionesApp.Helpers
{
    public class ImageFieldToByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] retSource = null;

            if (value != null)
            {
                using (MemoryStream stream = (MemoryStream)value)
                {
                    retSource = stream.ToArray();
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

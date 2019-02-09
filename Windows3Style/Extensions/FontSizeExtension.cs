using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace Windows3Style.Extensions
{
    public class FontSizeExtension : MarkupExtension
    {
        [TypeConverter(typeof(FontSizeConverter))]
        public double Size { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) => Size;
    }
}

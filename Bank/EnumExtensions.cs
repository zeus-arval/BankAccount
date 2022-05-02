using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal static class EnumExtensions
    {
        /// <summary>
        /// Gets Description of Enum value
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="e">Enum value </param>
        /// <returns>Description of Enum value</returns>
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum == false) return string.Empty;

            Type type = e.GetType();
            Array values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var memoryInfo = type.GetMember(type.GetEnumName(val)!);
                    var descriptionAttribute = memoryInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
                return String.Empty;
            }
            return String.Empty;
        }
    }
}

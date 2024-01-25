using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imtihani
{
    public static class ViewExtensions
    {
        public static SelectList ToSelectList(this IEnumerable<object> list, string valueField = "Id", string textField = "LocalizedName", string selectedValue = null)
        {
            if (list != null)
            {
                SelectList selectList = new SelectList(list, valueField, textField, selectedValue);
                return selectList;
            }
            return null;
        }

        public static SelectList ToSelectList<T>(this Enum item, string selectedValue = null) where T : struct, IConvertible
        {

            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");
            var list = Enum.GetValues(typeof(T)).Cast<T>().Select(v => new { Name = v.ToString(), Id = Convert.ToInt32(v) }).ToList();
            return new SelectList(list, "Id", "Name");
        }
    }
}

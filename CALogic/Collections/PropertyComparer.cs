using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace ChannelAdvisor
{
    public class PropertyComparer<T> : IComparer<T>
    {
        PropertyDescriptor Property { get; set; }
        ListSortDirection Direction { get; set; }

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            Property = property;
            Direction = direction;
        }

        public int Compare(T xValue, T yValue)
        {
            // Get property values
            object xVal = GetPropertyValue(xValue, Property.Name);
            object yVal = GetPropertyValue(yValue, Property.Name);

            if ((xVal == null) && (yVal == null))
                return 0;
            if (yVal == null) return 1;
            if (xVal == null) return -1;

            // Determine sort order
            if (Direction == ListSortDirection.Ascending)
            {
                return CompareAscending(xVal, yVal);
            }
            else
            {
                return CompareDescending(xVal, yVal);
            }

        }

        // Compare two property values of any type
        private int CompareAscending(object xValue, object yValue)
        {
            int result;

            // If values implement IComparer
            if (xValue is IComparable)
            {
                result = ((IComparable)xValue).CompareTo(yValue);
            }
            // If values don't implement IComparer but are equivalent
            else if (xValue.Equals(yValue))
            {
                result = 0;
            }
            // Values don't implement IComparer and are not equivalent, so compare as string values
            else result = xValue.ToString().CompareTo(yValue.ToString());

            // Return result
            return result;
        }

        private int CompareDescending(object xValue, object yValue)
        {
            // Return result adjusted for ascending or descending sort order ie
            // multiplied by 1 for ascending or -1 for descending
            return CompareAscending(xValue, yValue) * -1;
        }


        private object GetPropertyValue(T value, string property)
        {
            // Get property
            PropertyInfo propertyInfo = value.GetType().GetProperty(property);

            // Return value
            return propertyInfo.GetValue(value, null);
        }

    }
}

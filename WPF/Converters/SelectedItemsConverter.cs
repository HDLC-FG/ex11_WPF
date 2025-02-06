using System.Collections;

namespace WPF.Converters
{
    internal static class SelectedItemsConverter<T>
    {
        public static T[] ConvertToArray(object selectItems)
        {
            if(selectItems == null) return null;
            var collection = (IList)selectItems;
            T[] options = new T[collection.Count];
            collection.CopyTo(options, 0);
            return options;
        }
    }
}

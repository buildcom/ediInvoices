namespace EDI.Helper
{
    public static class NullableExtenstions
    {

        public static string ToStringDefaultIfEmpty<T>(this System.Nullable<T> obj, string defaultValue,
            bool considerWhiteSpaceIsEmpty = false) where T : struct
        {

            return obj.ToString().DefaultIfEmpty(defaultValue, considerWhiteSpaceIsEmpty);
 
        }

    }
}

namespace KnightTournament.Extensions
{
    public static class ComparerExtension
    {
        public static bool CompareObjectsExtension(this object middle, object first, object second)
        {
            if (middle == null || first == null || second == null)
            {
                return false;
            }

            var m = middle.GetType();
            var n = int.TryParse(first.ToString(), out int f);
            if (int.TryParse(first.ToString(), out int intResult) && int.TryParse(middle.ToString(), out int res1))
            {
                return intResult <= res1 && int.Parse(second.ToString()) >= res1;
            }

            if (double.TryParse(first.ToString(), out double outResult) && double.TryParse(middle.ToString(), out double res2))
            {
                return outResult <= res2 && double.Parse(second.ToString()) >= res2;
            }

            if (DateTime.TryParse(first.ToString(), out DateTime dateTimeResult) && DateTime.TryParse(middle.ToString(), out DateTime res3))
            {
                return dateTimeResult <= res3 && DateTime.Parse(second.ToString()) >= res3;
            }

            return first == second;
        }
    }
}

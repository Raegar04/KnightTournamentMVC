using System.Text;

namespace KnightTournament.Extensions
{
    public static class GetSelectedTypeExtension
    {
        public static string GetSelectedEntityType(this string query)
        {
            var normalQuery = query.ToUpper();
            var fromIndex = normalQuery.IndexOf("FROM");
            var result = new StringBuilder();
            for (int i = fromIndex + 4; i < normalQuery.Length; i++)
            {

                if (normalQuery[i] != ' ' && normalQuery[i] != 'D' && normalQuery[i] != 'B' && normalQuery[i] != 'O' && normalQuery[i] != '.')
                {
                    for (int j = i; j < normalQuery.Length && normalQuery[j] != ' '; j++)
                    {
                        result.Append(normalQuery[j]);
                    }
                    break;
                }


            }
            return result.ToString()[0] + result.ToString().Substring(1, result.ToString().Length - 2).ToLower();
        }
    }
}

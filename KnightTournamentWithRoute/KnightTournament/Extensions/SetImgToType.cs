using KnightTournament.Models.Enums;

namespace KnightTournament.Extensions
{
    public static class ImgExtension
    {
        public static string SetImgToType(this CombatType combatType) 
        {
            switch (combatType) 
            {
                case CombatType.WithHorses:
                    return "https://bogatyr.club/uploads/posts/2023-06/thumbs/1687546541_bogatyr-club-p-ritsarskii-turnir-foni-vkontakte-59.jpg";
                case CombatType.OnSwords:
                    return "https://www.btv.md/wp-content/uploads/2018/10/1116030.jpg";
                case CombatType.BetweenArchers:
                    return "https://parkkyivrus.com/images/uslugi/articles/verhovaya-strelba.jpg";
                case CombatType.FieldBattle:
                    return "https://media.istockphoto.com/id/1329912152/ru/%D0%B2%D0%B8%D0%B4%D0%B5%D0%BE/%D0%B0%D1%80%D0%BC%D0%B8%D0%B8-%D1%81%D1%80%D0%B5%D0%B4%D0%BD%D0%B5%D0%B2%D0%B5%D0%BA%D0%BE%D0%B2%D1%8B%D1%85-%D1%80%D1%8B%D1%86%D0%B0%D1%80%D0%B5%D0%B9-%D1%81%D1%80%D0%B0%D0%B6%D0%B0%D1%8E%D1%89%D0%B8%D1%85%D1%81%D1%8F-%D0%BC%D0%B5%D1%87%D0%B0%D0%BC%D0%B8-%D0%B2%D0%BE%D0%B9%D0%BD%D0%B0-%D1%82%D0%B5%D0%BC%D0%BD%D1%8B%D1%85-%D0%B2%D0%B5%D0%BA%D0%BE%D0%B2-%D1%81%D1%80%D0%B0%D0%B6%D0%B5%D0%BD%D0%B8%D0%B5-%D0%B1%D1%80%D0%BE%D0%BD%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%BD%D1%8B%D1%85.jpg?s=640x640&k=20&c=sDaae1uhaRZwAiP6Xdg2OQejVTs7eLQ0HXYC11IaEtw=";
                default:
                    return "https://upload.wikimedia.org/wikipedia/commons/f/f7/%D0%A1%D0%BE%D0%BB%D0%BD%D1%86%D0%B5%D0%B2_%D0%9A%D1%83%D0%BB%D0%B0%D1%87%D0%BD%D1%8B%D0%B9_%D0%B1%D0%BE%D0%B9_1836.jpg";
            }
        }
    }
}

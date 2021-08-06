namespace Assets.Scrypts.GameData
{
    public class EnemyData
    {
        //расстояния до цели, чтобы защитать, что враг дошел
        public static readonly float InaccuracyToTarget = 0.2f;
        //время, которое враг прячется
        public static readonly float TimeInHideState = 2f;
        //скэйл символов над врагом
        public static readonly float SymbolIconScale = 0.05f;
        //прозрачность символов, когда враг прячется
        public static readonly float SymbolOnHideFadeTo = 0.2f;
        //время, которое символы затухают
        public static readonly float TimeForHideSymbols = 0.5f;
    }
    public class PanelControllData
    {
        //время вывода панели в PanelAnim
        public static readonly float TimePanelOutput = 1;
        //задержка вываода панели для победы/поражения
        public static readonly float TimePanelSpawnDelay = 2.5f;
        //время затухания панели с информацией об уровне
        public static readonly float TimeLvlInfoFade = 5;
        //кристалы для повторения уровней
        public static readonly long DiamondForRepeatLevel = 5;
    }
}

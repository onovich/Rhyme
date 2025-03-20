namespace TenonKit.Rhyme.L10N {

    public enum L10NLangType {
        ZH_CN = 0,
        ZH_TW = 1,
        EN_US = 2,
        JA = 3,
    }

    public static class L10NLangTypeExtension {
        public static string ToVDFStr(this L10NLangType lang) {
            switch (lang) {
                case L10NLangType.ZH_CN:
                    return "schinese";
                case L10NLangType.ZH_TW:
                    return "tchinese";
                case L10NLangType.EN_US:
                    return "english";
                case L10NLangType.JA:
                    return "japanese";
                default:
                    UnityEngine.Debug.LogError("unknown lang type: " + lang.ToString());
                    return "schinese";
            }
        }
    }

}

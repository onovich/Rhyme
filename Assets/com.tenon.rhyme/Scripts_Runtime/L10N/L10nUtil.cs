using TenonKit.Rhyme;

namespace TenonKit.Rhyme.L10n {

    public static class L10NUtil {
        public static L10NCore l10n;
        public static string GetL10NString_Sentence(int l10nID, short sentenceIndex) {
            return l10n.Dialogue_GetSentence(l10nID, sentenceIndex);
        }
    }

}
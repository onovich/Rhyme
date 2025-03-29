using TenonKit.Rhyme;

namespace TenonKit.Rhyme.L10N {

    public static class L10nUtil {
        public static L10N l10n;
        public static string GetL10nString_Sentence(DialogueEntity dialogue, short sentenceIndex) {
            if (dialogue == null) {
                return default;
            }
            return l10n.Dialogue_GetSentence(dialogue.dialogueL10nID, sentenceIndex);
        }
    }

}
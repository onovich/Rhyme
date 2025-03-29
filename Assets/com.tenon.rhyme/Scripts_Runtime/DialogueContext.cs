using System.Collections.Generic;

namespace TenonKit.Rhyme {

    public class DialogueContext {

        Dictionary<int, DialogueEntity> dialogues = new Dictionary<int, DialogueEntity>();
        Dictionary<(int, short), SentenceModel> sentences = new Dictionary<(int, short), SentenceModel>();
        public DialogueStateEntity state;

        public DialogueContext() {
            dialogues = new Dictionary<int, DialogueEntity>();
            sentences = new Dictionary<(int, short), SentenceModel>();
        }

        public void Dialogue_Add(DialogueEntity sheet) {
            dialogues.Add(sheet.dialogueL10nID, sheet);
        }

        public DialogueEntity Dialogue_Get(int dialogueL10nID) {
            return dialogues[dialogueL10nID];
        }

        public void Sentence_Add(SentenceModel sentence) {
            sentences.Add((sentence.dialogueL10nID, sentence.index), sentence);
        }

        public SentenceModel Sentence_Get(int dialogueL10nID, short index) {
            return sentences[(dialogueL10nID, index)];
        }

    }

}
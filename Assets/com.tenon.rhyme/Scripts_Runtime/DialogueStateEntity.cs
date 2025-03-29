using System;
using TenonKit.Rhyme.L10N;

namespace TenonKit.Rhyme {

    public class DialogueStateEntity {

        // Dialogue
        public DialogueEntity dialogue_current;
        public bool dialogue_isPlaying;

        // Sentence
        public SentenceModel sentence_current;
        public int sentence_currentCharIndex;
        public float sentence_currentTime;

        public DialogueStateEntity() {
            dialogue_isPlaying = false;
        }

        // Dialogue
        public void Dialogue_Enter(DialogueEntity dialogue, SentenceModel sentence) {
            dialogue_current = dialogue;
            dialogue_isPlaying = false;
            sentence_current = sentence;
        }

        public void Dialogue_Exit() {
            dialogue_isPlaying = false;
            dialogue_current = null;
            sentence_current = null;
        }

        // Sentence
        public void Sentence_Refresh(SentenceModel sentence) {
            sentence_current = sentence;
            sentence_currentCharIndex = 0;
            sentence_currentTime = 0;
        }

        public void Sentence_Next(Func<SentenceModel, SentenceModel> getNextSentence) {
            if (sentence_current == null) {
                return;
            }
            if (sentence_current.isEnd) {
                Dialogue_Exit();
                return;
            }
            sentence_current = getNextSentence(sentence_current);
        }

        // Char
        public void Char_Next() {
            sentence_currentCharIndex++;
        }

        public string Char_GetAll() {
            if (sentence_current == null) {
                return default;
            }
            return L10nUtil.GetL10nString_Sentence(dialogue_current, sentence_current.index);
        }

    }

}
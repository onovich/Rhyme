using System;
using TenonKit.Rhyme.L10n;

namespace TenonKit.Rhyme {

    public class DialogueStateEntity {

        // Dialogue
        public bool dialogue_isPlaying;

        // Sentence
        public SentenceModel sentence_current;
        public int sentence_currentCharIndex;
        public float sentence_currentTime;

        public DialogueStateEntity() {
            dialogue_isPlaying = false;
        }

        // Dialogue
        public void Dialogue_Enter(SentenceModel sentence) {
            dialogue_isPlaying = true;
            sentence_current = sentence;
        }

        public void Dialogue_Exit() {
            dialogue_isPlaying = false;
        }

        // Sentence
        public void Sentence_Refresh(SentenceModel sentence) {
            sentence_current = sentence;
            sentence_currentCharIndex = 0;
            sentence_currentTime = 0;
        }

        // Char
        public void Char_Next(Action<int> onCharIndex) {
            if (!dialogue_isPlaying) {
                return;
            }
            if (sentence_currentCharIndex >= Char_GetLenth()) {
                return;
            }
            onCharIndex?.Invoke(sentence_currentCharIndex);
            sentence_currentCharIndex++;
        }

        public void Char_End() {
            if (!dialogue_isPlaying) {
                return;
            }
            sentence_currentCharIndex = Char_GetLenth() - 1;
        }

        public int Char_GetLenth() {
            if (!dialogue_isPlaying) {
                return default;
            }
            return L10NUtil.GetL10NString_Sentence(sentence_current.l10nID, sentence_current.index).Length;
        }

    }

}
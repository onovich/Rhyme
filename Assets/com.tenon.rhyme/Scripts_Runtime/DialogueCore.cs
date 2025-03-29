using TenonKit.Rhyme.L10N;
using UnityEngine;

namespace TenonKit.Rhyme {

    public class DialogueCore {

        DialogueContext ctx;

        public DialogueCore() {
            ctx = new DialogueContext();
        }

        public void Tick(float dt) {
            var state = ctx.state;
            if (!state.dialogue_isPlaying) {
                return;
            }

            var dialogue = state.dialogue_current;

            // Tick Sentence
            var speed = dialogue.playingSpeed;
            state.sentence_currentTime += dt;
            state.sentence_currentTime = Mathf.FloorToInt(state.sentence_currentTime / speed);

            // Tick Char
            if (state.sentence_currentCharIndex >= state.Char_GetAll().Length) {
                state.Sentence_Next((currentSentence) => GetNextSentence(currentSentence));
            } else {
                state.Char_Next();
            }

        }

        SentenceModel GetNextSentence(SentenceModel currentSentence) {
            return ctx.Sentence_Get(currentSentence.dialogueL10nID, currentSentence.nextIndex);
        }

        public void CreateDialogue(DialogueContext ctx, DialogueTM tm) {
            DialogueEntity dialogue = new DialogueEntity();
            dialogue.dialogueL10nID = tm.dialogueL10nID;
            dialogue.playingSpeed = tm.playingSpeed;
        }

    }

}
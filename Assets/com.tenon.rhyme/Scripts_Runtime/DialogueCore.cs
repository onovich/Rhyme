using System;
using System.Threading.Tasks;
using TenonKit.Rhyme.L10n;
using UnityEngine;

namespace TenonKit.Rhyme {

    public class DialogueCore {

        DialogueContext ctx;
        Action<int> onCharIndex;
        Action onDialogueExit;

        public DialogueCore() {
            ctx = new DialogueContext();
            ctx.Init();
            L10NUtil.l10n = ctx.l10n;
        }

        public async Task LoadAll() {
            await ctx.template.LoadAll();
        }

        public void Register(Action<int> onCharIndex, Action onDialogueExit) {
            this.onCharIndex = onCharIndex;
            this.onDialogueExit = onDialogueExit;
        }

        void Unregister() {
            onCharIndex = null;
            onDialogueExit = null;
        }

        public string GetCurrentFullSentence() {
            var state = ctx.state;
            if (!state.dialogue_isPlaying) {
                return string.Empty;
            }
            var sentence = state.sentence_current;
            var l10nID = sentence.l10nID;
            var index = sentence.index;
            string text = ctx.l10n.Dialogue_GetSentence(l10nID, index);
            return text;
        }

        public void TearDown() {
            ctx.template.ReleaseAll();
            Unregister();
        }

        public void RecordInput(bool isSubmit) {
            ctx.input.isSubmit = isSubmit;
        }

        public void SetLanguage(L10NLangType lang) {
            ctx.l10n.SetLang(lang);
        }

        public void Tick(float dt) {
            var state = ctx.state;
            if (!state.dialogue_isPlaying) {
                return;
            }
            var input = ctx.input;

            // Tick Sentence
            var sentence = state.sentence_current;
            bool isTypewriter = sentence.isTypewriter;
            if (!isTypewriter) {
                if (input.isSubmit) {
                    bool succ = TryGetNextSentence(sentence, out SentenceModel nextSentence);
                    if (succ) {
                        state.Sentence_Refresh(nextSentence);
                    } else {
                        Exit();
                    }
                } else {
                    state.Char_End();
                }
                onCharIndex?.Invoke(state.sentence_currentCharIndex);
                return;
            }

            var speed = sentence.playingSpeed;
            state.sentence_currentTime += dt;
            // state.sentence_currentTime = Mathf.FloorToInt(state.sentence_currentTime / speed);

            // Tick Char
            if (state.sentence_currentCharIndex >= state.Char_GetLenth()) {
                if (input.isSubmit) {
                    bool succ = TryGetNextSentence(sentence, out SentenceModel nextSentence);
                    if (succ) {
                        state.Sentence_Refresh(nextSentence);
                    } else {
                        Exit();
                    }
                }
            } else {
                if (state.sentence_currentTime > speed) {
                    state.sentence_currentTime = 0;
                    state.Char_Next(onCharIndex);
                }
            }
            onCharIndex?.Invoke(state.sentence_currentCharIndex);
        }

        public void ResetInput() {
            ctx.input.Reset();
        }

        bool TryGetNextSentence(SentenceModel currentSentence, out SentenceModel nextSentence) {
            if (currentSentence.isEnd) {
                nextSentence = default;
                return false;
            }
            var template = ctx.template;
            bool succ = template.Sentence_TryGet(currentSentence.l10nID, currentSentence.nextIndex, out SentenceTM tm);
            if (!succ) {
                nextSentence = default;
                return false;
            }
            nextSentence = CreateSentence(tm);
            return true;
        }

        SentenceModel CreateSentence(SentenceTM tm) {
            SentenceModel sentence = new SentenceModel();
            sentence.l10nID = tm.l10nID;
            sentence.index = tm.index;
            sentence.nextIndex = tm.nextIndex;
            sentence.isEnd = tm.isEnd;
            sentence.isTypewriter = tm.isTypewriter;
            sentence.playingSpeed = tm.playingSpeed;
            return sentence;
        }

        public void Exit() {
            onDialogueExit?.Invoke();
            var state = ctx.state;
            if (state.dialogue_isPlaying) {
                state.Dialogue_Exit();
            }
            Unregister();
        }

        public void Enter(SentenceTM tm) {
            SentenceModel sentence = CreateSentence(tm);
            ctx.state.Dialogue_Enter(sentence);
        }

    }

}
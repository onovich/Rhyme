using System.Collections.Generic;
using System.Diagnostics;

namespace TenonKit.Rhyme.L10N {

    public class L10NLangEntity {

        public L10NLangType langType;

        Dictionary<int, string> speakerTextDict;
        Dictionary<ulong, string> dialogueTextNewDict;

        public L10NLangEntity() {
            speakerTextDict = new Dictionary<int, string>(1000);
            dialogueTextNewDict = new Dictionary<ulong, string>(1000);
        }

        #region Speader
        public void Speader_Add(int key, string value) {
            bool succ = speakerTextDict.TryAdd(key, value);
            if (!succ) {
                // NJLog.Warning($"SameKey, {key}: {value}");
            }
        }

        public bool Speader_TryGet(int key, out string value) {
            return speakerTextDict.TryGetValue(key, out value);
        }
        #endregion

        #region Dialogue
        public void Dialogue_Add(int dialogueTypeID, short sentenceIndex, sbyte optionIndex, string value) {
            ulong key = Dialogue_Key(dialogueTypeID, sentenceIndex, optionIndex);
            bool succ = dialogueTextNewDict.TryAdd(key, value);
            if (!succ) {
                // NJLog.Warning($"SameKey, {key}: {value}");
            }
        }

        public bool Dialogue_TryGet(int dialogueTypeID, short sentenceIndex, sbyte optionIndex, out string value) {
            ulong key = Dialogue_Key(dialogueTypeID, sentenceIndex, optionIndex);
            return dialogueTextNewDict.TryGetValue(key, out value);
        }

        ulong Dialogue_Key(int dialogueTypeID, short sentenceIndex, sbyte optionIndex) {
            return (ulong)dialogueTypeID << 32 | (ulong)(ushort)sentenceIndex << 8 | (ulong)(byte)optionIndex;
        }
        #endregion

    }

}
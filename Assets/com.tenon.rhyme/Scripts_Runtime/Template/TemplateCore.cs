using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

namespace TenonKit.Rhyme {

    public class TemplateCore {

        AsyncOperationHandle sentenceHandle;
        Dictionary<(int, short), SentenceTM> sentenceDict;
        string sentenceLabel = "TM_Sentence";

        public TemplateCore() {
            sentenceDict = new Dictionary<(int, short), SentenceTM>();
        }

        public async Task LoadAll() {
            await Sentence_Load();
        }

        public void ReleaseAll() {
            Sentence_Release();
        }

        async Task Sentence_Load() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = sentenceLabel;
            var handle = Addressables.LoadAssetsAsync<SentenceSO>(labelReference, null);
            var list = await handle.Task;
            foreach (var so in list) {
                var tm = so.tm;
                bool succ = sentenceDict.TryAdd((tm.l10nID, tm.index), tm);
                if (!succ) {
                    Debug.LogError($"Sentence Load Error: {tm.l10nID}, {tm.index}");
                }
            }
            sentenceHandle = handle;
        }

        void Sentence_Release() {
            if (sentenceHandle.IsValid()) {
                Addressables.Release(sentenceHandle);
            }
        }

        public bool Sentence_TryGet(int l10nID, short index, out SentenceTM tm) {
            return sentenceDict.TryGetValue((l10nID, index), out tm);
        }

    }

}
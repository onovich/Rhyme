using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using NReco.Csv;

namespace TenonKit.Rhyme.L10N {

    public class L10N {

        L10NLangType currentLangType;
        Dictionary<L10NLangType, L10NLangEntity> all;

        public L10N() {
            all = new Dictionary<L10NLangType, L10NLangEntity>();
            currentLangType = L10NLangType.ZH_CN;
        }

        public void SetLang(L10NLangType langType) {
            currentLangType = langType;
        }

        public L10NLangType GetCurrentL10nLangType() {
            return currentLangType;
        }

        public void Init() {
            InitEntities();
            LoadCustomCSV("L10N_Speaker", (entity, id, text) => {
                entity.Speader_Add(id, text);
            });
            LoadDialogueCSV("L10N_DialogueNew");
        }

        void InitEntities() {
            var langTypes = Enum.GetValues(typeof(L10NLangType)); // CN EN JP
            string[] langNames = new string[langTypes.Length]; // CN EN JP
            for (int i = 0; i < langTypes.Length; i += 1) {
                L10NLangType langType = (L10NLangType)langTypes.GetValue(i);
                L10NLangEntity entity = new L10NLangEntity();
                entity.langType = langType;
                langNames[i] = langType.ToString();
                all.Add(langType, entity);
            }
        }

        void LoadCustomCSV(string fileNameWithoutExt, Action<L10NLangEntity, int, string> cb) {

            var langTypes = Enum.GetValues(typeof(L10NLangType)); // CN EN JP
            string[] langNames = new string[langTypes.Length]; // CN EN JP
            for (int i = 0; i < langTypes.Length; i += 1) {
                L10NLangType langType = (L10NLangType)langTypes.GetValue(i);
                langNames[i] = langType.ToString();
            }

            // 加载
            var handle = Addressables.LoadAssetAsync<TextAsset>(fileNameWithoutExt);
            TextAsset csv = handle.WaitForCompletion();
            Stream stream = new MemoryStream(csv.bytes);
            StreamReader sr = new StreamReader(stream);
            CsvReader reader = new CsvReader(sr);

            // 读首行
            reader.Read();
            var dict = new Dictionary<int, L10NLangType>();
            for (int i = 0; i < reader.FieldsCount; i += 1) {
                string key = reader[i];
                for (int j = 0; j < langNames.Length; j += 1) {
                    if (key == langNames[j]) {
                        // 第几列表示哪种语言
                        L10NLangType langType = (L10NLangType)langTypes.GetValue(j);
                        dict.Add(i, langType);
                    }
                }
            }

            // 读每行
            while (reader.Read()) {
                // 读一行:
                // 第0列 ID
                int id = int.Parse(reader[0]);

                // 第1列是描述, 可不读

                // 从第2列开始是文本
                for (int i = 2; i < reader.FieldsCount; i += 1) {
                    bool has = dict.TryGetValue(i, out var langType);
                    if (!has) {
                        Debug.LogWarning($"L10n.Init: langType not found: {i}, {reader[i]}; id: {id}");
                        continue;
                    }
                    has = all.TryGetValue(langType, out var entity);
                    if (!has) {
                        Debug.LogWarning($"L10n.Init: langType not found: {langType.ToString()}, {reader[i]}; id: {id}");
                        continue;
                    }
                    string text = reader[i];
                    if (string.IsNullOrEmpty(text)) {
                        continue;
                    }

                    // 用 ` 替换逗号
                    text = text.Replace("`", ",");

                    // 用 % 替换引号
                    text = text.Replace("%", "\"");

                    // 用\r\n替换/br
                    text = text.Replace("/br", "\r\n");

                    // 转义符替换

                    cb.Invoke(entity, id, text);
                }
            }

            // 释放
            if (handle.IsValid()) {
                Addressables.Release(handle);
            }
            sr.Dispose();
            stream.Dispose();

        }

        void LoadDialogueCSV(string fileNameWithoutExt) {

            var langTypes = Enum.GetValues(typeof(L10NLangType)); // CN EN JP
            string[] langNames = new string[langTypes.Length]; // CN EN JP
            for (int i = 0; i < langTypes.Length; i += 1) {
                L10NLangType langType = (L10NLangType)langTypes.GetValue(i);
                langNames[i] = langType.ToString();
            }

            // 加载
            var handle = Addressables.LoadAssetAsync<TextAsset>(fileNameWithoutExt);
            TextAsset csv = handle.WaitForCompletion();
            Stream stream = new MemoryStream(csv.bytes);
            StreamReader sr = new StreamReader(stream);
            CsvReader reader = new CsvReader(sr);

            // 读首行
            reader.Read();
            var dict = new Dictionary<int, L10NLangType>();
            for (int i = 0; i < reader.FieldsCount; i += 1) {
                string key = reader[i];
                for (int j = 0; j < langNames.Length; j += 1) {
                    if (key == langNames[j]) {
                        // 第几列表示哪种语言
                        L10NLangType langType = (L10NLangType)langTypes.GetValue(j);
                        dict.Add(i, langType);
                    }
                }
            }

            // 读每行
            while (reader.Read()) {
                // 读一行:
                // 第0列 TypeID
                int typeID = int.Parse(reader[0]);

                // 第1列 SentenceIndex
                short sentenceIndex = short.Parse(reader[1]);

                // 第2列 OptionIndex
                sbyte optionIndex = sbyte.Parse(reader[2]);

                // 第1列是描述, 可不读

                // 从第2列开始是文本
                for (int i = 3; i < reader.FieldsCount; i += 1) {
                    bool has = dict.TryGetValue(i, out var langType);
                    if (!has) {
                        Debug.LogWarning($"L10n.Init: langType not found: {i}, {reader[i]}; id: {typeID}");
                        continue;
                    }
                    has = all.TryGetValue(langType, out var entity);
                    if (!has) {
                        Debug.LogWarning($"L10n.Init: langType not found: {langType.ToString()}, {reader[i]}; id: {typeID}");
                        continue;
                    }
                    string text = reader[i];
                    if (string.IsNullOrEmpty(text)) {
                        continue;
                    }

                    // 用 ` 替换逗号
                    text = text.Replace("`", ",");

                    // 用 % 替换引号
                    text = text.Replace("%", "\"");

                    // 用\r\n替换/br
                    text = text.Replace("/br", "\r\n");

                    // 用</b></color> 替换</NJM>
                    text = text.Replace("</NJM>", "</b></color>");

                    // 转义符替换

                    entity.Dialogue_Add(typeID, sentenceIndex, optionIndex, text);
                }
            }

            // 释放
            if (handle.IsValid()) {
                Addressables.Release(handle);
            }
            sr.Dispose();
            stream.Dispose();

        }

        L10NLangEntity GetFallbackEntity(L10NLangType l10NLangType) {
            bool has = all.TryGetValue(l10NLangType, out var entity);
            if (!has) {
                has = all.TryGetValue(L10NLangType.EN_US, out entity);
            }
            if (!has) {
                has = all.TryGetValue(L10NLangType.ZH_CN, out entity);
            }
            if (!has) {
                Debug.LogError($"L10n.GetFallbackEntity: langType not found: {l10NLangType}");
            }
            return entity;
        }

        #region Speaker
        public string Speaker_Get(int key) {
            var entity = GetFallbackEntity(currentLangType);
            bool has = entity.Speader_TryGet(key, out var str);
            if (has) {
                return str;
            } else {
                has = all.TryGetValue(L10NLangType.EN_US, out var enEntity);
                if (has) {
                    enEntity.Speader_TryGet(key, out str);
                    return str;
                }
            }
            return string.Empty;
        }
        #endregion

        #region Enum: LangDesc
        public string GetLangDesc(L10NLangType langType) {
            if (all.TryGetValue(langType, out L10NLangEntity entity)) {
                int key = (int)langType + 2000;
                entity.Speader_TryGet(key, out var str);
                return str;
            } else {
                Debug.LogError($"L10n.GetLangDesc: langType not found: {langType}");
                return null;
            }
        }
        #endregion

        #region Dialogue
        public string Dialogue_GetSentence(int dialogueTypeID, short sentenceIndex) {
            return Dialogue_GetOption(dialogueTypeID, sentenceIndex, 0);
        }

        public string Dialogue_GetOption(int dialogueTypeID, short sentenceIndex, sbyte optionIndex) {
            var entity = GetFallbackEntity(currentLangType);
            bool has = entity.Dialogue_TryGet(dialogueTypeID, sentenceIndex, optionIndex, out var str);
            if (has) {
                return str;
            } else {
                has = all.TryGetValue(L10NLangType.EN_US, out var enEntity);
                if (has) {
                    enEntity.Dialogue_TryGet(dialogueTypeID, sentenceIndex, optionIndex, out str);
                    return str;
                }
            }
            return string.Empty;
        }
        #endregion

    }

}
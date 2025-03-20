using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEditor;
using NReco.Csv;

namespace TenonKit.Rhyme.MenuTool.Editor {

    public static class CommonMenuTool {

        #region CombineL10N
        [MenuItem("TenonKit/Rhyme/Combine L10N")]
        public static void CombineL10N() {
            const string file_speaker = "L10N_Speaker.csv";
            const string file_dialogue = "L10N_Dialogue.csv";

            string dir = Path.Combine(Application.dataPath, "com.tenon.rhyme", "Resources_Runtime", "Localization");

            byte[] bytes_speaker = File.ReadAllBytes(Path.Combine(dir, file_speaker));
            byte[] bytes_dialogue = File.ReadAllBytes(Path.Combine(dir, file_dialogue));

            string outPath = Path.Combine(dir, "L10N_Combined.csv");
            File.WriteAllBytes(outPath, CombineBytes(bytes_speaker, bytes_dialogue));
        }

        static byte[] CombineBytes(params byte[][] bytes) {
            int len = 0;
            foreach (var b in bytes) {
                len += b.Length + 10;
            }
            List<byte> list = new List<byte>(len);
            foreach (var b in bytes) {
                list.AddRange(b);
                list.Add((byte)'\n');
            }
            return list.ToArray();
        }
        #endregion

    }
}
using System;
using System.Threading.Tasks;
using TenonKit.Rhyme.L10n;
using TMPro;
using UnityEngine;

namespace TenonKit.Rhyme {

    public class SamplePanel : MonoBehaviour {

        [SerializeField] TextMeshProUGUI text;
        char[] charBuffer;

        public void Init() {
            text.text = "";
            charBuffer = new char[256];
        }

        void PreSetTxt(string str) {
            if (string.IsNullOrEmpty(str)) {
                Debug.LogWarning("str is null or empty");
                return;
            }
            if (str.Length > charBuffer.Length) {
                charBuffer = new char[str.Length];
                return;
            }
            str.CopyTo(0, charBuffer, 0, str.Length);
        }

        public void ShowTxt(string str, int index) {
            PreSetTxt(str);
            text.SetCharArray(charBuffer, 0, index);
        }

        public void Close() {
            Destroy(gameObject);
        }

        bool IsTextOverflowing() {
            return text.isTextOverflowing;
            // return textComponent.textInfo.lineCount > textComponent.maxVisibleLines;
        }

    }

}
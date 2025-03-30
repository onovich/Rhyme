using System;
using System.Threading.Tasks;
using TenonKit.Rhyme.L10n;
using UnityEngine;

namespace TenonKit.Rhyme {

    public class SampleMain : MonoBehaviour {

        [SerializeField] SentenceSO sentence_start;
        [SerializeField] SamplePanel panel;
        DialogueCore dialogueCore;

        void Start() {
            dialogueCore = new DialogueCore();
            panel.Init();
            Action action = async () => {
                try {
                    await dialogueCore.LoadAll();
                    dialogueCore.SetLanguage(L10NLangType.ZH_CN);
                    dialogueCore.Register(OnCharRefresh, OnDialogueExit);
                    dialogueCore.Enter(sentence_start.tm);
                } catch (Exception e) {
                    Debug.LogError(e);
                }
            };
            action.Invoke();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                dialogueCore.RecordInput(true);
            } else {
                dialogueCore.RecordInput(false);
            }
            dialogueCore.Tick(Time.deltaTime);
        }

        void LateUpdate() {
            dialogueCore.ResetInput();
        }

        void OnCharRefresh(int charIndex) {
            string fullString = dialogueCore.GetCurrentFullSentence();
            panel.ShowTxt(fullString, charIndex);
        }

        void OnDialogueExit() {
            ClosePanel();
        }

        void ClosePanel() {
            panel.Close();
            panel = null;
        }

    }

}
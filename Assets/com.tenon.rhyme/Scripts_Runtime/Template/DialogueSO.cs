using System;
using UnityEngine;

namespace TenonKit.Rhyme {

    [CreateAssetMenu(fileName = "SO_Dialogue_", menuName = "Rhyme/Dialogue")]
    public class DialogueSO : ScriptableObject {

        public int l10nID;
        public float playingSpeed;

    }

}
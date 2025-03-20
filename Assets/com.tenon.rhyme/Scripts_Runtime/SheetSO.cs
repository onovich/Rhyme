using System;
using UnityEngine;

namespace TenonKit.Rhyme {

    [CreateAssetMenu(fileName = "SO_Sheet_", menuName = "Rhyme/Sheet")]
    public class SheetSO : ScriptableObject {

        public int l10nID;
        public float playingSpeed;

    }

}
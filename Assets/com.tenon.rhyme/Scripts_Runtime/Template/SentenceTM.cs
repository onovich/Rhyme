using System;

namespace TenonKit.Rhyme {

    [Serializable]
    public struct SentenceTM {

        public int l10nID;
        public short index;
        public short nextIndex;
        public bool isEnd;
        public bool isTypewriter;
        public float playingSpeed;

    }

}
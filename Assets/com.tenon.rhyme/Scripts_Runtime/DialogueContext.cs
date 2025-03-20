namespace TenonKit.Rhyme {

    public class DialogueContext {

        Speaker player;
        public Speaker Player => player;

        public DialogueContext() {
            player = new Speaker();
        }

    }

}
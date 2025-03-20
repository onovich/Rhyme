namespace TenonKit.Rhyme {

    public class DialogueCore {

        DialogueContext ctx;

        public DialogueCore() {
            ctx = new DialogueContext();
        }

        // public void SetContent(string content, float interval = 0) {
        //     var sheet = new Sheet {
        //         content = content,
        //         interval = interval
        //     };
        //     ctx.Player.SetSheet(sheet);
        //     ctx.Player.EnterPlaying();
        // }

        // public char GetCurrentContent() {
        //     // return ctx.Player.GetCurrentContent();
        // }

        public void Tick(float dt) {
            ctx.Player.ApplyPlay(dt);
        }

    }

}
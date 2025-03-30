using System.Collections.Generic;
using System.Threading.Tasks;
using TenonKit.Rhyme.L10n;

namespace TenonKit.Rhyme {

    public class DialogueContext {

        public DialogueStateEntity state;

        public TemplateCore template;
        public InputEntity input;
        public L10NCore l10n;

        public DialogueContext() {
            state = new DialogueStateEntity();
            template = new TemplateCore();
            input = new InputEntity();
            l10n = new L10NCore();
        }

        public void Init() {
            l10n.Init();
        }

    }

}
using System;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Mushakushi.DialogueWriter.Runtime.External.YarnSpinner
{
    public class YarnOptionHandlerBase: MonoBehaviour
    {
        [SerializeField] private YarnObserver yarnObserver;

        private void OnEnable()
        {
            yarnObserver.onOptionSetParsed.Event += HandleOptions;
        }
        
        private void OnDisable()
        {
            yarnObserver.onOptionSetParsed.Event -= HandleOptions;
        }

        private void HandleOptions(YarnParser parser, OptionSet options)
        {
            var optionSet = new DialogueOption[options.Options.Length];
            for (var i = 0; i < options.Options.Length; i++)
            {
                var line = parser.LineProviderBehaviour.GetLocalizedLine(options.Options[i].Line);
                var text = Dialogue.ExpandSubstitutions(line.RawText, options.Options[i].Line.Substitutions);
                parser.Dialogue.LanguageCode = parser.LineProviderBehaviour.LocaleCode;
                line.Text = parser.Dialogue.ParseMarkup(text);

                optionSet[i] = new DialogueOption
                {
                    TextID = options.Options[i].Line.ID,
                    DialogueOptionID = options.Options[i].ID,
                    Line = line,
                    IsAvailable = options.Options[i].IsAvailable,
                };
            }
            OptionsNeedPresentation?.Invoke(optionSet);
        }
    }
}
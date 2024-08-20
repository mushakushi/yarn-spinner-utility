using Mushakushi.YarnSpinnerUtility.Runtime;
using Mushakushi.YarnSpinnerUtility.Runtime.Output;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Mushakushi.YarnSpinnerUtility.Samples.Demo
{
    [System.Serializable]
    public class DialogueOptionFactoryTMPro: IDialogueOptionFactory
    {
        [SerializeField] private DialogueObserver dialogueObserver;
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject dialogueOptionParent;

        public GameObject Create(DialogueOption dialogueOption)
        {
            // this code makes a lot of assumptions about the components in the prefab, which is generally something you shouldn't do.
            var instance = Object.Instantiate(prefab, dialogueOptionParent.transform, false);
            instance.GetComponent<Button>().onClick.AddListener(() =>
            { 
                dialogueObserver.optionSelected.RaiseEvent(dialogueOption.DialogueOptionID);
            });
            instance.GetComponentInChildren<TMP_Text>().text = dialogueOption.Line.Text.Text;
            return instance;
        }

        public void Dispose(GameObject dialogueOption)
        {
            Object.Destroy(dialogueOption);
        }
    }
}
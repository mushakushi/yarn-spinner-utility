# Yarn Spinner

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Utilities for [Yarn Spinner](https://docs.yarnspinner.dev/). The motivation for creating this is to be a replacement 
for the default dialogue output that comes with the Unity integration, which is, arguably, too opinionated
and tightly coupled.

## ⚙ Installation 

### With UPM

from the Add package from git URL option, enter:

```bash
https://github.com/mushakushi/yarn-spinner-utility.git?path=Assets/Mushakushi.YarnSpinnerUtility
```

If you are specifying a version, append #{VERSION} to the end of the git URL. 

```bash
https://github.com/mushakushi/yarn-spinner-utility.git?path=Assets/Mushakushi.YarnSpinnerUtility#{VERSION}
```
### Git Dependencies
* https://github.com/ayellowpaper/SerializedDictionary.git

## 🚀 Usage

### Dialogue Parser
A modified version of Yarn's [Minimal Dialogue Runner](https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/main/Samples~/Minimal%20Viable%20Dialogue%20System/Scripts/MinimalDialogueRunner.cs) that allows further separation of concerns
using the Scriptable Object observer pattern and makes no assumptions about how the game will be run.
Because of this, it can work across scenes using a `DialogueObserver`.

### View Controllers
For the recommended setup for displaying dialogue lines and options, see the `OptionViewController` and 
`LineViewController` classes, respectively.

### Views
Modify its `views` in the inspector, which is recalculated on each dialogue line and removed when the dialogue is completed.

You can create a new view by marking
any class that inherits from `IView` with the `[System.Serializable]` attribute.

```csharp
[System.Serializable] public class View: IView
```

### Command Handling
You can handle commands as you'd usually do by using `YarnCommandDispatcher.AddCommandHandler`.
The dialogue is must be continued manually after the command is handled.

```csharp
public class ExampleCommandHandler: MonoBehaviour
{
    [SerializeField] private YarnCommandDispatcher commandDispatcher;

    private void Start()
    {
        // Please note that the "wait" command is the only command
        // that is handled for you. 
        commandDispatcher.AddCommandHandler<float>("wait", HandleWaitCommand);
    }

    private void HandleWaitCommand(float waitDuration)
    {
        // handle the command ...
        // the continue the dialogue (note: this can be done asynchronously)
        commandDispatcher.dialogueParser.TryContinue();
    }
}
```

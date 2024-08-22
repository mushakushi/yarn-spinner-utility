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

### Dialogue Observer
Uses a `RaisableEvent` custom class which you can subscribe to each event using `Callback.Event`
and raise the event using `Callback.RaiseEvent(params)`. You can raise and handle these events however you'd like, and 
some are automatically called by the `DialogueParser`.

### Lines & Options Output
For the recommended setup for displaying dialogue lines and options, see the `OptionOutputController` and 
`LineOutputController` classes, respectively.

### Line Output Controller
Modify its `layoutElements` in the inspector, which is recalculated on each dialogue line and removed when the dialogue is completed.
Each layout `ILayoutElementAsync` callback is asynchronously and sequentially executed as they appear in the collection. 

You can create a new layout element by marking
any class that inherits from `ILayoutElementAsync` with the `[System.Serializable]` attribute.

```csharp
[System.Serializable] public class LayoutElementAsync: ILayoutElementAsync
```

### Command Handling
You can handle commands as you'd usually do by using `YarnCommandController.AddCommandHandler`.

```csharp
public class ExampleCommandHandler: MonoBehaviour
{
    [SerializeField] private DialogueObserver dialogueObserver;
    [SerializeField] private YarnCommandController commandController;

    private void Start()
    {
        commandController.AddCommandHandler<float>("wait", HandleWaitCommand);
    }

    private void HandleWaitCommand(float waitDuration)
    {
        // handle the command (note: this can be asynchronous) 
        // then let the dialogue know that the command was handled.
        dialogueObserver.commandHandled.RaiseEvent();
    }
}
```

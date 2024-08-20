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
### Dependencies
* https://openupm.com/packages/com.mackysoft.serializereference-extensions/

## 🚀 Usage

### Dialogue Parser
A modified version of Yarn's [Minimal Dialogue Runner](https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/main/Samples~/Minimal%20Viable%20Dialogue%20System/Scripts/MinimalDialogueRunner.cs) that allows further separation of concerns
using the Scriptable Object observer pattern and makes no assumptions about how the game will be run.
Because of this, it can work across scenes using a `DialogueObserver`.

### Dialogue Observer
Uses a `RaisableEvent` custom class which you can subscribe to each event using `Callback.Event`
and raise the event using `Callback.RaiseEvent(params)`. You can raise and handle these events however you'd like, and 
some are automatically called by the `DialogueParser`.

For the recommended setup for displaying dialogue lines and options, see the `OptionOutputController` and 
`LineOutputController` classes, respectively.


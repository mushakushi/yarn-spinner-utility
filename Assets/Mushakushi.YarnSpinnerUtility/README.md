# Yarn Spinner
Utilities for [Yarn Spinner](https://docs.yarnspinner.dev/). 

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Minimal Dialogue Runner 
A modified version of [Yarn's Minimal Dialogue Runner](https://github.com/YarnSpinnerTool/YarnSpinner-Unity/blob/main/Samples~/Minimal%20Viable%20Dialogue%20System/Scripts/MinimalDialogueRunner.cs) that allows further separation of concerns 
using the Scriptable Object observer pattern and makes no assumptions about how the game will be run. 
Because of this, it can work across scenes using the YarnDialogueObserver. 

## Yarn Observer
Uses a `RaisableEvent` custom class which you can subscribe to each event using `Callback.Event` 
and raise the event using `Callback.RaiseEvent(params)`.

## Yarn Utility

### SetAllVariables
```csharp
// set all variables directly without needing to populate three dictionaries
variableStorageBehaviour.SetAllVariables(yarnProject.InitialValues);
```

### EvaluateWaitCommandAsSeconds
```csharp
var commandElements = Yarn.Unity.DialogueRunner.SplitCommandText(command.Text).ToArray();
var secondsToWait = YarnUtility.GetWaitCommandDuration(commandElements); 
```


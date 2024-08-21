# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.1] - 2024-08-20

### Added 
- `CommandDispatcher` and `YarnCommandController` for to improve the command handling experience.

### Changed
- Made modifications to the Demo scene to showcase new functionality. 
- `DialogueObserver.CommandParsed` accepts a `Comnand` object rather than a string array.

### Removed 

### Fixed
- Ability to start a node that doesn't exist and continue dialogue when an option was no selected.

## [1.1.0] - 2024-08-19

### Added 
- Sample "Demo" scene
- Serialization and Deserialization from Yarn's Dialogue Runner.
- Line an dialogue option output. 

### Changed
- Moved all context menus from `"ScriptableObjects/YarnUtility"` to `"Yarn Spinner/Utility"`
- Renamed `MinimalDialogueRunner` to `YarnParser`
- Renamed `YarnDialogueObserver` to `DialogueObserver`
- `RaisableEvent` increased to support 17 generic parameters

### Removed 
### Fixed

## [1.0.0] - 2024-08-09

### Added
- Initial Commit

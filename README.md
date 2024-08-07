# Quack Attack

This is a Unity-based Duck Shooting Game where players aim to shoot moving targets (ducks) for points. The game includes various features like a start screen, scoring system, audio effects, and dynamic UI elements.

## Features

### Gameplay
- **Duck Movement**: Ducks have three movements: to the left, right, or up and down.
- **Target Interaction**: Ducks flash red when hit and their color is based on the points they give when destroyed.
- **Scoring System**: The player needs to hit some targets multiple times to score points. The final score and highest score are displayed at the end of the game.
- **Camera Control**: The camera returns to its initial position when the game restarts and it does not move during the countdown.

### User Interface
- **Start Screen**: Includes buttons for starting the game, exiting, and accessing instructions.
- **Instruction Panel**: Provides information on how to play, the points system, and the number of hits to destroy each duck.
- **HUD**: Displays the player’s current score, highest score, and a countdown timer.
- **End Game Screen**: Shows the final score and options to replay or exit.

## Technical Details
### Scripts
- **PlayerController**:
  - Use the Unity Input System to handle player input and control camera movement.
  - Uses a coroutine to smoothly transition the camera to a specified look-at position when the game ends or restarts.
- **TargetController**:
  - Uses an object pool to manage the spawning and destruction of targets, ensuring efficient resource usage.
  - Manages the behavior of targets, including movement, collisions, and scoring.
- **GameManager**:
  - Manages game events such as game start, game end, game restart, game countdown, and game pause through actions that other scripts can subscribe to.
  - Uses PlayableDirector components to control game and target sequences (e.g., cutscenes, animations) by playing, stopping, pausing, and resetting them.
- **AudioManager**:
  - Controls all audio-related functions, including playing sound effects.

## Video
[![Quack Attack Gameplay](https://github.com/user-attachments/assets/0c728e30-fa47-4ae8-8285-46f30ac5fabd)](https://youtu.be/4o0snNpCsCo)

## License
This project is licensed under the MIT License.

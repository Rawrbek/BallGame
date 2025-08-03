# Unity Game API Documentation

## Table of Contents
1. [Overview](#overview)
2. [Core Game Systems](#core-game-systems)
3. [Player Control](#player-control)
4. [Game Mechanics](#game-mechanics)
5. [UI and Scene Management](#ui-and-scene-management)
6. [Utility Functions](#utility-functions)
7. [Setup and Configuration](#setup-and-configuration)

## Overview

This Unity project is a 3D platformer game featuring a ball-based player character that can collect coins, jump on platforms, and navigate through levels. The game includes moving platforms, bounce pads, collectible systems, and camera controls.

**Unity Version**: Compatible with Unity 2018.x and later
**Target Platforms**: PC, Mac, Linux

---

## Core Game Systems

### GameControl Class

The central game management system that handles scoring, win conditions, and scene management.

#### Public Properties

| Property | Type | Description |
|----------|------|-------------|
| `currentScore` | `static int` | Current number of coins collected by the player |
| `canWin` | `static bool` | Whether the player can win (all collectibles collected) |
| `collectibleCount` | `static int` | Number of collectibles remaining in the scene |
| `player` | `Transform` | Reference to the player's transform |
| `scoreText` | `Text` | UI Text component for displaying score |

#### Public Methods

*Note: This class uses Unity's built-in lifecycle methods and doesn't expose public methods for external use.*

#### Usage Example

```csharp
// Check if player can win
if (GameControl.canWin)
{
    // Enable win portal or trigger win condition
}

// Access current score
int playerScore = GameControl.currentScore;

// Check remaining collectibles
int remaining = GameControl.collectibleCount;
```

#### Key Features

- **Automatic Score Tracking**: Monitors collectible collection automatically
- **Scene Reset**: Press 'R' key to reload current scene
- **Fall Detection**: Automatically reloads scene if player falls below environment
- **Win Condition Management**: Tracks when all collectibles are collected

---

## Player Control

### PlayerController Class

Handles all player movement, jumping mechanics, and physics interactions.

#### Public Properties

| Property | Type | Description |
|----------|------|-------------|
| `jumpHeight` | `int` | Force applied when jumping (default recommended: 500-1000) |
| `mask` | `LayerMask` | Layer mask for ground detection |
| `gravity` | `float` | Additional gravity multiplier (default recommended: 20-50) |
| `speed` | `float` | Movement speed (default: 6) |

#### Key Features

- **Ground Detection**: Uses raycast to detect when player is grounded
- **Double Jump System**: Hold space while airborne to charge a directional boost
- **Camera-Relative Movement**: Movement is relative to camera orientation
- **Moving Platform Support**: Player moves with platforms tagged "MovingPlatform"
- **Air Control**: Limited movement control while airborne

#### Usage Example

```csharp
// Setup PlayerController on a GameObject with Rigidbody
var playerController = gameObject.AddComponent<PlayerController>();
playerController.jumpHeight = 750;
playerController.speed = 8f;
playerController.gravity = 30f;
playerController.mask = LayerMask.GetMask("Ground");
```

#### Controls

| Input | Action |
|-------|--------|
| WASD / Arrow Keys | Move player |
| Space (tap) | Jump |
| Space (hold in air) | Charge double jump |
| Space (release) | Execute double jump |
| Right Mouse Button | Camera orbit |

#### Setup Requirements

1. Player GameObject must have a `Rigidbody` component
2. Ground objects should be on a layer included in the `mask` LayerMask
3. Moving platforms should be tagged "MovingPlatform"
4. Scene must have a Camera tagged "MainCamera"

---

## Game Mechanics

### Collectible Class

Manages collectible items (coins) that rotate and can be collected by the player.

#### Public Properties

*No public properties - all configuration is internal*

#### Key Features

- **Automatic Rotation**: Rotates at 100 degrees/second on Z-axis
- **Collision Detection**: Uses trigger collision to detect player contact
- **Score Integration**: Automatically updates GameControl score and count
- **Self-Destruction**: Destroys itself when collected

#### Setup Requirements

1. GameObject must have a `Collider` with `isTrigger` enabled
2. GameObject must be tagged "Collectible"
3. Player must be tagged "Player"

#### Usage Example

```csharp
// Add to a GameObject to make it collectible
var collectible = coinObject.AddComponent<Collectible>();
coinObject.tag = "Collectible";
coinObject.GetComponent<Collider>().isTrigger = true;
```

---

### PlatformMoverSimple Class

Creates moving platforms that oscillate between two points on a specified axis.

#### Public Properties

| Property | Type | Description |
|----------|------|-------------|
| `speed` | `int` | Movement speed of the platform |
| `moveDistance` | `int` | Maximum distance from center point |
| `Axis` | `MoveAxis` | Axis of movement (X, Y, or Z) |
| `direction` | `DirectionToTake` | Initial direction (Plus or Minus) |

#### Enumerations

```csharp
public enum MoveAxis { X, Y, Z }
public enum DirectionToTake { Plus, Minus }
```

#### Key Features

- **Smooth Movement**: Uses Vector3.Lerp for smooth oscillation
- **Configurable Axis**: Can move on any single axis
- **Automatic Reversal**: Automatically reverses direction at endpoints
- **Player Integration**: Works with PlayerController for platform riding

#### Usage Example

```csharp
// Create a horizontally moving platform
var platform = platformObject.AddComponent<PlatformMoverSimple>();
platform.speed = 2;
platform.moveDistance = 10;
platform.Axis = PlatformMoverSimple.MoveAxis.X;
platform.direction = PlatformMoverSimple.DirectionToTake.Plus;

// Tag for player interaction
platformObject.tag = "MovingPlatform";
```

#### Setup Requirements

1. Platform GameObject should have a `Rigidbody` component
2. Platform should be tagged "MovingPlatform" for player interaction
3. Ensure platform has appropriate colliders for player collision

---

### Bouncer Class

Creates bounce pads that launch the player in a specific direction.

#### Public Properties

*No public properties - force is set internally to 13000*

#### Key Features

- **Directional Launch**: Calculates launch direction from collision point
- **Velocity Reset**: Resets player velocity before applying bounce force
- **Horizontal Focus**: Removes Y component from bounce direction

#### Usage Example

```csharp
// Add bouncer component to a platform
var bouncer = bounceObject.AddComponent<Bouncer>();
bounceObject.tag = "Bouncer"; // Optional for identification
```

#### Setup Requirements

1. GameObject must have a `Collider` (not trigger)
2. Player must be tagged "Player"
3. Player must have a `Rigidbody` component

---

### GoalScript Class

Manages the goal/finish area with particle effects when the player can win.

#### Public Properties

| Property | Type | Description |
|----------|------|-------------|
| `GoalParticle` | `GameObject` | Particle system prefab to instantiate |

#### Key Features

- **Conditional Activation**: Only activates when `GameControl.canWin` is true
- **Particle Management**: Instantiates and positions particle effects
- **Single Activation**: Ensures particles are only created once

#### Usage Example

```csharp
// Setup goal area
var goal = goalObject.AddComponent<GoalScript>();
goal.GoalParticle = particleSystemPrefab;
```

---

## UI and Scene Management

### CameraFollowObject Class

Provides third-person camera controls that follow and orbit around a target.

#### Public Properties

| Property | Type | Description |
|----------|------|-------------|
| `target` | `Transform` | Target object to follow |
| `MouseSensitivity` | `float` | Mouse sensitivity for camera rotation (default: 4) |
| `OrbitDampening` | `float` | Smoothing for camera rotation (default: 10) |
| `ScrollDampening` | `float` | Smoothing for zoom (default: 6) |

#### Key Features

- **Position Following**: Camera position matches target position
- **Mouse Orbit**: Right-click and drag to orbit camera
- **Vertical Clamping**: Limits vertical rotation between -30° and 100°
- **Smooth Rotation**: Uses damping for smooth camera movement

#### Usage Example

```csharp
// Setup camera to follow player
var cameraFollow = Camera.main.gameObject.AddComponent<CameraFollowObject>();
cameraFollow.target = playerTransform;
cameraFollow.MouseSensitivity = 5f;
cameraFollow.OrbitDampening = 8f;
```

---

### LoadSceneOnClick Class

Provides functionality to load scenes by index, typically used with UI buttons.

#### Public Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `LoadByIndex` | `int sceneindex` | Loads scene by build index |

#### Usage Example

```csharp
// Attach to a UI Button's OnClick event
Button menuButton = GetComponent<Button>();
LoadSceneOnClick sceneLoader = menuButton.gameObject.AddComponent<LoadSceneOnClick>();

// In Inspector or code:
// Button OnClick() -> sceneLoader.LoadByIndex(1)
```

---

### QuitOnClick Class

Provides application quit functionality with editor support.

#### Public Methods

| Method | Parameters | Description |
|--------|------------|-------------|
| `Quit` | None | Quits application (or stops play mode in editor) |

#### Key Features

- **Editor Support**: Stops play mode when in Unity Editor
- **Build Support**: Calls Application.Quit() in builds

#### Usage Example

```csharp
// Attach to a UI Button for quit functionality
Button quitButton = GetComponent<Button>();
QuitOnClick quitHandler = quitButton.gameObject.AddComponent<QuitOnClick>();

// In Inspector: Button OnClick() -> quitHandler.Quit()
```

---

### GuiTextScript Class

Basic UI text management component (currently minimal implementation).

#### Public Properties

| Property | Type | Description |
|----------|------|-------------|
| `winText` | `Text` | Reference to UI Text component for win messages |

*Note: This class is currently a placeholder with minimal functionality.*

---

## Utility Functions

### GameUtilities Class

Static utility class providing common Vector3 manipulation functions.

#### Public Methods

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `ChangeX` | `Vector3 v, float x` | `Vector3` | Returns new Vector3 with modified X component |
| `ChangeY` | `Vector3 v, float y` | `Vector3` | Returns new Vector3 with modified Y component |
| `ChangeZ` | `Vector3 v, float z` | `Vector3` | Returns new Vector3 with modified Z component |

#### Usage Examples

```csharp
Vector3 position = transform.position;

// Change only the X coordinate
Vector3 newPosition = GameUtilities.ChangeX(position, 10f);

// Change only the Y coordinate
Vector3 raisedPosition = GameUtilities.ChangeY(position, position.y + 5f);

// Change only the Z coordinate
Vector3 forwardPosition = GameUtilities.ChangeZ(position, 0f);

// Chain operations
Vector3 modifiedPosition = GameUtilities.ChangeY(
    GameUtilities.ChangeX(position, 0f), 
    10f
);
```

---

## Setup and Configuration

### Required Tags

Ensure the following tags are created in your project:

- `"Player"` - For the player GameObject
- `"Collectible"` - For collectible items
- `"MovingPlatform"` - For moving platforms
- `"Environment"` - For environment objects (used in fall detection)

### Required Layers

Create and configure these layers:

- Ground layer (for player ground detection)
- Any additional layers as needed for specific collision detection

### Scene Setup Checklist

1. **Player Setup**:
   - GameObject with PlayerController, Rigidbody, and Collider
   - Tagged as "Player"
   - Appropriate mass and drag settings on Rigidbody

2. **Camera Setup**:
   - Main Camera with CameraFollowObject component
   - Target set to player Transform

3. **Game Controller**:
   - Empty GameObject with GameControl component
   - Player Transform reference assigned
   - UI Text component for score display

4. **Level Geometry**:
   - Ground objects on appropriate layer
   - Environment objects tagged "Environment"

5. **Collectibles**:
   - GameObjects with Collectible component
   - Trigger colliders
   - Tagged as "Collectible"

6. **Moving Platforms** (optional):
   - GameObjects with PlatformMoverSimple and Rigidbody
   - Tagged as "MovingPlatform"

7. **Bounce Pads** (optional):
   - GameObjects with Bouncer component
   - Non-trigger colliders

### Performance Considerations

- Use object pooling for collectibles in large levels
- Limit the number of moving platforms for better performance
- Consider using LOD (Level of Detail) for complex geometry
- Optimize particle systems for goal effects

### Debugging Features

- Ground detection rays are visualized in Scene view (green when grounded, red when airborne)
- Console logs show minimum Y position of environment objects
- Use Unity's built-in profiler to monitor performance

---

## Integration Examples

### Complete Player Setup

```csharp
// Create player GameObject
GameObject player = new GameObject("Player");
player.tag = "Player";

// Add required components
Rigidbody rb = player.AddComponent<Rigidbody>();
SphereCollider collider = player.AddComponent<SphereCollider>();
PlayerController controller = player.AddComponent<PlayerController>();

// Configure Rigidbody
rb.mass = 1f;
rb.drag = 0.5f;
rb.angularDrag = 0.05f;

// Configure PlayerController
controller.jumpHeight = 750;
controller.speed = 6f;
controller.gravity = 30f;
controller.mask = LayerMask.GetMask("Ground");
```

### Complete Level Setup

```csharp
// Create game controller
GameObject gameController = new GameObject("GameController");
GameControl gameControl = gameController.AddComponent<GameControl>();

// Setup UI
Canvas canvas = FindObjectOfType<Canvas>();
Text scoreText = new GameObject("ScoreText").AddComponent<Text>();
scoreText.transform.SetParent(canvas.transform);
gameControl.scoreText = scoreText;
gameControl.player = player.transform;

// Create collectibles
for (int i = 0; i < 10; i++)
{
    GameObject coin = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    coin.tag = "Collectible";
    coin.GetComponent<Collider>().isTrigger = true;
    coin.AddComponent<Collectible>();
    coin.transform.position = new Vector3(i * 2, 1, 0);
}
```

This documentation provides comprehensive coverage of all public APIs, functions, and components in the Unity project. Each section includes detailed parameter information, usage examples, and setup requirements to help developers effectively use and extend the game systems.
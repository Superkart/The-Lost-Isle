# The Lost Isle

**A Unity 3D Survival Horror Game**

**Demo Video**:  [Watch on YouTube](https://www.youtube.com/watch?v=YtKSy0-J0fU)

---

## Overview

**The Lost Isle** is a first-person survival horror game where players wake up stranded on a mysterious alien island after a crash landing. Players must explore the environment, manage oxygen levels, avoid a lurking entity called "The Watcher," and collect fuel to power their escape.  The game features intelligent AI systems, custom pathfinding algorithms, dynamic resource management, and atmospheric tension through visual and audio feedback.

### Objective
Explore the island while avoiding The Watcher.  Collect fuel canisters scattered throughout the map to power your escape.  Manage your oxygen levels carefully - when oxygen drops to critical levels, oxygen tanks will spawn dynamically to help you survive.

---

## Game Controls

### Movement
| Keybind | Description |
|---------|-------------|
| W | Move Forward in the direction of the camera |
| A | Move Left from the direction of the camera |
| S | Move Backwards from the direction of the camera |
| D | Move Right from the direction of the camera |
| Mouse | Look Around |
| Space | Jump |
| Shift | Sprint |

### Utility
| Keybind | Description |
|---------|-------------|
| F | Toggle Flashlight |
| Esc | Pause Menu |

---

## Key Features

### Advanced AI Systems

**The Watcher - Finite State Machine AI**
- Implemented complete FSM-based enemy behavior with four distinct states:  Idle, Alert, Chase, and Return
- Distance-based state transitions with hysteresis to prevent state flickering
- NavMesh-based pathfinding for realistic movement and obstacle avoidance
- Dynamic environmental effects:  darkness overlay intensifies as The Watcher approaches
- Physics manipulation: player movement becomes progressively heavier when in danger
- Patrol system with configurable waypoints
- Audio-visual feedback synchronized with AI state changes

**Custom A* Pathfinding System**
- Built from scratch without relying on Unity's built-in pathfinding
- Grid-based navigation system with dynamic walkability detection
- Heuristic-based optimization for efficient path calculation
- Real-time pathfinding that guides players toward spawned oxygen tanks
- Visual debugging tools for path visualization during development
- Neighbor node evaluation with diagonal movement support

**Intelligent Resource Spawning**
- Multi-attempt spawn validation system ensures valid placement
- Terrain slope detection prevents spawning on steep surfaces
- NavMesh snapping with fallback logic for reliability
- Raycast-based ground detection with layer mask filtering
- Collision detection to prevent spawning inside other objects
- Distance-based spawning relative to player position
- Responds dynamically to critical oxygen levels

### Resource Management Systems

**Oxygen System**
- Continuous depletion mechanic that drives gameplay urgency
- Real-time UI display using TextMeshPro for clean presentation
- Critical threshold triggers for dynamic oxygen tank spawning
- Audio feedback including low oxygen breathing sounds
- Visual feedback with red screen pulse effect when oxygen is dangerously low
- Integration with game over conditions

**Battery and Flashlight System**
- Toggleable flashlight using Unity Spot Light component
- Battery drain mechanic with visual UI indicator
- Recharge system through collectible batteries
- Atmospheric lighting adjustments for survival horror ambiance
- Integration with overall visibility and tension mechanics

**Inventory System**
- Tag-based item collection and categorization
- Fuel canister tracking with win condition integration
- Prevents duplicate collection through collision state management
- Audio feedback for each item type (oxygen tanks, fuel, batteries)
- Real-time UI updates for collected items

### Visual and Audio Feedback

**UI Systems**
- Dynamic directional arrow indicator that points to nearest oxygen tank
- Viewport-based visibility detection to show/hide arrow intelligently
- Crosshair that follows mouse position for FPS clarity
- Health and resource bars with smooth slider animations
- Fade-in screen effect at game start for polished introduction
- Win/Lose condition screens with proper game state management

**Visual Effects**
- Screen darkening overlay that responds to enemy proximity
- Red pulse effect when oxygen levels are critical
- Particle systems for collectible items (optimized for performance)
- Custom shaders including fuel canister pulse effect and grass swaying
- Ambient lighting setup with directional and point lights
- Material and texture implementation across terrain and game objects

**Audio Design**
- Positional 3D audio for environmental immersion
- Low oxygen breathing sound tied to player status
- Wing flap audio synchronized with Watcher animations
- Collection sounds for different item types
- Death audio trigger on game over
- Background ambiance for atmospheric tension

### Animation System

**Mecanim Integration**
- Wing flap animation triggered when player reaches low oxygen
- Animation synchronized with audio cues for impact
- Idle and movement animations for The Watcher
- Player character animations including jumping and idle states
- Rigged Mixamo character with blend trees for smooth transitions
- Animation state machine for complex behavior coordination

### Shader Programming

**Custom Shaders**
- Fuel Canister Pulse shader for visual attractiveness
- Grass Swaying shader for environmental realism
- Flashlight shader for dynamic lighting effects
- HLSL shader programming integrated with ShaderLab
- Performance-optimized shader variants through URP

---

## Technical Implementation

### Architecture and Design Patterns

**Component-Based Architecture**
- Modular C# scripts following Unity best practices and SOLID principles
- Clean separation of concerns across game systems
- Easy-to-extend manager classes for scalability

**Design Patterns Used**
- **Singleton Pattern**: GameManager, InventoryManager for global access
- **State Machine Pattern**: Enemy AI behavior control
- **Observer Pattern**: Event-driven UI updates and game state changes
- **Object Pool Pattern**: Resource spawning optimization (planned)

### Core Systems

#### AI and Pathfinding
```
Assets/Scripts/Ai/
├── PathFinder.cs          # Custom A* pathfinding implementation
├── GridManager.cs         # Navigation grid generation and management
├── Node.cs                # Node data structure with cost calculations
├── OxygenSpawner.cs       # Intelligent resource spawning system
└── PathFindingTest.cs     # Debugging and visualization tools
```

**Key Features:**
- Custom A* algorithm with gCost, hCost, and fCost calculations
- Dynamic grid generation based on world space
- Walkability detection using Physics.CheckSphere and raycasting
- Neighbor evaluation for 8-directional movement
- Path retracing from target to start node
- Distance calculation using diagonal movement costs

#### Enemy AI
```csharp
// watcherFSM.cs - Finite State Machine Implementation
States:
- Idle:  Waypoint-based patrol with NavMeshAgent
- Alert: Player detection and rotation to face threat
- Chase: Active pursuit with NavMesh pathfinding
- Return: Return to patrol route after player escapes

Features:
- Configurable detection distances (alertDistance, chaseDistance, returnDistance)
- Smooth state transitions with distance-based hysteresis
- Environmental feedback through UI darkness overlay
- Physics-based difficulty scaling (mass manipulation)
- Debug logging for state monitoring
```

#### Resource Management
```
Assets/Scripts/
├── OxygenCounter.cs       # Oxygen depletion and tracking
├── BatteryManager. cs      # Flashlight battery system
├── InventoryManager.cs    # Item collection and storage
├── CollectibleItem.cs     # Item pickup logic with audio
└── GameManager.cs         # Win/lose condition management
```

#### UI and User Experience
```
Assets/Scripts/
├── UiManager2. cs          # Game state UI (win/lose screens)
├── UiArrowIndicator.cs    # Directional navigation indicator
├── IntroSceneController.cs # Opening sequence management
└── FallOffDetector.cs     # Death trigger system
```

---

## Technologies and Tools

### Unity Systems
- **Unity Universal Render Pipeline (URP)**: Advanced rendering pipeline for optimized graphics
- **Unity Input System**: Modern action-based input handling
- **NavMesh and NavMeshAgent**: AI navigation and pathfinding
- **TextMesh Pro**: High-quality text rendering for UI
- **Unity Timeline**: Sequence and cutscene management
- **Shader Graph**: Visual shader authoring
- **Mecanim Animation System**: Advanced animation state machines

### Programming
- **C# (. NET Standard 2.1)**: Primary programming language (94% of codebase)
- **HLSL**: Custom shader programming (4. 9%)
- **ShaderLab**:  Shader property definitions (1.1%)

### Development Environment
- **Unity 6000. 0.34f1**: Game engine version
- **Visual Studio / JetBrains Rider**: C# IDE with Unity integration
- **Git**: Version control
- **Git LFS**: Large file storage for binary assets

---

## Project Structure

```
TheLostIsle/
├── Assets/
│   ├── Scripts/
│   │   ├── Ai/
│   │   │   ├── PathFinder. cs
│   │   │   ├── GridManager.cs
│   │   │   ├── Node.cs
│   │   │   ├── OxygenSpawner.cs
│   │   │   └── PathFindingTest.cs
│   │   ├── Animations/
│   │   │   └── Animate.cs
│   │   ├── AnimationController.cs
│   │   ├── BatteryManager.cs
│   │   ├── CollectibleItem.cs
│   │   ├── GameManager. cs
│   │   ├── InventoryManager.cs
│   │   ├── OxygenCounter.cs
│   │   ├── UiArrowIndicator.cs
│   │   ├── UiManager2.cs
│   │   ├── watcherFSM.cs
│   │   ├── FallOffDetector.cs
│   │   └── IntroSceneController.cs
│   ├── Scenes/
│   ├── Materials/
│   ├── Prefabs/
│   ├── Shaders/
│   └── TextMesh Pro/
├── ProjectSettings/
├── Packages/
└── README.md
```

---

## Getting Started

### Prerequisites
- Unity Hub (latest version)
- Unity Editor 6000.0.34f1 or compatible version
- Git with Git LFS enabled

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Superkart/The-Lost-Isle.git
   cd The-Lost-Isle
   ```

2. **Open in Unity Hub**
   - Launch Unity Hub
   - Click "Add" and navigate to the `TheLostIsle` folder
   - Select Unity version 6000.0.34f1
   - Open the project

3. **Wait for package resolution**
   - Unity will automatically download required packages
   - This may take several minutes on first load

4. **Open the main scene**
   - Navigate to `Assets/Scenes/` in the Project window
   - Open the main game scene

5. **Play the game**
   - Press the Play button in Unity Editor
   - Use WASD to move, F to toggle flashlight

---

## Performance Considerations

**Optimization Techniques**
- Layer mask filtering for raycast optimization
- Efficient grid-based spatial queries in pathfinding
- NavMesh baking for static geometry
- Shader variant optimization through URP
- LOD system for distant objects
- Baked lightmaps for static lighting

**Target Platform**
- PC (Windows/Mac/Linux)
- Quality presets: Mobile and PC configurations available
- VSync and frame rate management options

---

## Development Highlights

### What Makes This Project Stand Out

**Custom AI Implementation**
- Hand-coded A* pathfinding algorithm from scratch
- Complex FSM with multiple states and smooth transitions
- Dynamic difficulty adjustment based on player proximity
- Integrated pathfinding with UI feedback systems

**Robust Spawn System**
- Multi-layered validation for spawn position selection
- Terrain analysis including slope detection and NavMesh verification
- Fallback mechanisms for edge cases
- Performance-conscious with limited retry attempts

**Professional Code Quality**
- Clean, modular architecture following industry standards
- Comprehensive commenting and documentation
- Event-driven communication between systems
- Easy-to-extend and maintain codebase

**Polished User Experience**
- Context-sensitive UI elements
- Smooth state transitions and feedback
- Synchronized audio-visual effects
- Atmospheric lighting and shader effects

---

## Known Limitations and Future Improvements

### Current Limitations
- Some high-resolution terrain textures excluded from repository due to GitHub file size limits
- Git LFS not fully configured in initial version
- Single-player experience only
- Fixed island layout (not procedurally generated)

### Planned Features
- Save/Load system implementation
- Additional enemy types with varied AI behaviors
- Procedural island generation for replayability
- Expanded inventory system with more item types
- Achievement and progression systems
- Multiplayer co-op mode
- Mobile platform support

---

## Technical Notes

**Asset Management**
- Large terrain textures (. tif files) tested locally but excluded from version control
- Particle system effects optimized and included where file size permits
- Material and shader assets included with project
- Custom shaders require URP for proper rendering

**Performance**
- Black hole environment shader may cause minor GPU load spikes
- Ongoing optimization for complex visual effects
- NavMesh baking recommended for best AI performance

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Developer

**Superkart**

- GitHub: [@Superkart](https://github.com/Superkart)
- Project Repository: [The-Lost-Isle](https://github.com/Superkart/The-Lost-Isle)

---

## Acknowledgments

- Unity Technologies for the game engine and Universal Render Pipeline
- Mixamo for character rigging and animation resources
- TextMesh Pro for advanced text rendering capabilities
- Unity Asset Store community for learning resources
- Open-source community for code examples and best practices

---

## Media

**Demo Video**: [Watch the 3-minute gameplay demonstration on YouTube](https://www.youtube.com/watch?v=YtKSy0-J0fU)

The demo showcases:
- The Watcher AI behavior and visual effects
- Custom oxygen detection and pathfinding system
- Resource collection and survival mechanics
- Animation triggers and UI feedback systems
- Atmospheric lighting and shader effects
- Complete gameplay loop from start to win/lose conditions

---

**Built with Unity Engine**

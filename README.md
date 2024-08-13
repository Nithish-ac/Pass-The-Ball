# Pass the Ball Game

## Overview

"Pass the Ball" is a multiplayer game developed with Unity and Fusion. The goal is to pass the ball between players while managing smooth networking and UI interactions. This game demonstrates robust UI management and network integration to provide a seamless multiplayer experience.

## Features

- **Multiplayer Support**: Built using Fusion for real-time networking.
- **Persistent UI Manager**: Maintains UI state across scene transitions.
- **Smooth UI Transitions**: Handles UI changes with button event listeners.
- **Event Management**: Prevents duplicate event listener registrations.

## Approach

### Singleton Pattern Implementation

Implemented the singleton pattern to ensure a single instance of the `UIManager` throughout the game, maintaining UI consistency during gameplay and scene transitions.

### Persistent UI Manager

Used `DontDestroyOnLoad(gameObject);` in the `Awake` method to keep the `UIManager` persistent across scene loads. This prevents UI resets and maintains state, which is crucial for multiplayer games.

### UI Handling and Button Event Listeners

Managed UI transitions using methods like `ShowMainMenu`, `ShowWaitingPanel`, and `ShowOnGameUI`. Added checks to prevent multiple event listener registrations, ensuring each listener is registered only once.

### Null Checks

Implemented null checks for UI elements and the `NetworkManager` instance to safeguard against potential runtime errors, especially in networked scenarios.

## Challenges

### Maintaining UI State Across Scene Transitions

Ensuring the UI state is preserved during scene transitions was challenging. The `DontDestroyOnLoad` approach effectively addressed this issue.

### Ensuring Smooth Network Integration

Integrating UI with networking required careful management of UI transitions based on network events. Handling asynchronous operations and ensuring smooth transitions were key challenges.

### Event Listener Management

Preventing duplicate event listener registrations was essential to avoid memory leaks and unintended behavior. This was managed by adding checks to ensure each listener is registered only once.

## Installation For Code Review

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/Nithish-ac/Pass-The-Ball.git
    ```

2. **Open in Unity**: Open the project in Unity (recommended version X.X.X or higher).

3. **Install Packages**: Ensure necessary packages (e.g., Fusion) are installed.

4. **Build and Run**: Build and run the game to start playing.

## Installation For Play

1.**Download this Zip**:
  https://github.com/Nithish-ac/Pass-The-Ball/releases/download/V0.1/PassTheBall.zip
  
2.**Extract And Play**

## Usage

1. **Main Menu**: Start a new game or join an existing room.
2. **Waiting Panel**: Wait for other players to join.
3. **In-Game UI**: Play the game and pass the ball between players.

## Contributing

1. Fork the repository.
2. Create a feature branch:
    ```bash
    git checkout -b feature/your-feature
    ```
3. Commit your changes:
    ```bash
    git commit -am 'Add new feature'
    ```
4. Push to the branch:
    ```bash
    git push origin feature/your-feature
    ```
5. Create a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Unity Technologies
- Fusion Networking
- Contributors


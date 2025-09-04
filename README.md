# AI Learns to Play Flappy Bird with a Neural Network

![fbgann-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/add2a28c-2d89-49fe-a2ee-a3920db3a47b)

## 📝 Project Description

This project is a Unity simulation of the classic game "Flappy Bird," where an AI agent learns to play the game on its own. The "brain" of each bird is a simple Neural Network, and the population of birds learns over successive generations using a Genetic Algorithm. Birds that fly farther without crashing are considered more "fit," and their successful "genes" (the weights of their neural network) are passed on to the next generation, gradually evolving a capable player.

## 🌟 How it Works

Each bird in the simulation has its own neural network that takes in several inputs from the game state, including:

* The bird's vertical position (height).
* The horizontal distance to the next pair of pipes.
* The vertical position of the top pipe.
* The vertical position of the bottom pipe.

The neural network processes these inputs and produces a single output value. If this value is greater than a certain threshold (e.g., 0.5), the bird "flaps."

At the end of each generation, the genetic algorithm evaluates the "fitness" of each bird based on how far it flew. The most successful birds are selected as "parents" for the next generation. Their neural network weights are combined (crossover) and slightly randomized (mutation) to create a new population of birds that are, on average, slightly better at playing the game.

## ▶️ How to Run the Project

There are two ways to run this project: you can play the pre-built game directly, or you can open the source project in the Unity Editor.

### 🎮 For Players (Easy Method)

1.  Download the repository as a ZIP file and unzip it.
2.  Navigate to the `Builds` folder.
3.  Run the **`GeneticAlgorithmNeuralNetworkGame.exe`** file.
    * *(Note: This executable is for **Windows only**.)*

### 💻 For Developers (Requires Unity)

1.  Clone this repository.
2.  Open the project folder in the **Unity Hub**.
3.  The project needs to be opened with **Unity Editor version 2022.3.35f1** (or a newer version).
4.  Once the project is open, find and open the **`Game`** scene file located in the **`Assets`** folder.
5.  Press the **Play** button at the top of the editor to start the simulation.

## 🛠️ Tech Stack

* **Engine:** Unity 2022.3.35f1
* **Language:** C#
* **Platform:** Windows

## 🧠 Core Concepts & Algorithms

* **Genetic Algorithm:** Implemented from scratch, including:
    * **Selection:** Choosing the fittest individuals to be "parents."
    * **Crossover:** Combining the "genes" (neural network weights) of parents.
    * **Mutation:** Applying random changes to introduce new traits.
* **Neural Network:** A simple feed-forward neural network serves as the "brain" for each bird.
* **Object-Oriented Programming (OOP):** Used C# and Unity's component-based structure to create modular and reusable code for the birds, pipes, and simulation manager.

# Zombie Valley Survival
 
## Overview:
Zombie Valley is a first person horde shooter that combines tactical maneuvering, progression mechanisms, and in-game economy elements. Set in a valley with diverse terrains in the middle of the night, players must navigate through hordes of zombies that drop bullets and other pickups that help the player progress.

[![Gameplay Video](https://img.youtube.com/vi/rHBnnvJ3Pfg/0.jpg)](https://www.youtube.com/watch?v=rHBnnvJ3Pfg)

[Link to Itch.io (despite not rendering properly)](https://dy27.itch.io/sprint-3)

[Download Zip from Google Drive](https://drive.google.com/file/d/17iCuQjZIc8bzvT0cT-tTulBkrISzqptu/view?usp=sharing)

## Core Gameplay Features and Mechanics
1. Zombies
Zombies spawn randomly from all sides to intensify the challenge for the player.

2. Valley Checkpoints
The game includes campfire checkpoints throughout the valley, serving as progression milestones and a clear path to make it to the other side.
Each checkpoint heals and restores some ammo to the player as well as is a safe space from the zombies, except for the boss zombie.

3. Zombie drops
Defeating zombies yields ammo drops, fostering resource management.

4. Health
Players take damage when zombies hit them, but can recover health by healing at checkpoints

5. Shooting bullets
Player can shoot bullets at zombies, hitting a different amount of shots kills different types of zombies.

6. Timer
Limits how much time the player actually has to beat each level, adding urgency and intensity to the game.
Reaching campfire checkpoints adds an additional 30 seconds to your timer.

7. Environmental changes 
Occasionally randomly snows affecting the player by slowing them down and making visibility more difficult, making it harder to progress.
8. Dark
Have a light shining in the direction the player is looking at.
Makes it hard for the player to know where to go
Once the player is close and has a campfire in vision, the player will go straight for the campfire.
Makes campfires more clear and obvious in the distance.

9. Faster Zombies
Once in a while spawn in order to force you to actually shoot back at the zombie otherwise these zombies will likely kill you.
However these zombie have less health and takes less shots to kill

10. Boss
Bigger and tankier than the rest of the zombies.
Is the only zombie required to defeat in order to complete the level.

11. Stealth mechanic.
Holding crouch lowers the movement speed of the player significantly but makes it nearly impossible for zombies to notice the player and pauses zombie spawns, even if the player shoots at the zombie.

12. Up the difficulty
After winning, players can increase the difficulty in order to make the game harder when playing again.
Difficulty is increased by making the zombies stronger.

## Content Inventory
### World Assets:
#### Zombies:
Zombie models with different variations (appearance and behavior)
Animation sets for zombie movement, attacks, and death.
Sound effects for zombie growls, footsteps, and death cries.
#### Valley Checkpoint Campfires:
Campfire models and structures.
Transition animations or effects when reaching a checkpoint.
Health pack and ammo crate models for checkpoint restorations.
#### Difficulty Increase:
Game Manager to keep the information and increase the zombies’ stats.
#### Environmental Changes:
Models and particles for environmental obstacles (snow) affecting player movement and visibility.
##### Tree and Terrain:
Environment that the player has to traverse and navigate in order to survive and beat the level.
##### End goal home:
End home that the player has to reach in order to win the level
##### Bullet drops:
For player to pick up after a zombie dies

### Code Assets:
#### Scripts:
Zombie AI script for spawning, pathfinding, and attacking.
Zombie boss script to ensure is killed to win the game
Campfire script for unlocking new areas, healing, and restoring ammo.
Health and damage scripts for player interactions.
Boss AI script for controlling boss behavior.
Snow Script to turn the snow on and off
Weapon script to shoot out bullets
Script to handle time-related events and transitions.
Bullet pickup script
Particle Effects:
Weather-related particle effects (snow) affecting visibility.
Script for weather to follow the player
### Audio Assets:
#### Sound Effects:
Gunshot sounds for players shooting bullets.
Background music for different levels and during gameplay.
Zombie Sounds
Campfire sound effects
### UI Assets:
#### Night Time Lighting:
Light source model for the player's flashlight.
Script or system for dynamic lighting based on the player's direction.
#### Timer Elements:
Visual representation of the timer (countdown clock or other).
#### Weather Type:
Type of weather in order for the player to be aware.
#### Health bar:
To know how much health you have left
#### Bullets remaining:
Let players know how many bullets they have left.


###### sw-happyhour-archer
##### Happy Hour Games' Case Study

# Simple Archero-Like Control and Combat Mechanics

## Overview
This game is a simple combat simulation where the player controls an archer character using an on-screen joystick. When the character moves, they stop attacking; once stationary, the character automatically shoots arrows towards the nearest enemy. The game features stationary enemy dummies that act as simple targets for ranged attacks. When an enemy is defeated, it respawns at a random position within the map boundaries.

The primary focus of this case is on:
- Simple movement and attack controls
- Targeting the nearest enemy
- A skill system for combat enhancements

---

## Mechanics

### 1. Control Mechanics (Movement via Joystick)
- The character is controlled using an on-screen joystick.
- When the player releases the joystick, the character stops moving, and attacks are triggered automatically.

### 2. Combat Mechanics
- When stationary, the character automatically targets and attacks the nearest enemy.
- Attacks are archer-style ranged attacks, with each shot projected at an angle.
- Projectile motion will be calculated using realistic physics-based trajectory formulas, including gravity.

### 3. Enemies
- Enemies are stationary "dummy" cubes that only receive damage and do not move.
- Each enemy has a health bar displayed above it.
- When an enemy is defeated, it respawns at a random position within the map boundaries.
- At least five enemies should be visible on the screen at any time.

### 4. Camera and Map
- The map is a limited area, with the entire game field visible within a 16:9 portrait view.
- The camera remains fixed.

---

## Skill System
The character can activate any of five skills that enhance attack capabilities. Each skill can be toggled on and off by the player and takes immediate effect upon activation or deactivation.

### Skills:
1. **Arrow Multiplication**: Each attack fires two arrows instead of one.
2. **Bounce Damage**: Arrows that hit an enemy bounce to the nearest additional enemy.
3. **Burn Damage**: Arrows inflict burn damage on impact, causing additional damage over 3 seconds, with a stacking effect.
4. **Attack Speed Increase**: Doubles the attack speed.
5. **Rage Mode**: Doubles the effects of all active skills:
   - **Skill 1**: Fires four arrows.
   - **Skill 2**: Arrows bounce to two additional enemies.
   - **Skill 3**: Burn damage duration increases to 6 seconds.
   - **Skill 4**: Attack speed quadruples.

---

## UI and Skill Activation Mechanics
- The skills are displayed in a tab menu that can be opened or closed during gameplay.
- Each skill is represented by a button within the tab menu.
- When a button is pressed, the corresponding skill is activated, indicated by an outline around the button.
- Pressing the button again deactivates the skill, immediately ending its effects.

---

## Key Considerations
- **Flexibility**: Skills should be designed with scalability in mind, allowing for future upgrades.
- **Design Patterns**: A minimum of three different design patterns should be implemented (e.g., Strategy, Factory Method, and Observer patterns).
- **Projectile Motion**: The character's projectile attacks should follow realistic physics-based projectile motion, including gravity.
- **Efficient Targeting**: Implement an efficient algorithm to identify the nearest enemy based on the character’s position.



# CS485-monkey-ball
Simple, but hopefully well polished Unity game implementing mechanics from Super Monkey Ball

## Summary
This project is inspired by the classic Monkey Ball games, specifically Super Monkey Ball for GameCube (https://www.youtube.com/watch?v=MSQCQ8UaI8A), meaning it is a puzzle platformer focusing on momentum and a simple control scheme.

The player controls a ball that rolls around the world. They traverse a bunch of simple levels to reach a goal, with each level presenting new obstacles or reincorporating old obstacles in new ways. Some obstacles include: bridges, pitfalls, ramps, slides, bumps, moving platforms, buttons (to trigger platforms), and AI enemies that chase you. The game has a running timer throughout the levels, which allows the player to set accumulative high scores. The scoring system enables a player to compete to get faster and faster on every play through. On the menu, there is a New Game option, a level select option, and options (where the player can toggle things like FOV).

## Graphics
The game is colorful and cartoon styled. The models will be primarily crafted of simple shapes. I had to make some tweaks, but there is a Unity asset package which provides a “Toon” shader that fits my style. I’ve added post processing elements such as bloom/AA/motion blur.

## Controls
The game is playable with a keyboard or a Nintendo Joycon. When the player pushes forward on the control stick (or presses W) the ball will slowly accelerate forwards, similarly when the player pushes backwards (or S) the ball will start to accelerate backwards. The left and right directions (A or D) applies a force that moves the player in those directions.
  
## Main Features
•	Main Menu w/ “New Game”, “Level Select”, “High Scores”, “Options” - 5 pt

•	Options Menu with at least FOV slider - 5 pt

•	At least 5 very simplistic levels - 10 pt

•	Simple control scheme created by applying forces to the player character - 10 pt

•	Keyboard input - 4 pt

•	Third Person camera that follows the player, must rotate smoothly - 10 pt

•	Models/Textures for player, obstacles, world - 5 pt

•	Skybox graphics - 2 pt

•	Obstacles: 

  o	bumpers - 1 pt

  o	bumps - 1 pt

  o	launchers (vertical bumper) - 1 pt

  o	moving platform - 1 pt

  o	button - 1 pt

•	UI for speedometer/timer/fps/lives - 5 pt

•	Save/Load and other scripting for timer, lives, highscores - 5 pt

•	Loading between levels when you reach a goal - 5 pt

•	Spawning player, handling deaths - 5 pt

## Stretch-Goals

•	10 or more levels - 5 pt

•	Joycon Motion Controls - 3 pt

•	Additional camera tricks - changing FOV/simulating world tilt/bloom - 2 pt

•	Cartoon shaders - 4 pt

•	Glass shader (w/ outline tweak) - 4 pt

•	Sound/particles on collisions - 2 pt

•	Transition between levels (Titles?) - 2 pt

•	Way of showing current direction traveled (ball texture, object inside ball?) - 2 pt

•	Bowling mini game (maybe local multiplayer where the players can hand off the controller) - 0 pt 

## Assorted Descriptions/Notes/Techniques/Credits

### Movement
In forward/back, movement is done by adding a force in the direction of the camera’s forward (this allows the ball to rotate freely), this works because the camera will always be looking towards the forward direction of the ball. Similarly, left/right are done by adding a force in the direction of the camera’s right vector.
 
### Camera
So, I made a few errors right away when I started working on camera. Mainly, I locked the camera behind the player, so that it would always be a distance of 7 away from the player and 2 above the player, which made it difficult to rotate the camera over the player when the player moves “under” the camera.

Ultimately, I ended up changing my approach, and used a more typical follow camera. My implementation gets a normalized vector from the camera to the ball, add the offsets, and then move the camera into place using lerp (using a multiple of Time.deltaTime for alpha value to account for possible frame drops).

### Menu
The menu manager has a list of menu items and an int pos which gives the current position in the list. Up and down will add one to pos and color the next element in the list. Confirm will do something based on the text of the selected element (for example, new game loads the first level and options loads the options page).
 
### Font
Font taken from here: https://www.dafont.com/big-bottom-cartoon.font?l[]=10&l[]=1

### Making a sphere “hollow”
Unity no longer has concave collisions, aka collisions on the inside of a curve - they’ve been deemed too slow and computationally intensive. Nvidia’s PhysX3 removed support for concave colliders/triggers.

Then how can we keep a 3d object in a ball? We can fake it’s outside using convex colliders.

Wrap convex colliders (I used rectangular colliders) outside of the rim of your spherical object. Make only one loop, then create a prefab and rotate it to cover the entire spherical object. Now we have a collider that’s almost concave, but it messes with the collider for the sphere itself.

We only want the object inside the sphere and the colliders outside the sphere to interact, how can we do that? Give them their own layer!

In inspector, drop down the layer list and click add new layer. Create a layer for the inside object and the spheres, then go into Project Settings>Physics and uncheck everything under that layer until only your new layer can interact with itself.
  
### Toon Shader
Unity already has a toon shader in the standard assets package. It’s pretty easy to implement.

Import the package, then make a new material. Change the shader to Toon/Lit Outline (in my project all toon materials can be lit up/have shadows and have outlines of 0.003). Apply the 2d toon ramp gradient that is included in the package. If you’ve created a texture using the UV map, you can apply it in the Base texture slot.

### Post Processing
Unity has post processing for the camera in their standard assets package. I’ve used it to tweak the bloom on my game.

First right click and go to effects > post processing profile, and tweak the settings of your new profile in Inspector.

Then you need to add a Post Processing Behavior script on the Main camera in your scene, and put the new profile into the slot in the editor.

### Glass Ball - AKA struggling with Shaders
Asset Package Used: https://assetstore.unity.com/packages/vfx/shaders/mk-glass-free-100712

Outline code used in shader stolen from here: https://forum.unity.com/threads/adding-an-outline-to-unity-5-standard-shader.286554/

Essentially, I used the mk-glass-free asset package and added to the shader so that my ball would have an outline.
 
Also, adding shadows to the glass shader conflicted with the outline that I used, so instead I attached a child that is the same shape as the main glass ball that uses with a different shader that only draws shadows.

### Cat Model
Model taken from here: https://free3d.com/3d-model/low-poly-cat-46138.html

The animal model cannot be a child of the ball (because we don’t want it to be affected by position/rotation), but we need it to spawn inside the ball no matter where the ball is placed. So I created a script that spawns the cat at the position of an empty object that is a child to the ball.

### JoyCon Controls
Asset Package: https://github.com/Looking-Glass/JoyconLib

Super easy to follow demo code is included with asset package, basically my movement script gets the accelerometer readings every update, and adds force the same way as before, but scaled to the amount of the x accel value for forward/back and to the y accel value for left/right. The buttons or stick is used for menus (positive y is up, negative y is down) and the trigger or stick pressed in is used for selecting a menu option.

### Player Data
We need to save int FOV (from options), the float current elapsed time during a playthrough, and the int number of remaining lives. To do this I use PlayerPrefs, which allows us to get/set floats and ints. When “new game” is selected from the menu, the timer is set to 0 and lives is set to 3. When a scene loads, the timer loads the last value and continues counting in a coroutine. There is a trigger underneath the map, and when the player enters it, the scene reloads and the lives decrements by 1. At the moment, a game over just returns you to the menu.

### Pause
Since all gameplay scripts are tied to Time.deltatime, we can set Time.timeScale to 0 to pause and set it to 1 to unpause. I make a panel and “pause” text appear when you’ve paused and disappear when unpaused.

### Goal
The goal is a custom mesh created in blender shaped like a donut. I created a super simple checkered texture for its material. In the center is a trigger that loads the next level when the player enters it.

### Platforms
The platforms are custom meshes created in Blender and have a texture added to them (using UV maps so the texture only appears on the top.

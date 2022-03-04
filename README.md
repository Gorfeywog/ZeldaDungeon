# ZeldaDungeon
CSE3902 Project
The Beedles - Caleb Bucci, Josh Harper, Nick Jones, Ronnie Marashdeh, Luke Van De Weghe

Sprint_2: 
	To navigate our implementation for creating interfaces and classes for different parts of our game framework, one can go through the folder structure outlined below:
		- Commands - Contain classes that implement what each input from the user does on the screen. These classes will be called in the KeyboardController, 
					 which implements the IController interface.
		- Content - Contains the texture atlases created by the group to hold all essential sprites. The texture atlases were created by a script that Luke wrote, which 
					makes the accessing of sprites much easier.
		- Entities - Contains the bulk of our implementations for sprint_2: interfaces for each game framework's function, state machine for Link, and classes that implement
					 the function of the blocks, items, enemies, and projectiles.
		- Sprites - Contains the ISprite interface, which is implemented by the classes contained in BlockSprites, EnemySprites, ItemSprites, and LinkSprites. To reduce the amount of 
					classes our project contains, we used two classes for each possible sprite to be generated on the screen: animated and static. The sprite factories are also held here, 
					which handles what sprites from the texture atlas will be passed into the animated or static sprite classes.

Notable changes from the original NES game includes the wall master enemy was changed from orthogonal movement to diagonal movement. The group decided to make it a flying enemy, 
similar to the keese, rather than a walking enemy.
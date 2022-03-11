# ZeldaDungeon
CSE3902 Project
The Beedles - Caleb Bucci, Josh Harper, Nick Jones, Ronnie Marashdeh, Luke Van De Weghe

Sprint_3: 
	To navigate our implementation for creating interfaces and classes for different parts of our game framework, one can go through the folder structure outlined below:
		- Commands - Contain classes that implement what each input from the user does on the screen. These classes will be called in the KeyboardController, 
					 which implements the IController interface.
		- Content - Contains the texture atlases created by the group to hold all essential sprites. The texture atlases were created by a script that Luke wrote, which 
					makes the accessing of sprites much easier.
		- Entities - Contains the bulk of our implementations for sprint_2: interfaces for each game framework's function, state machine for Link, and classes that implement
					 the function of the blocks, items, enemies, and projectiles. This now contains a folder called "Pickups" to denote the items a player can pick up and interact 
					 with. 
					 All implementations regarding collision are also contained within the entities folder. At the moment, our implementation for CollisionHandler has every enemy 
					 having its own CollisionHandler. This is done to let each entity have its own logic on how its interacting with other entities and objects within the game. 
					 EntityList is used to keep track of all entities that can currently be interacted with in the game. Also, the EntityUtils class can be found within here.
		- InventoryItems - Contains items the player can hld in their inventory. These items are held in a dictionary.
		- RoomData - holds the csv files used to parse through and generate the rooms within the dungeon.
		- Rooms - holds the actual parser for the csv files, the classes to implement how doors, rooms, and walls are generated in the game.
		- Sprites - Contains the ISprite interface, which is implemented by the classes contained in BlockSprites, EnemySprites, ItemSprites, and LinkSprites. To reduce the amount of 
					classes our project contains, we used two classes for each possible sprite to be generated on the screen: animated and static. The sprite factories are also held here, 
					which handles what sprites from the texture atlas will be passed into the animated or static sprite classes.
					A few additions for this folder came along with sprint 3. For example, the SpriteUtil and SpecialSpriteFactory are notable new classes that have been implemented.
					The SpriteUtil class eliminates many magic numbers throughout the project and establishes positions and sizes for the sprites necessary in the game.
					The SpecialSpriteFactory is used to handle how the sprite for the walls are handled.

If you would like to test the functionality in a larger or smaller window, you can change the SCALE_FACTOR constant on line 10 of the SpriteUtil class. This will adjust everything
in the game to be larger or smaller for you to see and test.

Notable changes from the original NES game includes the wall master enemy was changed from orthogonal movement to diagonal movement. The group decided to make it a flying enemy, 
similar to the keese, rather than a walking enemy.
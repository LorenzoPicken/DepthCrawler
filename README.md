<h1>Depth Crawler</h1>
<p>is a text based, dungeon crawler, action RPG with a simple looting and combat system as well as a fully realised story reliant on NPC dialogue and a quest system. This project takes a lot of inspiration from RPGs like runescape and Dark Souls as well as a combat system similar to pokemon. This was a solo project developed over the span of two weeks in Visual Studio, parallel with another one of my projects, <a href="#">Death Delt Hand</a>. This game relies heavily on random procedural generation which allows for a different experience each and every playthrough. This randomness effects the map layout, enemy spawns, loot chances as well as other factors.</p>
<br>



<h2>Gameplay</h2>
<p>The main gameplay loop revolves around exploring the dungeon in order to find support items, complete quests and eventually reach the ending. To do this, the player is given access to Primary Actions. </p>
<p>Primary Actions can be broken down into 4 main mechanics which come in the form of executable actions that the player can choose between. These are:</p>
<ul>
  <li><p>Navigation</p></li>
  <li><p>Combat</p></li>
  <li><p>Looting</p></li>
  <li><p>Inventory</p></li>
</ul>
<img src="Images/MainActionDemo.png">
<br><br>


<h3>Navigation</h3>
<p>The "Leave Room" command is the navigation prompt which allows the player to move from chamber to chamber as they explore the dungeon. When prompted with the command *LEAVE*, the user will be presented with up to 4 directions to leave by (North, South, East, and West). The number of exits and the direction of each of these exits are randomised upon the room's creation. Each room will always have at minimum 2 exits and can never lead to a dead end. Additionally, returning from the entered direction of a room will lead back to the previously visited room, meaning that the player can accurately backtrack to rooms they have already explored should the need arise.</p>
<img src="Images/NavigationDemo.png">
<a>Navigation Menu that will be presented to the player if they type the LEAVE prompt</a>
<br><br>


<h3>Combat</h3>
<p>Combat is a focal point of gameplay as the player will encounter various foes as they travel through the dungeon's various chambers. Whenever the player enters a new chamber, there is a chance that they will partake in battle, whether this be by choice of the player or a forced confrontation by the enemy occupying the room. This is determined by the enemy's awareness, a predetermined state that is assigned to them when they spawn. This awareness can be one of three possibilities:</p>

<ul>
  <li>
    <h4>Alert</h4>
    <p>An alert enemy will automatically force a confrontation upon entering its chamber. These enemies are not susceptible to surprise attacks</p>
  </li>
  <li>
    <h4>Distracted</h4>
    <p>A distracted enemy will not force the player into a battle upon entering the room, however, they are easily alerted by actions such as looting or attempting to leave the chamber. These foes are susceptible to surprise attacks.</p>
  </li>
  <li>
    <h4>Asleep</h4>
    <p>Sleeping foes are the least likely to notice a player when they enter the chamber and will not force combat unless alerted. Actions such as looting or attempting to leave the room are possible without alerting the enemy however, the longer a player remains in the chamber, the higher the chance that they wake the sleeping monster. While asleep, they are susceptible to surprise attacks.</p>
  </li>
</ul>

<p>Upon triggering a battle, the player will be locked into turn based combat with their opponent. In this situation, regular action prompts will be replaced with combat specific actions as seen below:</p>
<img src="Images/CombatDemo.png">
<br>
<p>Combat is a game of probability determined by taking both the player and the monster's statistics and pitting them up against each other to determine the likelihood of a successful action. These statistics are:</p>
<ul>
  <li>
    <h4>Hit Points</h4>
    <p>Hit Point or HP are the health of the associated entity. When this drops to zero, the entity will die. For the player, this results in a game over.</p>
  </li>
  <li>
    <h4>Strength</h4>
    <p>Strength directly correlates to the damage that an entity can deal. Strength is also used in calculating the probability of a successful guard break against a foe by measuring against the opponents defence when attacking.</p>
  </li>
  <li>
    <h4>Defense</h4>
    <p>Defence is first and foremost a value used in the calculation of damage negation. The higher the defence compared to the strength value of an incoming attack, the less damage that attack will do. Defence also determines the likelihood of a successful block against incoming attacks when guarding.</p>
  </li>
  <li>
    <h4>Agility</h4>
    <p>Agility has two main functions in combat. The first is determining the likelihood of an attack missing its intended target. The second function is for mid-fight escapes. In both cases, the agility of both fighters are measured against each other. The fighter with the highest agility is more likely to avoid attacks and successfully escape.</p>
  </li>
  <li>
    <h4>Accuracy</h4>
    <p>Finally, accuracy determines the probability of attacks hitting vital areas during combat, resulting in higher damage numbers. The higher the accuracy of an entity, the more likely they are to hit the torso of an opponent or even the head as opposed to the limbs which are more well guarded.</p>
  </li>
  
</ul>
<p>Combat can also be affected by entity specific statistics such as different monster types having unique weaknesses, resistances and even invulnerabilities as well as stat boosting special abilities. Examples of this include resistances to slashing damage from swords and axes or weaknesses to fire damage from spells which will increase or reduce damage from those sources. Some monsters are also invulnerable to status effects such as poison or stuns.</p>
<p>In the case of the player, base stats are altered during combat. Equipped weapons have their own individual stats that affect the player. For example, damage and guard effectiveness will add to player strength and defence while weight will reduce agility. Most weapons are balanced such that they excel in some aspects while being weaker in others. For example, the battle hammer excels in damage and guarding ability but heavily reduces agility. By contrast, the flail has above average damage and slightly lower weight at the cost of being a poor defensive choice.</p>
<p>Mana and fatigue levels also directly affect performance and must be managed in order to not suffer any drawbacks. Low mana will prevent spell casting which can be very powerful in a battle. Fatigue will heavily hinders combat ability if it gets too high as it will reduce agility, defence and strength across the board.</p>
<br><br>

<h4>Looting</h4>
<p>Looting is another focal point of the experience and will be the main mechanic that allows the player to cheat death. Due to how prominent combat is, resources such as mana, fatigue and even health can be expended rather quickly. Because of this, support items are a very powerful resource that can be collected during a playthrough. These items can help replenish health and mana, deplete fatigue build up, cure ailments and even provide temporary stat boosts. While there are searchable items in the environment, the player will have the option to search them to find various consumables so long as they aren't in combat. Upon typing the *SEARCH* prompt, a looting menu will be displayed with a list of all places the player can search. This includes bookshelves, vases, skeletal remains, etc. Searchable items are created along with the room they are in and can either reward the player with a consumable or yield nothing.</p>
<img src="Images/LootingDemo.png">
<br><br>

<h2>NPC Interactions</h2>
<p>Upon entering a room, if an NPC is present, the player will be able to interact with them through a special menu. This menu is only available if an NPC is in the current room. These interactions can reveal information about the world the player finds themselves in, start a quest, progress the story, or even reward the player with an item or effect. NPC's can be spoken to multiple times which will unlock new dialogue. Additionally, as the story progresses, new characters will present themselves and existing ones will have their dialogue updated.</p>
<br><br>

<h2>Procedural Generation</h2>
<p>As explained above, the game heavily relies on procedural generation. This procedural generation affects the layout of the rooms and its contents. This ensures that no two playthroughs are the same, giving this game more replayability. The following is a list of all procedurally generated elements.</p>
<br>
<h4>Rooms</h4>
<p>There are three different types of rooms within the game files. These are the Dungeon Rooms, Loot Rooms and Quest Rooms. Upon entering a new chamber, the room will be generated based on one of these three templates. Which template the system chooses is completely random and only affected by the spawn chance of each room type, aside from one exception.</p>
<br>
<h5>Dungeon Room</h5>
<p>The Dungeon Room is the most common type of chamber to spawn and has the following conditions:</p>
<ul>
  <li>Must Contain 1 Enemy</li>
  <li>May Contain Lootable Items</li>
  <li>Cannot Contain Any NPCs</li>
</ul>
<p>The bulk of the challenges that the player faces will take place within rooms of this type during combat. This is the only common room that will contain enemies.</p>
<br>
<h5>Loot Room</h5>
<p>Loot rooms are more rare than dungeon rooms but in exchange also have a much higher chance of spawning some consumables for the player to loot.</p>
<ul>
  <li>Cannot Contain Enemies</li>
  <li>Must Contain Lootable Items (Higher Max Items Than Dungeon Rooms)</li>
  <li>Cannot Contain Any NPCs</li>
</ul>
<p>The more loot rooms a player finds in their playthrough, the higher their chances of survival.</p>

<h5>Quest Room</h5>
<p>Quest Rooms are the rarest of all the possible rooms and trigger events that progress the story. These rooms contain NPCs, Cutscenes and Boss Fights</p>
<ul>
  <li>Can Only Contain Boss Type Enemies</li>
  <li>Cannot Contain Lootable Items</li>
  <li>Must Contain NPC's or a Cutscene</li>
</ul>
<p>Unlike the other rooms, Quest Rooms have a spawn gap of 3 to avoid potentially completing the story by accessing all the rooms back to back. Additionally, a quest room cannot spawn unless the player has reached the corresponding point in the story to access that room or completed the quest to unlock it.</p>
<br><br>
<h4>Monsters</h4>
<p>All monsters aside from Boss Monsters are procedurally generated with the room which randomises the species, subspecies and statistics. In the game, there are 6 types of basic monster species:</p>
<ul>
  <li>Minotaurs</li>
  <li>Giant Spiders</li>
  <li>Gargoyles</li>
  <li>Bonedrikes</li>
  <li>Oculus Seekers</li>
  <li>Living Armour</li>
</ul>
<p>Each one of these monsters has different behaviours and base stats from the others. This makes them fight very differently from one another. Additionally, all monsters have two special abilities unique to their species, for example, giant spiders have the ability to stun the player and poison them whereas minotaurs have a devastating heavy attack and a rage amp which can increase their strength. Subspecies are variations of the general species of a monster. This leads to two of the same monster having a different name, description and interaction text from one another. This is to help create variety within the 6 main monster types. This can be seen with the living armour which has three different variations. Finally, each monster species has particular base stats which are scaled up in relation to the player's current level. This is done by adding up all the player's stats, then the base monster's and then from here, a random number is rolled between 10 levels under the player's current level and 5 above. Then each stat is increased until the total level of the monster is equal to the random level chosen.</p>

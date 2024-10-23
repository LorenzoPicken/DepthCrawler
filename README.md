<h1>Depth Crawler</h1>
<p>is a text based, dungeon crawler, action RPG with a simple looting and combat system as well as a fully realised story reliant on NPC dialogue and a quest system. This was a solo project developped over the span of two weeks in parallel 
with another project <a href="#">Death Delt Hand</a>. This game relies heavily on random procedural generation which allows for a different experience each and every playthrough. This randomness effects the map layout, enemy spawns, loot chances as well as other factors.</p>
<br>



<h2>Gameplay</h2>
<p>The main gameplay loop revolves around exploring the dungeon in order to find support items, complete quests and eventually reach the ending. To do this, the player is given access to Primary Actions. </p>
<p>Primary Actions can be broken down into 4 main mechanics which come in the form of executeable actions that the player can choose between. These are:</p>
<ul>
  <li><p>Leave Room</p></li>
  <li><p>Surprise Attack</p></li>
  <li><p>Search for Valuables</p></li>
  <li><p>Check Inventory</p></li>
</ul>
<img src="Images/MainActionDemo.png">
<br><br>


<h3>Navigation</h3>
<p>The "Leave Room" command is the navigation prompt which allows the player to move from chamber to chamber as they explore the dungeon. When prompted with the command *LEAVE*, the user will be presented with up to 4 directions to leave by (North, South, East, and West). The number of exits and the direction of each of these exits are randomized upon the room's creation. Each room will always have at minimum 2 exits and can never lead to a dead end. Additionally, returning from the entered direction of a room will lead back to the previously visited room, meaning that the player can accurately backtrack to rooms they have already explored should the need arise.</p>
<img src="Images/NavigationDemo.png">
<a>Navigation Menu that will be presented to the player if they type the LEAVE prompt</a>
<br><br>


<h3>Combat</h3>
<p>Combat is a focal point of gameplay as the player will encounter various foes as they travel through the dungeons various chambers. Whenever the player enters a new chamber, there is a chance that they will partake in a battle, whether this be by choice of the player or a forced confrontation by the enemy occupying the room. This is determined by the enemy's awareness, a predetermined state that is assigned to them when they spawn. This awareness can be one of three possibilities:</p>
<ul>
  <li>Alert</li>
  <li>Distracted</li>
  <li>Asleep</li>
</ul>


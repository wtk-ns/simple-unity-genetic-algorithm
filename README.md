#simple-unity-genetic-algorithm
<h3>Description</h3>

This is simple genetic-algorithm with mutation, based on unity 3D engine.

You have got a creature, witch must have:
<ul>
	<li>BotHead - the part by which it is determined whether the creature has reached the goal or not</li>
	<li>BotPart - the part, which have controlled joint
</ul>
In the prefabs directory you can find creature's prefab. The creature's prefab can be changed as you wish, but it should be afraid to have parts and a head. 
<br>
Population size, distance, max angles of joints and other - you can set in the BotProperties.cs
<br>
By default - the main task of the creature is to go as far as possible
<br> 
By playing with mistakePossibility variable - you can change mutation possibility in population. Too low probability of an error will lead to finding 
a local minimum, therefore it is not recommended to set it below 0.1.

<h3>Techs</h3>
<ul>
	<li>c#</li>
	<li>Unity3D</li>
<ul>
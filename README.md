# Exponentia

Location. There is a long rect board, limited by walls. 
Gameplay. At the start of the board we spawn a cube at the center with some Power-Of-2 number value (Po2). The Po2 value of spawned cube is:
75% probability - 2
25% probability - 4
Players can down the screen anywhere with their finger to prepare the cube to launch. When the finger is down, Player can drag the cube left or right by scrolling the finger left or right. When the finger is up, the cube physically launches in forward direction. Cube merges with any other cube, if:
It has enough impulse at collision. The cube should not just touch another cube, it should have some minimal impulse directed to another cube when colliding.
It has the same Po2-number.
As a result of the merge, two cubes should become one with the value that equals the sum of their value (that is also bigger Po2).

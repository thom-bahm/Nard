# Nard
Simple 2D Board Game in Unity

Ultimately this project has a small goal to make a playable game against an AI (maybe networking if I get into it), but mostly for the purpose of learning how to write the AI for a basic decision set (The pieces all move the same, only one way to win, and there are very simple capture rules).

This game (Nard) is / is based on the following [quick article](http://www.cyningstan.com/game/389/nard) which lists the rules and origins; It is an extremely old, and *extremely* simple board game.

Some useful properties of 'Nard':
- it is a 2-player, turn based game (sequential)
- there is perfect information
- no randomness involved
- the game must always end

Therefore it can be considered a 'combinatorial' game. This should allow me to use a Game tree to write the AI , which is probably the best choice for this game. I may still opt for a Neural Net approach just to practice some stuff I recently learned in a Math for AI class. Also I may try to calculate if it is 'solvable' as a combinatorial exercie but for now I'll leave it alone.

Current state: Basic game logic is 'finished'. Need to create Menu screens and then I can work on the AI.

Hopefully very soon!

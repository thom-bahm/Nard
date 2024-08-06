# Nard
Simple 2D Board Game in Unity - Ultimately the purpose is to learn how to write the AI

This project has a small goal to make a playable game against an AI (maybe networking if I get into it), but mostly for the purpose of learning how to write the AI for a basic decision set (The pieces all move the same, only one way to win, and there are very simple capture rules).

**Current state**: Basic game logic is 'finished' - will write some test cases soon to check. Need to create Menu / game over screens and then I can work on the AI.

### Overview

This game (Nard) is / is based on the following [quick article](http://www.cyningstan.com/game/389/nard) which lists the rules and origins; It is an extremely old, and *extremely* simple board game.

Some useful properties of 'Nard':
- it is a 2-player, turn based game (sequential)
- there is perfect information
- no randomness involved
- the game must always end

Therefore it can be considered a 'combinatorial' game - this makes writing the AI more straightforward than if some of those properties were not true.

I may try to prove /calculate some useful properties about the game such as the Avg branching factor and if it is 'solvable' as that could prove useful in deciding the correct AI to program.


### To-Do

- MOSTLY: Rewrite some of the code so it will be more friendly with writing the AI:
- Move class that uses an integer (bit mask) to represent a move including info about the start and end square (I think I can use 8 bits to do this?)
- GenerateMoves() function that generates a list of all legal moves for all pieces on the board (for the player who is making a move) and returns that list.
- LegalMoves() function that returns a list of legal moves for a specific piece
- Small optimization: Can precalculate the # of moves to an edge for every position, so we can just look up that information if needed.
- small feature: option to show the squares which are legal moves for a selected piece.
- **Backtracking feature similar to what exists on chess.com**

### Current thoughts on AI Implementation

#### Strong AI
- MCTS - Monte Carlo Search Tree
  - This seems like a solid approach to me, as even though the *rules* of Nard are very simple, the branching factor could be quite large which makes the Minimax approach potentially inefficient. (Branching factor on the first move is 53)
  - Could also add some useful heuristic eval functions to make this more efficient.
  - Need to read more about this
 
- Minimax w/ Alpha-beta pruning
  - Could be the best, most straight-forward approach
  - Although there may be a quite large avg branching factor, I believe there are quite useful heuristics I could use to prune a very large portion of the decision tree.  

- Learning
  - This is probably not the way to go; the game is too simple with no exisiting data, making this method unlikely to pay off
  - If I train using simulated games it could be useful
  - I get to implement some stuff that I learned this past semester so that would be nice.

##### Basic / 'Very Easy' AI
- Greedy algorithm, only judges a move by if the immediate result is gaining material
- Ironically this AI wouldn't be that bad just by looking 1 move into the future & defense capabilities.

Before or after writing the POC of the AI, implement a backtracking feature (look at the moves that have already happend)

Hopefully very soon!


### Resources:

- [Combinatorial games article](https://hal.science/hal-01883569/document)
- [Deep reinforcement learning for games](https://towardsdatascience.com/how-to-teach-an-ai-to-play-games-deep-reinforcement-learning-28f9b920440a)
- [MCTS For Connect 4 (Medium)](https://pranav-agarwal-2109.medium.com/game-ai-learning-to-play-connect-4-using-monte-carlo-tree-search-f083d7da451e)
- [A* stanford](https://theory.stanford.edu/~amitp/GameProgramming/AStarComparison.html)

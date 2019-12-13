# City Builder Prototype

Requires Unity 2018.3.0f2 or higher (in older ones some models can be broken)

**Goal:**
Implement a simple city builder where you can place and move buildings and produce resources from these buildings.

**Set of features:**

The game has two main modes:
* **Regular mode:** in which player can select a building by clicking on it and see a building name on top of it and current production progress (or can start a new production if no production is running)
* **Build mode:** the player can place a new building or move an existing building 
when player presses 'build mode' he should see a simple list with building's names and their prices where he can choose a new building to place.
Or the player can either select and move an existing building on the grid.

* Buildings should not be placeable on cells occupied by other buildings
* Placing a building cost resources
* When building is placed it should go through construction phase first (simple progressbar on top of the building) for 10 seconds before it can produce anything.

**Building types**

3 Types of production buildings:
* 'Residence' - a building which produces automatically 100 gold every 10 seconds. After production is finished, the next one starts automatically. 
Placing a building cost 100 gold
* 'Wood production building' - a building which player first have to select and press 'start production' (just a simple button which appears on top of the building when we select it) and after it should start producing 50 wood in 10 seconds.
Placing a building cost 150 gold
* 'Steel production building' - same as wood production but produces steel instead. Produces 50 steel in 10 seconds.
Placing a building cost 150 gold and 100 wood

* Player is able to select a building in the regular mode by clicking on it and see a simple progressbar of current production progress. 


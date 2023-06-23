# BadWay
Simple roguelike with procedural generation of map.

Mobs spawn in front of the player and a map is generated. For each kill, the player levels up. The player also receives coins that can be used during one session of the game. For killing 5 mobs, the player receives a soul. After the session ends, the souls can be spent on improving the player's characteristics.

![](https://github.com/mackbet/BadWay/blob/main/Assets/git/demo.gif)

Improvable characteristics are available after certain conditions are met.

![image](https://github.com/mackbet/BadWay/assets/89740987/c7433eec-3ecb-4800-8750-d07ef3d77b04)

# Mobs
There are two types of mobs:
- BadBat
- Mummy

BadBat is developed on the usual algorithm. Movement is based on the rigidbody component.

![image](https://github.com/mackbet/BadWay/assets/89740987/9bdc035a-57f2-4851-94dd-a87bc31af2ec)

The mummy is designed using the NavMesh component.

![image](https://github.com/mackbet/BadWay/assets/89740987/35778d16-63e5-4648-8b0e-7ccb41d9d73a)

The map has a NavMesh Surface component. He creates a map for the movement of the mummy. At the same time, each time during the generation of the map, this component updates the map.

![image](https://github.com/mackbet/BadWay/assets/89740987/a80baeb5-9f61-4fa8-880c-f6ad1344d9dc)


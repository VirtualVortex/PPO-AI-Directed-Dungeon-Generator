### MSc Major project - PPO AI directed dungeon crawler ### 

This repository has been pre-configured with a .gitignore and .gitattributes file specific to Unity projects using git-lfs. 

This project was created for Unity [2020.3.12](https://download.unity3d.com/download_unity/b3b2c6512326/Windows64EditorInstaller/UnitySetup64-2020.3.12f1.exe?_gl=1*19gknxd*_ga*MTQ0MTQ4NzI0Ni4xNjIxOTM4ODk1*_ga_1S78EFL1W5*MTYzMDQyMTA4MS4zMS4wLjE2MzA0MjEwODEuNjA)

This project contains asset packs for the player mechanics, enemy mechaincs and dungeon generator

* Enemy AI / mechanics - https://assetstore.unity.com/packages/tools/ai/emerald-ai-3-0-203904#description

* Player mechanics - https://assetstore.unity.com/packages/tools/gui/item-inventory-system-45568#description

* Dungeon generator - https://assetstore.unity.com/packages/tools/ai/procedural-level-generator-136626#reviews

* Dungeon environment - https://assetstore.unity.com/packages/3d/environments/dungeons/ultimate-low-poly-dungeon-143535

These assets have been modified to ensure the systems can interact with one another and with the custom features I created.

Additionally, this project uses Unity's Machine Learning package to help build the PPO AI, the libary uses PyTorch version 1.7.1 for Reinforcment Learning. All of these are installed automatically when installing Unity's machine learning package. Additionally, this it install the libaries Python 3 is required.

Unity's Machine Learning package can be found here - https://github.com/Unity-Technologies/ml-agents

My work to project is:
* Director AI
* Training system
* Scene changer manager
* Flow element bars
* Float variables
* Flow element interaction
* DispayTotal
* PPOAgent script
* Respawn collision system
* DistanceToTeleporter (compass)

All custom scripts and assets can be found in GAM705_Artefact_Repo/Assets/Scripts and GAM705_Artefact_Repo/Assets/ScriptableObjects

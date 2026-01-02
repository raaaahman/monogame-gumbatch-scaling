# MonoGame & GumBtach Scaling

This project showcase the use of GumBatch in a MonoGame project.

## How to run

1. Clone this repository: `git clone https://github.com/raaaahman/monogame-gumbatch-scaling`
2. Pull the git submodule that contains the assets (https://github.com/sparklinlabs/superpowers-asset-packs - CC0): `git submodule init && git submodule update`
3. Step into the MonoGameGumBatchScaling folder and build the project: `cd MonoGameGumBatchScaling && dotnet build`
4. Run the project: `dotnet run`

## Dependencies

- [MonoGame Extended](https://www.monogameextended.net/):ViewportAdapter and Camera modules are used to scale the game when the screen is resized.
- [Gum](https://docs.flatredball.com/gum/code/getting-started/setup/empty-project-before-adding-gum/monogame-kni-fna): Uses GumBatch to display GUI.

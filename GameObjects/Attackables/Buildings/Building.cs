using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class Building : Attackable
    {

        protected int gridX, gridY, tileWidth, tileHeight;
        protected float width, height;
        protected Rectangle bounds;
        protected Grid grid;

        public Building(GameplayManager gm, int gridX, int gridY, int faction, World world, int tileWidth, int tileHeight, int HP, float visionRange, Grid grid)
            : base(gm, new Vector2(((float)gridX + (float)tileWidth / 2.0f) * Grid.TileSize, ((float)gridY + (float)tileHeight / 2.0f) * Grid.TileSize), faction, HP, visionRange, world)
        {
            body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(tileWidth * Grid.TileSize), ConvertUnits.ToSimUnits(tileHeight * Grid.TileSize), 10, ConvertUnits.ToSimUnits(new Vector2(((float)gridX + (float)tileWidth / 2.0f) * Grid.TileSize, ((float)gridY + (float)tileHeight / 2.0f) * Grid.TileSize)));
            body.BodyType = BodyType.Static;
            body.CollisionCategories = Category.All;
            body.CollidesWith = Category.All;

            width = tileWidth * Grid.TileSize;
            height = tileHeight * Grid.TileSize;
            this.tileHeight = tileHeight;
            this.tileWidth = tileWidth;
            this.gridX = gridX;
            this.gridY = gridY;
            this.grid = grid;

            BlockTiles();
            grid.AssignNeighbours();

            radius = Math.Max(width / 2.0f, height / 2.0f);
            bounds = new Rectangle((int)(Position.X - width / 2.0f), (int)(Position.Y - height / 2.0f), (int)width, (int)height);
        }

        private void BlockTiles() {
            for (int i = gridX; i < gridX + tileWidth; i++)
            {
                for (int j = gridY; j < gridY + tileHeight; j++)
                {
                    grid.BlockTile(i, j);
                }
            }
        }

        private void UnblockTiles() {
            for (int i = gridX; i < gridX + tileWidth; i++)
            {
                for (int j = gridY; j < gridY + tileHeight; j++)
                {
                    grid.UnblockTile(i, j);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsDead())
            {
                UnblockTiles();
                grid.AssignNeighbours();
            }
                
        }

        public override void DealDamage(int dmg, float delay = 0.0f)
        {
            base.DealDamage(dmg, delay);
            if (IsDead())
            {
                UnblockTiles();
                grid.AssignNeighbours();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (selected)
                spriteBatch.Draw(AssetManager.GetTexture("pixel"), Position, null, Factions.GetFactionSelectionColor(faction), 0, new Vector2(0.5f, 0.5f), new Vector2(tileWidth * 1.1f * Grid.TileSize, tileHeight * 1.1f * Grid.TileSize), SpriteEffects.None, 0);
            spriteBatch.Draw(AssetManager.GetTexture("pixel"), Position, null, Factions.GetFactionColor(faction), 0, new Vector2(0.5f, 0.5f), new Vector2(tileWidth * Grid.TileSize, tileHeight * Grid.TileSize), SpriteEffects.None, 0);
            base.Draw(spriteBatch);
        }

        public override bool Contains(Vector2 point)
        {
            return bounds.Contains(point);
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class GameplayManager
    {
        GameWindow window;
        Grid grid;
        ArmyController armyController;
        List<GameObject> gameObjects = new List<GameObject>();
        List<UnitController> controllers = new List<UnitController>();
        List<ISelectable> selectables = new List<ISelectable>();

        bool startClick = true;
        Vector2 startPos, endPos;

        public GameplayManager(GameWindow window) {
            this.window = window;
            grid = new Grid();
            armyController = new PlayerController(this, 0);

            SpawnUnit(new Vector2(150, 150), 0);
            SpawnUnit(new Vector2(250, 150), 1);
            SpawnUnit(new Vector2(250, 150), 1);
            SpawnUnit(new Vector2(290, 150), 0);
            
        }

        public void SpawnUnit(Vector2 position, int faction) {
            Unit u = new Unit(position, faction);
            gameObjects.Add(u);
            controllers.Add(new UnitController(u));
            selectables.Add(u);
        }


        public void Update(GameTime gameTime) {
            foreach (GameObject go in gameObjects)
                go.Update(gameTime);
            foreach (UnitController c in controllers)
                c.Update(gameTime);

            HandleCollisions();

            if (KeyMouseReader.LeftClick() && grid.Bounds.Contains(KeyMouseReader.mouseState.Position))
            {
                if (startClick)
                {
                    startClick = false;
                    startPos = KeyMouseReader.mouseState.Position.ToVector2();
                }
                else {
                    endPos = KeyMouseReader.mouseState.Position.ToVector2();
                    grid.FindPath(startPos, endPos);
                    startClick = true;

                }
            }

            armyController.Update(gameTime);
        }

        private void HandleCollisions()
        {
            for (int i = 0; i < 10; ++i)
            {
                foreach (GameObject go in gameObjects)
                {
                    if (go is Unit)
                    {
                        CollisionResponse cr = grid.CollisionCheck(go as Unit);
                        if (cr.collided)
                        {
                            go.Position += cr.normal * cr.penetrationDepth;
                            go.RecalculateBounds();
                        }
                    }

                }
                foreach (GameObject go in gameObjects)
                {
                    if (go is Unit)
                    {
                        foreach (GameObject ogo in gameObjects)
                        {
                            if (ogo is Unit && ogo != go)
                            {
                                CollisionResponse cr = CollisionDetection.CollisionCheck((go as Unit).boundingCircle, (ogo as Unit).boundingCircle);
                                if (cr.collided)
                                {
                                    go.Position += cr.normal * cr.penetrationDepth;
                                    go.RecalculateBounds();
                                }
                            }

                        }
                    }
                }
            }
        }

        public ISelectable ClickSelect(Vector2 location) {
            ISelectable result = null;
            foreach (ISelectable s in selectables) {
                if (s.GetBounds().Contains(new Point((int)location.X, (int)location.Y)) && result == null)
                {
                    result = s;
                    s.Select();
                }
                else {
                    s.Deselect();
                }
            }
            return result;
        }

        public List<ISelectable> BoxSelect(Rectangle box) {
            List<ISelectable> selected = new List<ISelectable>();
            foreach (ISelectable s in selectables)
            {
                if (box.Contains(s.GetSelectionPoint()))
                {
                    s.Select();
                    selected.Add(s);
                }
                else {
                    s.Deselect();
                }
            }
            return selected;
        }

        public Path GetPath(Vector2 a, Vector2 b) {
            return grid.FindPath(a, b);
        }

        public void Draw(SpriteBatch spriteBatch) {
            grid.Draw(spriteBatch);
            foreach (GameObject go in gameObjects)
                go.Draw(spriteBatch);
            foreach (UnitController uc in controllers)
            {
                if(uc.pathToFollow != null)
                    for (int i = 1; i < uc.pathToFollow.PointCount(); i++)
                        DebugDraw.DrawLine(spriteBatch, uc.pathToFollow.GetPoint(i - 1), uc.pathToFollow.GetPoint(i));
            }
            DebugDraw.DrawRectangle(spriteBatch, (armyController as PlayerController).SelectionBox);
        }

    }
}

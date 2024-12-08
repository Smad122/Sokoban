using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Diagnostics;

namespace Sokoban
{
    public class SokobanGame : Game
    {
        private static int scale = 50;
        private static int playerScale = scale / 25;

        private string playerName;
        private string path;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Stopwatch sw = new Stopwatch();
        private int currentTime = 0;
        private int period = 60;

        private List<ICreature>[] creatures;
        private Map map;


        private int activeButtonsCounter = 0;

        private enum Elements
        {
            Wall, Button, Player, Container
        }

        public SokobanGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            creatures = new List<ICreature>[4];
            for (int i = 0; i < creatures.Length; i++)
                creatures[i] = new List<ICreature>();

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = map.Size.X * scale;
            _graphics.PreferredBackBufferHeight = map.Size.Y * scale;
            _graphics.ApplyChanges();

            InitCreatures();
            sw.Restart();

            base.Initialize();
        }

        private void InitCreatures()
        {
            for (int i = 0; i < map.Game.Length; i++)
            {
                var line = map.Game[i];
                for (int j = 0; j < line.Length; j++)
                {
                    var item = line[j];
                    var position = new Vector2(j * scale, i * scale);
                    switch (item)
                    {
                        case 'C':
                            creatures[(int)Elements.Container].Add(new Container(position));
                            break;
                        case 'P':
                            creatures[(int)Elements.Player].Add(new Player(position));
                            break;
                        case 'B':
                            creatures[(int)Elements.Button].Add(new Key(position));
                            break;
                        case 'W':
                            creatures[(int)Elements.Wall].Add(new Wall(position));
                            break;
                    }
                }
            }
        }

        public void LoadPlayerName(string name) => playerName = name;

        public void LoadLevel(string levelPath)
        {
            path = levelPath;
            map = new Map(levelPath);
            Initialize();
        }

        protected override void LoadContent()//загрузка текстур
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Player.Textures = new Texture2D[5];
            Player.Textures[(int)Player.Movement.Stay] = Content.Load<Texture2D>("Stay");
            Player.Textures[(int)Player.Movement.Left] = Content.Load<Texture2D>("L_Walk");
            Player.Textures[(int)Player.Movement.Right] = Content.Load<Texture2D>("R_Walk");
            Player.Textures[(int)Player.Movement.Up] = Content.Load<Texture2D>("U_Walk");
            Player.Textures[(int)Player.Movement.Down] = Content.Load<Texture2D>("D_Walk");

            Wall.SharedTexture = Content.Load<Texture2D>("Wall");
            Container.SharedTexture = Content.Load<Texture2D>("Container");
            Key.SharedTexture = Content.Load<Texture2D>("Button");
        }

        private Vector2 UpdateInput(KeyboardState keyboard, Vector2 position) //ввод с клавиатуры
        {
            var keys = keyboard.GetPressedKeys();
            if (keys.Length == 0)
            {
                Player.CurrentMove = Player.Movement.Stay;
                return position;
            }

            var moveDirection = Vector2.Zero;
            switch (keys[0])
            {
                case Keys.Left:
                    moveDirection = new Vector2(-playerScale, 0);
                    Player.CurrentMove = Player.Movement.Left;
                    break;
                case Keys.Right:
                    moveDirection = new Vector2(playerScale, 0);
                    Player.CurrentMove = Player.Movement.Right;
                    break;
                case Keys.Up:
                    moveDirection = new Vector2(0, -playerScale);
                    Player.CurrentMove = Player.Movement.Up;
                    break;
                case Keys.Down:
                    moveDirection = new Vector2(0, playerScale);
                    Player.CurrentMove = Player.Movement.Down;
                    break;
                default:
                    Player.CurrentMove = Player.Movement.Stay;
                    break;
            }

            var newPos = position + moveDirection;
            if (!CollidingWithContainer(position, moveDirection) || IsCollidingWithWall(newPos, creatures[(int)Elements.Player][0].GetSize()))
                return position;

            return newPos;
        }

        private bool CollidingWithContainer(Vector2 playerPos, Vector2 moveDirection) //проверка на столкновения с контейннером
        {
            foreach (var container in creatures[(int)Elements.Container])
            {
                if (IsTouching(container.GetPosition(), container.GetSize(), playerPos + moveDirection, creatures[(int)Elements.Player][0].GetSize()))
                {
                    Vector2 newContainerPos = container.GetPosition() + moveDirection;

                    if (!IsCollidingWithWall(newContainerPos, container.GetSize()) && !IsCollidingWithOtherContainers(newContainerPos, container))
                    {
                        container.SetPosition(newContainerPos);

                        foreach (var button in creatures[(int)Elements.Button])
                        {
                            if (IsTouching(newContainerPos, container.GetSize(), button.GetPosition(), button.GetSize()) && button is IActivatable activatable)
                                activatable.Activation = true;
                        }

                        return true;
                    }

                    return false;
                }
            }
            return true;
        }

        private bool IsTouching(Vector2 rect1Pos, Point rect1Size, Vector2 rect2Pos, Point rect2Size)//проверка столкновения объектов
        {
            return rect1Pos.X < rect2Pos.X + rect2Size.X &&
                   rect1Pos.X + rect1Size.X > rect2Pos.X &&
                   rect1Pos.Y < rect2Pos.Y + rect2Size.Y &&
                   rect1Pos.Y + rect1Size.Y > rect2Pos.Y;
        }

        private bool IsCollidingWithOtherContainers(Vector2 position, ICreature currentContainer) //столкновения контейнера с другими контейнерами
        {
            foreach (var container in creatures[(int)Elements.Container])
            {
                if (container == currentContainer)
                    continue;

                if (IsTouching(container.GetPosition(), container.GetSize(), position, container.GetSize()))
                    return true;
            }
            return false;
        }

        private bool IsCollidingWithWall(Vector2 position, Point size) //отдельная проверка на столкновения со стенами через исходную карту
        {
            var centerX = (int)(position.X + size.X / 2);
            var centerY = (int)(position.Y + size.Y / 2);

            var pointsToCheck = new[]
            {
                new Vector2(position.X, position.Y),
                new Vector2(position.X + size.X - 1, position.Y),
                new Vector2(position.X, position.Y + size.Y - 1),
                new Vector2(position.X + size.X - 1, position.Y + size.Y - 1),
                new Vector2(centerX, centerY)
            };

            foreach (var point in pointsToCheck)
            {
                var col = (int)point.X / scale;
                var row = (int)point.Y / scale;

                if (map.Game[row][col] == 'W')
                    return true;
            }

            return false;
        }

        protected override void Update(GameTime gameTime) //Обновление состояния игры
        {
            KeyboardState keyboard = Keyboard.GetState();
            currentTime += gameTime.ElapsedGameTime.Milliseconds;

            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape)))
                Exit();

            var player = (Player)creatures[(int)Elements.Player][0];

            if (keyboard.GetPressedKeyCount() > 0)
                player.SetPosition(UpdateInput(keyboard, player.GetPosition()));
            else
                Player.CurrentMove = Player.Movement.Stay;

            EndGameChecker();

            if (currentTime > period)
                UpdateSprite(player);

            base.Update(gameTime);
        }

        private void EndGameChecker() //Проверка состояния ящиков
        {
            activeButtonsCounter = 0;
            foreach (var button in creatures[(int)Elements.Button])
            {
                if (button is IActivatable activatable && (activatable.Activation))
                {
                    var counter = 0;
                    foreach (var container in creatures[(int)Elements.Container])
                        if (IsTouching(container.GetPosition(), container.GetSize(), button.GetPosition(), button.GetSize()))
                            counter++;

                    if (counter == 0)
                        activatable.Activation = false;
                    else activeButtonsCounter++;
                }
            }

            if (activeButtonsCounter == creatures[(int)Elements.Button].Count) //конец игры
                EndGame();
        }

        private void EndGame()
        {
            sw.Stop();
            File.AppendAllText("Leaderboard.txt", path + " " + playerName + " " + sw.ToString() + Environment.NewLine);
            Exit();
        }

        private void UpdateSprite(Player player) // обновление спрайта персонажа по таймеру
        {
            currentTime -= period;
            var frame = player.CurrentFrame;
            ++frame.X;
            if (frame.X >= player.SpriteSize.X)
                frame.X = 0;
            player.CurrentFrame = frame;
        }

        protected override void Draw(GameTime gameTime) //отрисовка
        {
            GraphicsDevice.Clear(Color.BurlyWood);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            var i = 0;

            foreach (var classes in creatures)
            {
                foreach (var element in classes)
                {
                    if (i == (int)Elements.Player) //отдельная отрисовка игрока, т.к. нужно выводить верное расположение окна для отрисовки анимации 
                    {
                        var player = (Player)creatures[(int)Elements.Player][0];
                        _spriteBatch.Draw(Player.Textures[(int)Player.CurrentMove], player.GetPosition(),
                        new Rectangle(player.CurrentFrame.X * player.FrameWidth,
                        player.CurrentFrame.Y * player.FrameHeight,
                        player.FrameWidth, player.FrameHeight),
                        Color.White);
                    }

                    else
                        _spriteBatch.Draw(element.Texture, element.GetPosition(), Color.White);

                }
                i++;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
using Raylib_cs;
using System.Numerics;

class Program
{
    public static void Main()
    {
        int sizex = 1300;
        int sizey = 800;
        int txtsize = 30;
        float playerSpeed = 4.5f;
        float speedIncrease = 1.1f;
        Raylib.InitWindow(sizex, sizey, "title");
        Raylib.SetTargetFPS(60);
        Context context = new Context(sizex, sizey);

        while (!Raylib.WindowShouldClose())
        {
            HandleInputs(context, playerSpeed);
            if (context.playing) ProcessGame(context, speedIncrease);
            DrawContext(context, txtsize);
        }

        Raylib.CloseWindow();
    }

    public static void HandleInputs(Context context, float playerSpeed) // QWERTY
    {
        if (context.playing) // GAME INPUTS
        {
            if (Raylib.IsKeyDown(KeyboardKey.S) && !Raylib.IsKeyDown(KeyboardKey.X))
            {
                // P1 LEFT
                if (context.player1.y - playerSpeed > 0)
                {
                    context.player1.y -= playerSpeed;
                }
            }
            else if (Raylib.IsKeyDown(KeyboardKey.X) && !Raylib.IsKeyDown(KeyboardKey.S))
            {
                // P1 RIGHT
                if (context.player1.y + playerSpeed + context.player1.sizey < context.sizey)
                {
                    context.player1.y += playerSpeed;
                }
            }

            if (Raylib.IsKeyDown(KeyboardKey.K) && !Raylib.IsKeyDown(KeyboardKey.O))
            {
                // P2 LEFT
                if (context.player2.y + playerSpeed + context.player1.sizey < context.sizey)
                {
                    context.player2.y += playerSpeed;
                }
            }
            else if (Raylib.IsKeyDown(KeyboardKey.O) && !Raylib.IsKeyDown(KeyboardKey.K))
            {
                // P2 RIGHT
                if (context.player2.y - playerSpeed > 0)
                {
                    context.player2.y -= playerSpeed;
                }
            }

        }
        else // MENU INPUTS
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                // Start game
                context.playing = true;
                context.score = 0;
            }
        }
    }

    public static void ProcessGame(Context context, float speedIncrease)
    {
        // COLLIDE ball
        // w players
        if (context.ball.Collide(context.player1))
        {
            // bounce right
            context.ball.x = context.player1.x + context.player1.sizex;
            context.ball.speed.X = -context.ball.speed.X;

            context.ball.speed = Vector2.Multiply(context.ball.speed, speedIncrease);
            context.score++;
        }
        else if (context.ball.Collide(context.player2))
        {
            // bounce left
            context.ball.x = context.player2.x - context.ball.sizex;
            context.ball.speed.X = -context.ball.speed.X;

            context.ball.speed = Vector2.Multiply(context.ball.speed, speedIncrease);
            context.score++;
        }

        // w walls
        if (context.ball.x <= 0)
        {
            // Game over P2 win
            context.playing = false;
            context.winner = 2;
            context.Reset();
        }
        else if (context.ball.x + context.ball.sizex >= context.sizex)
        {
            // Game over P1 win
            context.playing = false;
            context.winner = 1;
            context.Reset();
        }

        if (context.ball.y <= 0)
        {
            // bounce down
            context.ball.y = 0;
            context.ball.speed.Y = -context.ball.speed.Y;
        }
        else if (context.ball.y + context.ball.sizey >= context.sizey)
        {
            // bounce up
            context.ball.y = context.sizey - context.ball.sizey;
            context.ball.speed.Y = -context.ball.speed.Y;
        }

        // MOVE ball
        context.ball.x += context.ball.speed.X;
        context.ball.y += context.ball.speed.Y;
    }

    public static void DrawContext(Context context, int txtsize)
    {
        bool firstStart = context.score == -1;
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        if (!context.playing) // game over/not started overlay
        {
            Raylib.DrawText(
                "move with S+X and K+O",
                context.sizex - 250,
                context.sizey - 60,
                20,
                Color.White
            );
            Raylib.DrawText(
                "click to play",
                context.sizex - 140,
                context.sizey - 30,
                20,
                Color.White
            );

            if (firstStart)
            {
                Raylib.DrawText(
                    "Plopong",
                    context.sizex / 2 - (int)Math.Round(txtsize * 2 * 2.5),
                    70,
                    txtsize * 2,
                    Color.White
                );
            }
            else
            {
                Raylib.DrawText(
                    "game over",
                    context.sizex / 2 - (int)Math.Round(txtsize * 2.5),
                    50,
                    txtsize,
                    Color.White
                );
                Raylib.DrawText(
                    "score was " + context.score,
                    (int)Math.Round(context.sizex / 8f),
                    (int)Math.Round(2f * context.sizey / 3f),
                    txtsize,
                    Color.White
                );
                if (context.winner == 1) {
                    Raylib.DrawText(
                        "P1 wins !",
                        (int)Math.Round(context.sizex *0.1),
                        (int)Math.Round(1f * context.sizey / 3f),
                        txtsize,
                        Color.White
                    );
                } else {
                    Raylib.DrawText(
                        "P2 wins !",
                        (int)Math.Round(context.sizex *0.8),
                        (int)Math.Round(1f * context.sizey / 3f),
                        txtsize,
                        Color.White
                    );
                }
            }
        }
        else
        {
            // score
            Raylib.DrawText(
                "score - " + context.score,
                context.sizex / 2 - txtsize,
                20,
                (int)Math.Round(txtsize * 0.6),
                Color.Gray
            );
        }

        // p1
        Raylib.DrawRectangle(
            (int)Math.Round(context.player1.x),
            (int)Math.Round(context.player1.y),
            context.player1.sizex,
            context.player1.sizey,
            Color.White
        );
        // p2
        Raylib.DrawRectangle(
            (int)Math.Round(context.player2.x),
            (int)Math.Round(context.player2.y),
            context.player2.sizex,
            context.player2.sizey,
            Color.White
        );
        // ball
        Raylib.DrawRectangle(
            (int)Math.Round(context.ball.x),
            (int)Math.Round(context.ball.y),
            context.ball.sizex,
            context.ball.sizey,
            Color.Red
        );

        Raylib.EndDrawing();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlappyBird
{
    class Component
    {
        StackPanel stackPanel;
        MainWindow window;
        Canvas canvas;
        public GamePause gp;
        public DispatcherTimer timer = new DispatcherTimer();
        public Rectangle birdRec;
        public Point bird = new Point();
        public enum Direction { UP, DOWN, LEFT };
        Direction direction;
        public Rectangle Tube1UP = new Rectangle();
        public Rectangle Tube2UP = new Rectangle();
        public Rectangle Tube3UP = new Rectangle();
        public Point p1UP = new Point();
        public Point p2UP = new Point();
        public Point p3UP = new Point();
        public Rectangle Tube1DOWN = new Rectangle();
        public Rectangle Tube2DOWN = new Rectangle();
        public Rectangle Tube3DOWN = new Rectangle();
        public Point p1DOWN = new Point();
        public Point p2DOWN = new Point();
        public Point p3DOWN = new Point();
        public Random random = new Random((int)DateTime.Now.Ticks);
        public Random random1 = new Random((int)DateTime.Now.Millisecond);
        public bool POINTS = false;
        public bool Death = false;
        public int counter;
        public Component(Window w, Canvas c)
        {
            canvas = c;
            window = (MainWindow)w;
            birdRec = new Rectangle();
            BitmapImage bitmapImage = new BitmapImage(new Uri("flappy_bird_png_505775.png", UriKind.Relative));
            ImageBrush imageBrush = new ImageBrush(bitmapImage);
            BitmapImage Tube = new BitmapImage(new Uri("pipe (1).png", UriKind.Relative));
            ImageBrush tubec = new ImageBrush(Tube);
            BitmapImage tube1 = new BitmapImage(new Uri("pipe2.png", UriKind.Relative));
            ImageBrush tubec1 = new ImageBrush(tube1);
            birdRec.Fill = imageBrush;
            birdRec.Width = 68;
            birdRec.Height = 48;
            canvas.Children.Add(birdRec);
            Canvas.SetLeft(birdRec, 30);
            Canvas.SetBottom(birdRec, 100);

            Tube1UP = new Rectangle();
            Tube1UP.Width = 68;
            Tube1UP.Height = random.Next(50, 125);
            Tube1UP.Fill = tubec;
            canvas.Children.Add(Tube1UP);
            p1UP.X = 400;
            p1UP.Y = 0;
            Canvas.SetRight(Tube1UP, p1UP.X);
            Canvas.SetBottom(Tube1UP, p1UP.Y);

            Tube2UP = new Rectangle();
            Tube2UP.Width = 68;
            Tube2UP.Height = random1.Next(126, 200);
            Tube2UP.Fill = tubec;
            canvas.Children.Add(Tube2UP);
            p2UP.X = 618;
            p2UP.Y = 0;
            Canvas.SetRight(Tube2UP, p2UP.X);
            Canvas.SetBottom(Tube2UP, p2UP.Y);

            Tube3UP = new Rectangle();
            Tube3UP.Width = 68;
            Tube3UP.Height = random.Next(201, 275);
            Tube3UP.Fill = tubec;
            canvas.Children.Add(Tube3UP);
            p3UP.X = 836;
            p3UP.Y = 0;
            Canvas.SetRight(Tube3UP, p3UP.X);
            Canvas.SetBottom(Tube3UP, p3UP.Y);

            Tube1DOWN = new Rectangle();
            Tube1DOWN.Width = 68;
            Tube1DOWN.Height = random.Next(201, 275);
            Tube1DOWN.Fill = tubec1;
            canvas.Children.Add(Tube1DOWN);
            p1DOWN.X = 400;
            p1DOWN.Y = 500;
            Canvas.SetRight(Tube1DOWN, p1DOWN.X);
            Canvas.SetTop(Tube1DOWN, p1DOWN.Y);

            Tube2DOWN = new Rectangle();
            Tube2DOWN.Width = 68;
            Tube2DOWN.Height = random1.Next(126, 200);
            Tube2DOWN.Fill = tubec1;
            canvas.Children.Add(Tube2DOWN);
            p2DOWN.X = 618;
            p2DOWN.Y = 500;
            Canvas.SetRight(Tube2DOWN, p2DOWN.X);
            Canvas.SetTop(Tube2DOWN, p2DOWN.Y);

            Tube3DOWN = new Rectangle();
            Tube3DOWN.Width = 68;
            Tube3DOWN.Height = random.Next(50, 125);
            Tube3DOWN.Fill = tubec1;
            canvas.Children.Add(Tube3DOWN);
            p3DOWN.X = 836;
            p3DOWN.Y = 500;
            Canvas.SetRight(Tube3DOWN, p3DOWN.X);
            Canvas.SetTop(Tube3DOWN, p3DOWN.Y);

            timer.Interval = new TimeSpan(0, 0, 0, 0, 7);
            timer.Tick += update;
            timer.Start();
        }
        public void update(object sender, EventArgs e)
        {
            TubesMovement();
            BirdMovement();
            Collision();
            Points();
            int points = counter;
            Grid grid = (Grid)window.Content;
            foreach (Object w in grid.Children)
            {
                if (w.GetType().ToString().Contains("Label"))
                {
                    Label l = (Label)w;
                    l.Content = "\n        " + counter;
                }
            }
        }

        public void BirdMovement()
        {
            direction = Direction.DOWN;
            if (Keyboard.IsKeyDown(Key.Space))
            {
                direction = Direction.UP;
            }
            switch (direction)
            {
                case Direction.UP:
                    bird = new Point(30, bird.Y - 20 / 2);
                    break;
                case Direction.DOWN:
                    bird = new Point(30, bird.Y + 5 / 2);
                    break;
            }
            Canvas.SetLeft(birdRec, 30);
            Canvas.SetTop(birdRec, bird.Y);
        }
        public void TubesMovement()
        {
            direction = Direction.LEFT;
            switch (direction)
            {
                case Direction.LEFT:
                    p1UP = new Point(p1UP.X - 1, p1UP.Y);
                    p2UP = new Point(p2UP.X - 1, p2UP.Y);
                    p3UP = new Point(p3UP.X - 1, p3UP.Y);
                    p1DOWN = new Point(p1DOWN.X - 1, 500 - Tube1DOWN.Height);
                    p2DOWN = new Point(p2DOWN.X - 1, 500 - Tube2DOWN.Height);
                    p3DOWN = new Point(p3DOWN.X - 1, 500 - Tube3DOWN.Height);
                    break;
            }
            if (p1UP.X == -150)
            {
                canvas.Children.Remove(Tube1UP);
                canvas.Children.Add(Tube1UP);
                p1UP.X = 528;
                Tube1UP.Height = random.Next(50, 125);
                direction = Direction.LEFT;
            }
            if (p2UP.X == -150)
            {
                canvas.Children.Remove(Tube2UP);
                canvas.Children.Add(Tube2UP);
                p2UP.X = 528;
                Tube2UP.Height = random1.Next(126, 200);
                direction = Direction.LEFT;
            }
            if (p3UP.X == -150)
            {
                canvas.Children.Remove(Tube3UP);
                canvas.Children.Add(Tube3UP);
                p3UP.X = 528;
                Tube3UP.Height = random.Next(201, 275);
                direction = Direction.LEFT;
            }
            if (p1DOWN.X == -150)
            {
                canvas.Children.Remove(Tube1DOWN);
                canvas.Children.Add(Tube1DOWN);
                p1DOWN.X = 528;
                Tube1DOWN.Height = random.Next(201, 275);
                direction = Direction.LEFT;
            }
            if (p2DOWN.X == -150)
            {
                canvas.Children.Remove(Tube2DOWN);
                canvas.Children.Add(Tube2DOWN);
                p2DOWN.X = 528;
                Tube2DOWN.Height = random1.Next(126, 200);
                direction = Direction.LEFT;
            }
            if (p3DOWN.X == -150)
            {
                canvas.Children.Remove(Tube3DOWN);
                canvas.Children.Add(Tube3DOWN);
                p3DOWN.X = 528;
                Tube3DOWN.Height = random.Next(50, 125);
                direction = Direction.LEFT;
            }
            Canvas.SetLeft(Tube1UP, p1UP.X);
            Canvas.SetTop(Tube1UP, p1UP.Y);

            Canvas.SetLeft(Tube2UP, p2UP.X);
            Canvas.SetTop(Tube2UP, p2UP.Y);

            Canvas.SetLeft(Tube3UP, p3UP.X);
            Canvas.SetTop(Tube3UP, p3UP.Y);

            Canvas.SetLeft(Tube1DOWN, p1DOWN.X);
            Canvas.SetTop(Tube1DOWN, p1DOWN.Y);

            Canvas.SetLeft(Tube2DOWN, p2DOWN.X);
            Canvas.SetTop(Tube2DOWN, p2DOWN.Y);

            Canvas.SetLeft(Tube3DOWN, p3DOWN.X);
            Canvas.SetTop(Tube3DOWN, p3DOWN.Y);
        }
        public void Collision()
        {
            if (bird.Y <= Tube1UP.Height && bird.X + 68 >= p1UP.X && bird.X - 40 <= p1UP.X)
            {
                Death = true;
            }
            else if (bird.Y <= Tube2UP.Height && bird.X + 68 >= p2UP.X && bird.X - 40 <= p2UP.X)
            {
                Death = true;
            }
            else if (bird.Y <= Tube3UP.Height && bird.X + 68 >= p3UP.X && bird.X - 40 <= p3UP.X)
            {
                Death = true;
            }
            else if (bird.Y >= 452 - Tube1DOWN.Height && bird.X + 68 >= p1DOWN.X && bird.X - 40 <= p1DOWN.X)
            {
                Death = true;
            }
            else if (bird.Y >= 452 - Tube2DOWN.Height && bird.X + 68 >= p2UP.X && bird.X - 40 <= p2UP.X)
            {
                Death = true;
            }
            else if (bird.Y >= 452 - Tube3DOWN.Height && bird.X + 68 >= p3UP.X && bird.X - 40 <= p3UP.X)
            {
                Death = true;
            }
            else if (bird.Y >= 462)
            {
                Death = true;
            }
            if (Death == true)
            {
                timer.Stop();
                window.btnExit.Visibility = Visibility.Visible;
                window.btnRestart.Visibility = Visibility.Visible;
            }
        }
        public void Points()
        {
            if (bird.X == p1UP.X + 68 || bird.X == p2UP.X + 68 || bird.X == p3UP.X + 68)
            {
                POINTS = true;
            }
            if (POINTS == true)
            {
                counter++;
                POINTS = false;
            }
        }
    }
}
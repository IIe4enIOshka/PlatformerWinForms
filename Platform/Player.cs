using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Platform
{
    class Player : PictureBox
    {
        private Control _control;
        private Direction _currentDirection;
        private Direction _previousDirection;
        private List<List<Image>> _animations;
        private Timer _animationTimer;
        private Timer _runTimer;
        private Point _pointPlayer;
        private Rectangle _boxCollider;

        private int _currentFrame = 0;
        private int _currentAnimation = 1;
        private int _previousAnimation = 1;
        private int _currentY = 0;
        private int _previousY = 0;

        private int _counter;
        private int _directionX = 0;
        private int _directionY = 0;
        private int _speed = 3;
        private int _gravity = 4;
        private bool _isJump = false;

        private int _jumpHeight = 150;
        private int _targetY;

        private enum Direction
        {
            Left,
            Right
        }

        public Player(Control control)
        {
            _control = control;
            Init();
        }

        private void Init()
        {
            _pointPlayer = new Point(100, 100);
            _boxCollider = new Rectangle();
            _animations = new List<List<Image>>();
            _animationTimer = new Timer();
            _runTimer = new Timer();

            _animationTimer.Interval = 50;
            _runTimer.Interval = 10;

            _animationTimer.Tick += new EventHandler(Animation_Tick);
            _runTimer.Tick += new EventHandler(Game_tick);

            _animationTimer.Start();
            _runTimer.Start();

            _currentDirection = Direction.Right;
            _previousDirection = _currentDirection;
        }

        private void ChangeAnimation(int currentAnimation)
        {
            if (currentAnimation != _currentAnimation)
            {
                _currentFrame = 0;
                _previousAnimation = _currentAnimation;
                _currentAnimation = currentAnimation;
            }
        }

        private void ChangeDirection(Direction direction)
        {
            if (_currentDirection != direction)
            {
                _previousDirection = _currentDirection;
                _currentDirection = direction;
                Flip();
            }
        }

        public void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.A)
            {
                _directionX = 0;
            }
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.A)
            {
                PlayAnimation();
            }

            if (e.KeyCode == Keys.D)
            {
                _directionX = 1;
                ChangeDirection(Direction.Right);
            }

            if (e.KeyCode == Keys.A)
            {
                _directionX = -1;
                ChangeDirection(Direction.Left);
            }

            if (e.KeyCode == Keys.Space && _isJump == false && CheckGround())
            {
                _isJump = true;
                _targetY = _pointPlayer.Y - _jumpHeight;
                _directionY = -1;
            }
        }

        private void PlayAnimation()
        {
            if (_isJump == false && CheckGround() == false && CheckFall() == true)
                ChangeAnimation(Animation.Fall);

            if (_isJump == true)
                ChangeAnimation(Animation.Jump);

            if (CheckGround() && _directionX == 0 && _isJump == false)
                ChangeAnimation(Animation.Idle);

            if (_directionX != 0 && _isJump == false && CheckFall() == false)
                ChangeAnimation(Animation.Run);
        }

        private bool CheckFall()
        {
            return _previousY < _currentY;
        }

        private bool CheckGround()
        {
            foreach (Control item in _control.Controls)
            {
                if (item != this)
                {
                    if (_boxCollider.IntersectsWith(item.Bounds))
                    {
                        Rectangle intersectionArea = Rectangle.Intersect(_boxCollider, item.Bounds);

                        if (intersectionArea.Height < this.Height / 2 && _isJump == false)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void Game_tick(object sender, EventArgs e)
        {
            PlayAnimation();
            CheckCollisions();
            MovePlayer();
        }

        private void Animation_Tick(object sender, EventArgs e)
        {
            this.Location = _pointPlayer;
            CalculationColider();

            if (_counter < _animations[_currentAnimation].Count)
                _counter++;

            _currentFrame++;

            if (_currentFrame >= _animations[_currentAnimation].Count)
                _currentFrame = 0;
        }

        private void MovePlayer()
        {
            _pointPlayer.X += _directionX * _speed;
            _pointPlayer.Y += _directionY * _gravity;

            _previousY = _currentY;
            _currentY = _pointPlayer.Y;

            if (_pointPlayer.Y < _targetY)
            {
                _isJump = false;
            }

            if (_isJump == false && CheckGround() == false)
            {
                _directionY = 1;
            }
        }

        private void CheckCollisions()
        {
            foreach (Control item in _control.Controls)
            {
                if (item != this)
                {
                    if (_boxCollider.IntersectsWith(item.Bounds))
                    {
                        Rectangle intersectionArea = Rectangle.Intersect(_boxCollider, item.Bounds);

                        if (intersectionArea.Width > this.Width / 2)
                        {
                            CheckCollisionUp(intersectionArea, item);
                            CheckCollisionDown(intersectionArea, item);
                        }

                        if (intersectionArea.Height >= this.Height / 2)
                        {
                            CheckCollisionLeft(intersectionArea, item);
                            CheckCollisionRight(intersectionArea, item);
                        }
                    }
                }
            }
        }

        private void CheckCollisionUp(Rectangle intersectionArea, Control item)
        {
            if (intersectionArea.Top > _boxCollider.Top && _isJump == false)
            {
                _pointPlayer.Y = item.Top - this.Height / 2 + _gravity;
                _directionY = 0;
            }
        }

        private void CheckCollisionDown(Rectangle intersectionArea, Control item)
        {
            if (intersectionArea.Top <= _boxCollider.Top)
            {
                _pointPlayer.Y = item.Bottom + this.Height / 2 + 10;
                _targetY = _pointPlayer.Y;
            }
        }

        private void CheckCollisionRight(Rectangle intersectionArea, Control item)
        {
            if (intersectionArea.Left > _boxCollider.Left && _directionX > 0)
                _pointPlayer.X = item.Left - _boxCollider.Width / 2 + _speed;
        }

        private void CheckCollisionLeft(Rectangle intersectionArea, Control item)
        {
            if (intersectionArea.Left <= _boxCollider.Left && _directionX < 0)
                _pointPlayer.X = item.Left + item.Width + _boxCollider.Width / 2 - _speed;
        }

        private void CalculationColider()
        {
            int currentPointX = 0;
            int currentPointY = 0;

            this.Size = new Size(_animations[_currentAnimation][_currentFrame].Width, _animations[_currentAnimation][_currentFrame].Height);

            currentPointX = this.Size.Width / 2;
            currentPointY = this.Size.Height / 2;

            this.Left = _pointPlayer.X - currentPointX;
            this.Top = _pointPlayer.Y - currentPointY;

            this.Image = _animations[_currentAnimation][_currentFrame];

            _boxCollider.Size = new Size(75, this.Height + 20);
            _boxCollider.Location = new Point(this.Left + this.Width / 2 - _boxCollider.Width / 2, this.Top);
        }

        public void AddImageAnimation(int numberAnimation, Image image)
        {
            _animations[numberAnimation].Add(image);
        }

        public void AddAnimation()
        {
            _animations.Add(new List<Image>());
        }

        private void Flip()
        {
            for (int j = 0; j < _animations.Count; j++)
            {
                for (int i = 0; i < _animations[j].Count; i++)
                {
                    Bitmap bmp = (Bitmap)_animations[j][i];
                    bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    _animations[j][i] = bmp;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform
{
    public partial class Form1 : Form
    {
        private Player _player;


        public Form1()
        {
            InitializeComponent();

            _player = new Player(this);

            _player.AddAnimation();
            _player.AddAnimation();
            _player.AddAnimation();
            _player.AddAnimation();

            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_1);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_2);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_3);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_4);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_5);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_6);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_7);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_8);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_9);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_10);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_11);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_12);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_13);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_14);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_15);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_16);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_17);
            _player.AddImageAnimation(Animation.Run, Properties.Resources.run_18);

            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_1);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_2);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_3);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_4);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_5);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_6);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_7);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_8);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_9);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_10);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_11);
            _player.AddImageAnimation(Animation.Idle, Properties.Resources.idle_12);

            _player.AddImageAnimation(Animation.Jump, Properties.Resources.jump_1);

            _player.AddImageAnimation(Animation.Fall, Properties.Resources.fall_1);

            this.Controls.Add(_player);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _player.KeyDown(sender, e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            _player.KeyUp(sender, e);
        }
    }
}

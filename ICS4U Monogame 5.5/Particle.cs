using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ICS4U_Monogame_5._5
{
    public class Particle
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _rect;
        private Vector2 _velocity;
        private float _angle;
        private float _angularVelocity;
        private Color _color;
        private int _size;
        private int _TTL;
        private int _initialTTL;
        private float _opacity;
        private bool _fadeOut;
        private bool _doGrow;
        private bool _doShrink;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, Color color, int size, int TTL, bool fadeOut, bool doGrow, bool doShrink)
        {
            _texture = texture;
            _position = position;
            _rect = new Rectangle(0, 0, size, size);
            _velocity = velocity;
            _angle = angle;
            _angularVelocity = angularVelocity;
            _color = color;
            _size = size;
            _TTL = TTL;
            _fadeOut = fadeOut;
            _doGrow = doGrow;
            _doShrink = doShrink;
            _opacity = 1f;
            _initialTTL = TTL;
        }

        public bool Dead
        {
            get 
            { 
                return _TTL <= 0; 
            }
        }

        public void Update()
        {
            _position += _velocity;
            _angle += _angularVelocity;
            _rect.Location = _position.ToPoint();
            _TTL--;

            if (_fadeOut)
            {
                _opacity = _TTL / (float)_initialTTL;
            }

            if (_doGrow)
            {
                for (int i = 0; i < _opacity; i++)
                    _size += 1;
            }

            if (_doGrow)
            {
                for (int i = 0; i < _opacity; i++)
                    _size -= 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            spriteBatch.Draw(_texture, _rect, null, _color * _opacity, _angle, origin, SpriteEffects.None, 0f);

        }
    }
}

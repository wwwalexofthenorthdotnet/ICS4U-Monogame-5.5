using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ICS4U_Monogame_5._5
{
    public class ParticleEngine
{
        private Random _generator;
        private Vector2 _emitterLocation { get; set; }
        private List<Particle> _particles;
        private List<Texture2D> _textures;
        private bool _enabled;

        private int _TTL;

        private float _speed;

        private bool _randomizeColor;
        private Color _color;

        private float _angle;
        private float _angularVelocity;

        private bool _fadeOut;
        private int _opacity;

        private int _maxSize;
        private int _minSize;

        private bool _doGrow;
        private bool _doShrink;

        private bool _doRect;

        private bool _doDirection;
        private Rectangle _spawnRect;

        private Vector2 _direction;

        private float _spreadAngle;

        private bool _doGravity;

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public Vector2 EmitterLocation
        {
            get { return _emitterLocation; }
            set { _emitterLocation = value; }
        }

        public float Speed { get { return _speed; } set { _speed = value; } }
        public bool RandomizeColor { get { return _randomizeColor; } set { _randomizeColor = value; } }
        public bool FadeOut { get { return _fadeOut; } set { _fadeOut = value; } }
        public bool Grow { get { return _doGrow; } set { _doGrow = value; } }
        public bool Shrink { get { return _doShrink; } set { _doShrink = value; } }
        public Color Color { get { return _color; } set { _color = value; } }
        public float Angle { get { return _angle; } set { _angle = value; } }
        public float AngularVelocity { get { return _angularVelocity; } set { _angularVelocity = value; } }
        public int TTL { get { return _TTL; } set { _TTL = value; } }
        public int Opacity { get { return _opacity; } set { _opacity = value; } }
        public int MaxSize { get { return _maxSize; } set { _maxSize = value; } }
        public int MinSize { get { return _minSize; } set { _minSize = value; } }

        public Rectangle SpawnRect { get { return _spawnRect; } set { _spawnRect = value; } }

        public bool DoRect { get { return _doRect; } set { _doRect = value; } }
        public bool DoDirection { get { return _doDirection; } set { _doDirection = value; } }
        public bool DoGravity { get { return _doGravity; } set { _doGravity = value; } }

        public Vector2 Direction { get { return _direction; } set { _direction = value; } }
        public float SpreadAngle { get { return _spreadAngle; } set { _spreadAngle = value; } }

        public ParticleEngine(List<Texture2D> textures, Vector2 location, bool enabled)
        {
            _emitterLocation = location;
            _textures = textures;
            _particles = new List<Particle>();
            _generator = new Random();
            _enabled = enabled;
            _speed = 1f;

            _randomizeColor = false;
            _color = Color.White;
            _TTL = 40;
            _fadeOut = false;

            _doGrow = false;
            _doShrink = false;
            _doRect = false;

            _maxSize = 10;
            _minSize = 5;

            _angularVelocity = 1f;

            _spreadAngle = 30f;

            _direction = new Vector2(0, -1);

            _spawnRect = new Rectangle();
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = _textures[_generator.Next(_textures.Count)];
            Vector2 position = _emitterLocation;
            Vector2 velocity = new Vector2(0, 0);
            float gravity = 1;
            if (!_doDirection)
                velocity = new Vector2(_speed * (float)(_generator.NextDouble() * 2 - 1), _speed * (float)(_generator.NextDouble() * 2 - 1));
            else if (_doDirection)
            {
                float baseAngleRadians = (float)Math.Atan2(_direction.Y, _direction.X);
                float spreadRadians = (float)((_generator.NextDouble() * _spreadAngle - _spreadAngle / 2.0) * (Math.PI / 180.0));
                velocity = new Vector2(_speed * (float)Math.Cos(baseAngleRadians + spreadRadians), _speed * (float)Math.Sin((float)baseAngleRadians + spreadRadians));
            }
            

            if  (DoGravity)
            { 
                velocity = Vector2.Add(velocity, new Vector2(0, gravity));
            }
            
            float angle = _angle;
            //float angularVelocity = 1f * (float)(_generator.NextDouble() * 2 - 1);
            float angularVelocity = 1f * _angularVelocity;
            Color color = _color;
            if (_randomizeColor)
            {
                color = new Color((float)_generator.NextDouble(), (float)_generator.NextDouble(), (float)_generator.NextDouble());
            }

            bool fadeOut = _fadeOut;
            int size = _generator.Next(_minSize, _maxSize);
            int TTL = 20 + _generator.Next(_TTL);

            bool doGrow = _doGrow;
            bool doShrink = _doShrink;

            Rectangle spawnRect = _spawnRect;

            if (DoRect)
            {
                position = new Vector2(_generator.Next(0, _spawnRect.Width), _generator.Next(0, _spawnRect.Height));
            }


            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, TTL, fadeOut, doGrow, doShrink);
        }

        public void Update()
        {
            if (_enabled)
            {
                int total = 10;
                for (int i = 0; i < total; i++)
                {
                    _particles.Add(GenerateNewParticle());
                }
            }
                for (int i = 0; i < _particles.Count; i++)
                {
                    _particles[i].Update();
                    if (_particles[i].Dead)
                    {
                        _particles.RemoveAt(i);
                        i--;
                    }
                }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in _particles)
            {
                particle.Draw(spriteBatch);
            }
        }

}
}

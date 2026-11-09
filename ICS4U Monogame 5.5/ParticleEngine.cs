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

        private float _speed;

        private bool _randomizeColor;
        private Color _color;

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
        public Color color { get { return _color; } set { _color = value; } }

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
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = _textures[_generator.Next(_textures.Count)];
            Vector2 position = _emitterLocation;
            Vector2 velocity = new Vector2(_speed * (float)(_generator.NextDouble() * 2 - 1), _speed * (float)(_generator.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(_generator.NextDouble() * 2 - 1);
            Color color = _color;
            if (_randomizeColor)
            {
                color = new Color((float)_generator.NextDouble(), (float)_generator.NextDouble(), (float)_generator.NextDouble());
            }
            int size = _generator.Next(5, 10);
            int TTL = 20 + _generator.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, TTL);
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

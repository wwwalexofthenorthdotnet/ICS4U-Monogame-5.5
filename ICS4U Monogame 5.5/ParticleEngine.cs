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

        public ParticleEngine(List<Texture2D> textures, Vector2 location, bool enabled)
        {
            _emitterLocation = location;
            _textures = textures;
            _particles = new List<Particle>();
            _generator = new Random();
            _enabled = enabled;
        }

        private Particle GenerateNewParticle()
        {
            Texture2D texture = _textures[_generator.Next(_textures.Count)];
            Vector2 position = _emitterLocation;
            Vector2 velocity = new Vector2(1f * (float)(_generator.NextDouble() * 2 - 1), 1f * (float)(_generator.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(_generator.NextDouble() * 2 - 1);
            Color color = new Color((float)_generator.NextDouble(), (float)_generator.NextDouble(), (float)_generator.NextDouble());

            int size = _generator.Next(5, 10);
            int TTL = 20 + _generator.Next();

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

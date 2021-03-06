﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ParticleGame
{
    class FireEmitter : ParticleEmitter
    {

        public FireEmitter(List<Texture2D> textures, Vector2 location) : base(textures,location)
        {
        }
        protected override Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;

            Vector2 velocity = new Vector2(NextFloat*2-1 ,NextFloat*-2);

            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                    1f,
                    0.33f + 0.67f * (float)random.NextDouble(),
                    0f);
            float size = (float)random.NextDouble();
            int ttl = 20 + random.Next(40);

            return new ParticleFire(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
        private Particle GenerateSmokeParticle(Vector2 position, Vector2 velocity, float angle, float angularVelocity)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            float darkness = (float)random.NextDouble() / 2f;
            Color color = new Color(darkness, darkness, darkness);
            float size = (float)random.NextDouble();

            int ttl = 30 + random.Next(40);

            return new ParticleSmoke(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
        public override void Update()
        {
            PureUpdate();
        }
        public void PureUpdate()
        {
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
                else if (particles[particle].Old)
                {
                    particles.Add(GenerateSmokeParticle(particles[particle].Position, particles[particle].Velocity, particles[particle].Angle, particles[particle].AngularVelocity));
                }
            }
        }
    }
}

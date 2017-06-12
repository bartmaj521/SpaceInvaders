﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SpaceInvaders.Classes;

namespace SpaceInvaders.Classes
{
    abstract class GameObject
    {
        // Animacja obiektu
        public Animation animation { get; set; }

        // Hitbox obiektu
        protected FloatRect collider;

        // Abstrakcyjna metoda aktualizująca pozycję obiektu
        abstract public GameObject update(float deltaTime);

        public FloatRect getCollider()
        {
            return collider;
        }

        // Konstruktor bezargumentowy
        public GameObject()
        {

        }

        // Kostruktor txt - textura obiektu, _frames tablica kolejności klatek animacji, startingPosition - pozycja startowa(lewy górny róg), Scale - skala obiektu
        public GameObject(ref Texture txt, int[] _frames, float _frameTime, Vector2f startingPosition, Vector2f Scale)
        {
            animation = new Animation(ref txt, _frames, _frameTime, startingPosition, Scale);
            animation.updateAnimation(0, new Vector2f(0,0));

            collider = new FloatRect(startingPosition.X, startingPosition.Y, animation.animationSprite.GetGlobalBounds().Width, animation.animationSprite.GetGlobalBounds().Height);
        }
    }
}

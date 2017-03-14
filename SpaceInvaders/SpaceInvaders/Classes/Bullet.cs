using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;


namespace SpaceInvaders.Classes
{
    class Bullet: Projectile, ICloneable
    {       
        public Bullet(ref Texture txt, float _damage , Vector2f scale)
        {
            damage = _damage;
            animation = new Animation();

            animation.animationSprite.Texture = txt;
            animation.animationSprite.Scale = scale;

            collider.Width = animation.animationSprite.Texture.Size.X * animation.animationSprite.Scale.X;
            collider.Height = animation.animationSprite.Texture.Size.Y * animation.animationSprite.Scale.Y;
        }

        public override GameObject update(float deltaTime)
        {
            if (animation.animationSprite.Position.Y < topBoundary)
                return null;
            animation.animationSprite.Position += velocity * deltaTime;
            collider.Left = animation.animationSprite.Position.X;
            collider.Top = animation.animationSprite.Position.Y;
            return this;
        }

        override public object Clone()
        {
            Bullet p = (Bullet)MemberwiseClone();
            p.animation = new Animation();
            p.animation.animationSprite = new Sprite();
            p.animation.animationSprite.Texture = this.animation.animationSprite.Texture;
            p.velocity = this.velocity;
            return (object)p;
        }
    }
}
